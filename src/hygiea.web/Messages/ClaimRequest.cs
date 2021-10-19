using Akka.Routing;

namespace hygiea.web.Messages
{
    public record ClaimRequest(string ServiceCode, string ProviderCode)
    : IConsistentHashable
    {
        public object ConsistentHashKey => ProviderCode;
    }
}