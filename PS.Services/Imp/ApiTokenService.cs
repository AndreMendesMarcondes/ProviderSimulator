using PS.Domain.Interfaces.Services;
using System.Text;

namespace PS.Services.Imp
{
    public class ApiTokenService : IApiTokenService
    {
        public string GenerateToken(string chave, string userId)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var tokenData = $"{chave}:{userId}:{timestamp}";
            var tokenBytes = Encoding.UTF8.GetBytes(tokenData);
            var tokenBase64 = Convert.ToBase64String(tokenBytes);
            return tokenBase64;
        }
    }
}
