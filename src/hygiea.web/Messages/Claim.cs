namespace hygiea.web.Messages
{
    public record Claim(string Id, (decimal Sum, int Count) Data): Notification($"PEG {Id} Valor {Data.Sum}");
}