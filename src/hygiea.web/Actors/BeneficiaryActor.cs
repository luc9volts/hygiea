using System;
using Akka.Actor;
using hygiea.web.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace hygiea.web.Actors
{
    public class BeneficiaryActor : ReceiveActor, IWithUnboundedStash
    {
        public IStash Stash { get; set; }
        private readonly IServiceScope _scope;

        public BeneficiaryActor(IServiceProvider sp)
        {
            _scope = sp.CreateScope();
            Ready();
        }

        protected override void PostStop() => _scope.Dispose();

        private void Ready()
        {
            Receive<AuthMessage>(auth =>
            {
                var a = Context.Self.Path.ToString();
            });
        }
    }
}