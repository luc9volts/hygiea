namespace hygiea.web.Messages
{
    public record ServiceRequestStatus(int BeneficiaryId, string ServiceCode, string ProviderCode, bool Approved)
    : Notification($"Beneficiario: {BeneficiaryId} Serviço: {ServiceCode} Aprovado: {Approved}");
}