using System;
using System.Linq;
using Akka.Actor;
using hygiea.domain;
using hygiea.web.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace hygiea.web.Actors
{
    public class ClaimActor : ReceiveActor, IWithTimers
    {
        private readonly IServiceScope _scope;
        private readonly HealthServiceRepository _healthServiceRep;
        private (decimal Sum, int Count) _claimData;
        public ITimerScheduler Timers { get; set; }

        public ClaimActor(IServiceProvider sp)
        {
            _scope = sp.CreateScope();
            _healthServiceRep = _scope.ServiceProvider.GetRequiredService<HealthServiceRepository>();

            SetActorTimeout();
            Ready();
        }

        protected override void PreStart()
        {
            var claimRequestId = Context.Self.Path.ToString().Split('/').Last();
            var intervalToNextClaim = TimeSpan.FromSeconds(60 - DateTime.Now.Second);

            Timers.StartSingleTimer("Notificator", claimRequestId, intervalToNextClaim);
            //base.PreStart();
        }

        protected override void PostStop() => _scope.Dispose();

        private void SetActorTimeout() => Context.SetReceiveTimeout(TimeSpan.FromMinutes(2));

        private void Ready()
        {
            Receive<ClaimRequest>(claimRequest =>
            {
                var healthService = _healthServiceRep.GetBy(claimRequest.ProviderCode);
                _claimData.Sum += healthService.Price;
                _claimData.Count++;
            });

            Receive<string>(claimRequestId =>
            {
                Context.Parent.Tell(new Claim(claimRequestId, _claimData.Count, _claimData.Sum));
            });

            Receive<ReceiveTimeout>(_ => Context.Stop(Self));
        }
    }
}