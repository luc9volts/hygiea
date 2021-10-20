using System;
using System.Linq;
using Akka.Actor;
using hygiea.domain;
using hygiea.web.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace hygiea.web.Actors
{
    public class ClaimActor : ReceiveActor
    {
        private readonly IServiceScope _scope;
        private readonly HealthServiceRepository _healthServiceRep;
        private (decimal Sum, int Count) _claimData;

        public ClaimActor(IServiceProvider sp)
        {
            _scope = sp.CreateScope();
            _healthServiceRep = _scope.ServiceProvider.GetRequiredService<HealthServiceRepository>();

            SetActorTimeout();
            Ready();
        }

        protected override void PostStop() => _scope.Dispose();

        private void SetActorTimeout() => Context.SetReceiveTimeout(TimeSpan.FromSeconds(60 - DateTime.Now.Second));

        private void Ready()
        {
            Receive<ClaimRequest>(claimRequest =>
            {
                var healthService = _healthServiceRep.GetBy(claimRequest.ProviderCode);
                _claimData.Sum += healthService.Price;
                _claimData.Count++;
            });

            Receive<ReceiveTimeout>(_ =>
            {
                var claimRequestId = Context.Self.Path.ToString().Split('/').Last();
                Context.Parent.Tell(new Claim(claimRequestId, _claimData.Count, _claimData.Sum));
                Context.Stop(Self);
            });
        }
    }
}