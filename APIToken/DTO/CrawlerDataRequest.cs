namespace PS.Domain.DTO
{
    public class CrawlerDataRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required Guid InvoiceIssuerId { get; set; }
    }
}
