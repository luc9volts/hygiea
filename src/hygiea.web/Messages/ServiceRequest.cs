using Akka.Actor;
using Akka.Routing;

namespace hygiea.web.Messages
{
    public record ServiceRequest(int BeneficiaryId, string ServiceCode, string ProviderCode, IActorRef Router)
    : IConsistentHashable
    {
        public object ConsistentHashKey => BeneficiaryId;
    }
}