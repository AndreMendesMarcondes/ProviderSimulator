using PS.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using PS.Domain.Interfaces.Services;

namespace PS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController(IInvoiceService invoiceService) : ControllerBase
    {
        private readonly Random _random = new Random();
        private readonly IInvoiceService _invoiceService = invoiceService;

        [HttpPost("webhook")]
        public IActionResult ReceiveInvoice([FromBody] InvoiceWebhookRequest invoiceWebhook)
        {
            var simulation = _random.Next(1, 10);

            if (simulation == 1)
            {
                return Unauthorized(new { message = "Não autorizado. Simulação de falha aleatória." });
            }
            else if (simulation == 2)
            {
                return StatusCode(503, new { message = "Serviço indisponível. Simulação de falha aleatória." });
            }

            return Ok(new { message = "Fatura recebida com sucesso!" });
        }

        [HttpPost("RetrieveInvoiceDocument/{invoiceId}")]
        public async Task<IActionResult> GetInvoiceDocument(Guid invoiceId)
        {
            try
            {
                await _invoiceService.GetInvoice(invoiceId);
                return Ok();
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("{invoiceId}/file")]
        public async Task<IActionResult> SendInvoiceFile(Guid invoiceId)
        {
            await _invoiceService.SendInvoiceFile(invoiceId);
            return Ok();
        }

        [HttpPost("{invoiceId}")]
        public async Task<IActionResult> SendInvoiceData(Guid invoiceId)
        {
            await _invoiceService.SendInvoiceData(invoiceId);
            return Ok();
        }
    }
}
