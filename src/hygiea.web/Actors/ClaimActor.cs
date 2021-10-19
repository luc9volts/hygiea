using System;
using System.Linq;
using Akka.Actor;
using hygiea.domain;
using hygiea.web.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace hygiea.web.Actors
{
    public class ClaimActor : ReceiveActor, IWithUnboundedStash
    {
        public IStash Stash { get; set; }
        private readonly IServiceScope _scope;
        private readonly HealthServiceRepository _healthServiceRep;
        private decimal _claimValue;

        public ClaimActor(IServiceProvider sp)
        {
            _scope = sp.CreateScope();
            _healthServiceRep = _scope.ServiceProvider
                                .GetRequiredService<HealthServiceRepository>();

            Ready();
        }

        private void Ready()
        {
            SetActorTimeout();

            Receive<ClaimRequest>(claimRequest =>
            {
                var healthService = _healthServiceRep.GetBy(claimRequest.ServiceCode);
                _claimValue += healthService.Price;
            });

            Receive<ReceiveTimeout>(timeout =>
            {
                var claimRequestId = Context.Self.Path.ToString().Split('/').Last();
                Sender.Tell(new Claim(claimRequestId, _claimValue));
                Context.Stop(Self);
            });
        }

        private void SetActorTimeout() => Context.SetReceiveTimeout(TimeSpan.FromSeconds(60 - DateTime.Now.Second));

        protected override void PostStop() => _scope.Dispose();
    }
}