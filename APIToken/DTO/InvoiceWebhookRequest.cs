namespace PS.Domain.DTO
{
    public class InvoiceWebhookRequest
    {
        public Guid InvoiceId { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
