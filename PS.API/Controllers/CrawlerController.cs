using Microsoft.AspNetCore.Mvc;
using PS.Domain.DTO;

namespace PS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrawlerController(ICrawlerDataService service) : ControllerBase
    {
        private readonly ICrawlerDataService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CrawlerDataRequest data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdData = await _service.CreateAsync(data);
            return CreatedAtAction(nameof(Get), new { id = createdData.InvoiceIssuerId }, createdData);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CrawlerDataRequest data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.UpdateAsync(id, data);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
