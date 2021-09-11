using Akka.Routing;

namespace hygiea.web.Messages
{
    public record AuthMessage(int BeneficiaryId, string ProcedureCode)
    : IConsistentHashable
    {
        public object ConsistentHashKey => BeneficiaryId;
    }
}