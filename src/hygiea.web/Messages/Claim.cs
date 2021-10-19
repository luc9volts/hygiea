namespace hygiea.web.Messages
{
    public record Claim(string Id, decimal Value): Notification($"PEG {Id} Valor {Value}");
}