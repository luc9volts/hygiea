using Akka.Actor;
using Akka.DependencyInjection;
using Akka.Routing;

namespace hygiea.web.Actors
{
    public class SpecificActorRouter<T> : ReceiveActor where T : ActorBase
    {
        private readonly DependencyResolver _props;

        public SpecificActorRouter()
        {
            _props = DependencyResolver.For(Context.System);
            Ready();
        }

        private void Ready()
        {
            Receive<IConsistentHashable>(msg =>
            {
                GetChildActor(msg.ConsistentHashKey.ToString()).Forward(msg);
            });
        }

        private IActorRef GetChildActor(string actorName)
        {
            var child = Context.Child(actorName);

            return child is Nobody
                ? Context.ActorOf(_props.Props<T>(), actorName)
                : child;
        }
    }
}