using PS.Domain.DTO;
public interface ICrawlerDataService
{
    Task<IEnumerable<CrawlerDataRequest>> GetAllAsync();
    Task<CrawlerDataRequest> GetByIdAsync(Guid invoiceIssuerId);
    Task<CrawlerDataRequest> CreateAsync(CrawlerDataRequest data);
    Task UpdateAsync(Guid invoiceIssuerId, CrawlerDataRequest data);
    Task DeleteAsync(Guid invoiceIssuerId);
}
