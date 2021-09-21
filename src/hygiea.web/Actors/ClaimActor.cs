using System;
using System.Linq;
using Akka.Actor;
using hygiea.web.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace hygiea.web.Actors
{
    public class ClaimActor : ReceiveActor, IWithUnboundedStash
    {

        public IStash Stash { get; set; }
        private readonly IServiceScope _scope;

        public ClaimActor(IServiceProvider sp)
        {
            var claimId = Context.Self.Path.ToString().Split('/').Last();
            _scope = sp.CreateScope();

            Ready();
        }

        private void Ready()
        {
            Receive<Claim>(msg =>
            {
                //var a = 1;
            });
        }

        protected override void PostStop() => _scope.Dispose();

    }
}