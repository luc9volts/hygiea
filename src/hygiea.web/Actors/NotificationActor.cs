using System;
using Akka.Actor;
using hygiea.web.Hubs;
using hygiea.web.Messages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace hygiea.web.Actors
{
    public class NotificationActor : ReceiveActor, IWithUnboundedStash
    {
        public IStash Stash { get; set; }
        private readonly IServiceScope _scope;
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationActor(IServiceProvider sp)
        {
            _scope = sp.CreateScope();
            _hub = _scope.ServiceProvider.GetRequiredService<IHubContext<NotificationHub>>();
            
            Ready();
        }

        private void Ready()
        {
            Receive<Claim>(msg =>
            {
                _hub.Clients.All.SendAsync("ReceiveClaim", msg);
            });

            Receive<RefusedServiceRequest>(msg =>
            {
                _hub.Clients.All.SendAsync("ReceiveServiceRequest", msg);
            });
        }

        protected override void PostStop() => _scope.Dispose();
    }
}