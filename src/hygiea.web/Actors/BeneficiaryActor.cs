using System;
using System.Linq;
using Akka.Actor;
using hygiea.domain;
using hygiea.web.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace hygiea.web.Actors
{
    public class BeneficiaryActor : ReceiveActor, IWithUnboundedStash
    {
        public IStash Stash { get; set; }
        private readonly IServiceScope _scope;
        private readonly Beneficiary _thisBeneficiary;

        public BeneficiaryActor(IServiceProvider sp)
        {
            var beneficiaryId = int.Parse(Context.Self.Path.ToString().Split('/').Last());
            _scope = sp.CreateScope();
            _thisBeneficiary = _scope.ServiceProvider
                                .GetRequiredService<BeneficiaryRepository>()
                                .GetBy(beneficiaryId);
            Ready();
        }

        protected override void PostStop() => _scope.Dispose();

        private void Ready()
        {
            Receive<ServiceRequest>(msg =>
            {
                if (Approved(msg.ServiceCode))
                    Context.Parent.Tell(new ClaimRequest(msg.ServiceCode, msg.ProviderCode));
                else
                {
                    Context.Parent.Tell(new RefusedServiceRequest(msg.BeneficiaryId, msg.ServiceCode, msg.ProviderCode));
                }
            });
        }

        private bool Approved(string procedureCode) => _thisBeneficiary.CoveredProcedures.Contains(procedureCode);
    }
}