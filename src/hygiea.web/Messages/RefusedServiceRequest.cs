namespace hygiea.web.Messages
{
    public record RefusedServiceRequest(int BeneficiaryId, string ServiceCode, string ProviderCode)
    : Notification($"Beneficiario: {BeneficiaryId} Serviço: {ServiceCode}");
}