using Microsoft.Extensions.Configuration;
using PS.Domain.DTO;
using PS.Domain.Interfaces.Services;
using System.Text;
using System.Text.Json;

namespace PS.Services.Imp
{
    public class InvoiceService(HttpClient httpClient,
                          IConfiguration configuration) : IInvoiceService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _configuration = configuration;

        public async Task GetInvoice(Guid invoiceId)
        {
            var response = await _httpClient.GetAsync($"{_configuration["API:BaseUrl"]}/api/invoices/{invoiceId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erro ao obter a fatura: {response.StatusCode}");
            }

            var contentType = response.Content.Headers.ContentType.MediaType;
            if (contentType != "application/pdf")
            {
                throw new InvalidOperationException("O conteúdo retornado não é um arquivo PDF.");
            }
        }

        public async Task SendInvoiceFile(Guid invoiceId)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "File", "01-2024.pdf");

            var content = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

            content.Add(fileContent, "invoice", Path.GetFileName(filePath));

            var response = await _httpClient.PostAsync($"{_configuration["API:BaseUrl"]}/api/invoices/{invoiceId}/file", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erro ao enviar a fatura: {response.StatusCode}");
            }
        }

        public async Task SendInvoiceData(Guid invoiceId)
        {
            InvoiceResponse invoice = new();
            string jsonInvoice = JsonSerializer.Serialize(invoice);
            var content = new StringContent(jsonInvoice, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["API:BaseUrl"]}/api/invoices/{invoiceId}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erro ao enviar a fatura: {response.StatusCode}");
            }
        }
    }
}
