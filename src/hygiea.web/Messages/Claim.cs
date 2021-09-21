using System;
using Akka.Routing;

namespace hygiea.web.Messages
{
    public record Claim(string ServiceCode, string ProviderCode)
    : IConsistentHashable
    {
        public object ConsistentHashKey => $"{ProviderCode}{DateTime.Now:HHmm}";
    }
}