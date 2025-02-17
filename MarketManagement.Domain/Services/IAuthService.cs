using MarketManagement.Domain.Record;

namespace MarketManagement.Domain.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, IList<string>? roles = null);
        string GenerateRefreshToken(string email);
        string HashPassword(string password);
        Task<TokenValidationResultRecord> ValidateTokenAsync(string token);
    }
}
