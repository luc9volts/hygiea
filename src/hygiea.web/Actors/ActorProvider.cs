using Akka.Actor;

namespace hygiea.web.Actors
{
    public interface ActorProvider
    {
        IActorRef Router { get; }
    }
}