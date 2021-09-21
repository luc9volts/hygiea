using System;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DependencyInjection;
using hygiea.web.Actors;
using Microsoft.Extensions.Hosting;

namespace hygiea.web
{
    public class AkkaService : ActorProvider, IHostedService
    {
        private ActorSystem _actorSystem;
        public IActorRef Router { get; private set; }
        private readonly IServiceProvider _sp;

        public AkkaService(IServiceProvider sp) => _sp = sp;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var bootstrap = BootstrapSetup.Create();
            var di = DependencyResolverSetup.Create(_sp);
            var actorSystemSetup = bootstrap.And(di);

            _actorSystem = ActorSystem.Create("HygieaSystem", actorSystemSetup);

            var props = DependencyResolver.For(_actorSystem).Props<ActorRouter>();

            this.Router = _actorSystem.ActorOf(props, "beneficiary-pool");

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // theoretically, shouldn't even need this - will be invoked automatically via CLR exit hook
            // but it's good practice to actually terminate IHostedServices when ASP.NET asks you to
            await CoordinatedShutdown.Get(_actorSystem).Run(CoordinatedShutdown.ClrExitReason.Instance);
        }
    }
}