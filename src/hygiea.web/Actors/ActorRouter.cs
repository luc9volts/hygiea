using Akka.Actor;
using Akka.DependencyInjection;
using hygiea.web.Messages;

namespace hygiea.web.Actors
{
    public class ActorRouter : ReceiveActor
    {
        private readonly DependencyResolver _props;

        public ActorRouter()
        {
            _props = DependencyResolver.For(Context.System);
            Ready();
        }

        private void Ready()
        {
            Receive<ServiceRequest>(msg =>
            {
                GetChildActor<BeneficiaryActor>(msg.ConsistentHashKey.ToString()).Tell(msg);
            });

            Receive<Claim>(msg =>
            {
                GetChildActor<ClaimActor>(msg.ConsistentHashKey.ToString()).Forward(msg);
            });
        }

        private IActorRef GetChildActor<T>(string actorName) where T : ActorBase
        {
            var child = Context.Child(actorName);

            return child is Nobody
                ? Context.ActorOf(_props.Props<T>(), actorName)
                : child;
        }
    }
}