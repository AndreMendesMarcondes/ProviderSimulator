namespace PS.Domain.Interfaces.Services
{
    public interface IInvoiceService 
    {
        Task GetInvoice(Guid invoiceId);
        Task SendInvoiceFile(Guid invoiceId);
        Task SendInvoiceData(Guid invoiceId);
    }
}
