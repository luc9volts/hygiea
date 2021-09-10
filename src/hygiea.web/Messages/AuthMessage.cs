using Akka.Routing;

namespace hygiea.web.Messages
{
    public record AuthMessage(string BeneficiaryNumber, string ProcedureCode)
    : IConsistentHashable
    {
        public object ConsistentHashKey => BeneficiaryNumber;
    }
}