using PS.Domain.DTO;
using PS.Domain.Interfaces.Services;

namespace PS.Services.Imp
{
    public class CrawlerDataService : ICrawlerDataService
    {
        private readonly List<CrawlerDataRequest> _dataStorage = new();

        public CrawlerDataService()
        {
            _dataStorage.Add(new CrawlerDataRequest
            {
                Email = "example@example.com",
                Password = "password123",
                InvoiceIssuerId = Guid.NewGuid()
            });
        }

        public async Task<IEnumerable<CrawlerDataRequest>> GetAllAsync()
        {
            return await Task.FromResult(_dataStorage);
        }

        public async Task<CrawlerDataRequest> GetByIdAsync(Guid invoiceIssuerId)
        {
            var data = _dataStorage.FirstOrDefault(d => d.InvoiceIssuerId == invoiceIssuerId);
            return await Task.FromResult(data);
        }

        public async Task<CrawlerDataRequest> CreateAsync(CrawlerDataRequest data)
        {
            _dataStorage.Add(data);
            return await Task.FromResult(data);
        }

        public async Task UpdateAsync(Guid invoiceIssuerId, CrawlerDataRequest newData)
        {
            var data = _dataStorage.FirstOrDefault(d => d.InvoiceIssuerId == invoiceIssuerId);
            if (data != null)
            {
                _dataStorage.Remove(data);
                _dataStorage.Add(newData);
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid invoiceIssuerId)
        {
            var data = _dataStorage.FirstOrDefault(d => d.InvoiceIssuerId == invoiceIssuerId);
            if (data != null)
            {
                _dataStorage.Remove(data);
            }
            await Task.CompletedTask;
        }
    }
}