using Akka.Routing;

namespace hygiea.web.Messages
{
    public record ServiceRequest(int BeneficiaryId, string ServiceCode, string ProviderCode)
    : IConsistentHashable
    {
        public object ConsistentHashKey => BeneficiaryId;
    }
}