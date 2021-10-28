using Akka.Actor;
using Akka.DependencyInjection;
using hygiea.web.Messages;

namespace hygiea.web.Actors
{
    public class ActorRouter : ReceiveActor
    {
        private readonly DependencyResolver _props;
        private readonly IActorRef _notificationActor;

        public ActorRouter()
        {
            _props = DependencyResolver.For(Context.System);
            _notificationActor = Context.ActorOf(_props.Props<NotificationActor>(), "NotifyBrowsers");
            Ready();
        }

        private void Ready()
        {
            Receive<ServiceRequest>(msg =>
            {
                GetChildActor<BeneficiaryActor>(msg.ConsistentHashKey.ToString()).Tell(msg);
            });

            Receive<ClaimRequest>(msg =>
            {
                GetChildActor<ClaimActor>(msg.ConsistentHashKey.ToString()).Tell(msg);
            });

            Receive<Notification>(msg =>
            {
                _notificationActor.Tell(msg);
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