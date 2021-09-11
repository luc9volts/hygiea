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
            _scope = sp.CreateScope();
            var beneficiaryId = int.Parse(Context.Self.Path.ToString().Split('/').Last());

            _thisBeneficiary = _scope.ServiceProvider
                                .GetRequiredService<BeneficiaryRepository>()
                                .GetBy(beneficiaryId);
            Ready();
        }

        private void Ready()
        {
            Receive<AuthMessage>(auth =>
            {
                var a = _thisBeneficiary.Name;
            });
        }

        protected override void PostStop() => _scope.Dispose();
    }
}