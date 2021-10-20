namespace hygiea.web.Messages
{
    public record Claim(string Id, int Quantity, decimal Value): Notification($"PEG {Id} Valor {Value}");
}