using Microsoft.AspNetCore.Mvc;
using PS.Domain.Interfaces.Services;
using static PS.API.Middleware.TokenValidationMiddleware;

namespace PS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiTokenController(IApiTokenService apiTokenService) : ControllerBase
    {
        private const string CHAVE = "Biludinhos";
        private const string USERID = "Usuario123";
        private readonly IApiTokenService _apiTokenService = apiTokenService;

        [HttpGet("Test")]
        [ValidateApiToken]
        public IActionResult TestToken()
        {
            return Ok("Token válido!");
        }

        [HttpGet]
        public IActionResult GetToken()
        {
            var token = _apiTokenService.GenerateToken(CHAVE, USERID);
            return Ok(token);
        }
    }
}
