namespace PS.Domain.Interfaces.Services
{
    public interface IApiTokenService
    {
        string GenerateToken(string chave, string userId);
    }
}
