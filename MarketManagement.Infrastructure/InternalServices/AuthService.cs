using MarketManagement.Domain.Record;
using MarketManagement.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MarketManagement.Infrastructure.InternalServices
{
    public class AuthService(IConfiguration configuration) : IAuthService
    {
        public readonly IConfiguration _configuration = configuration;

        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public string GenerateJwtToken(string email, IList<string>? roles)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // TODO: Add roles to claims
            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            var token = new JwtSecurityToken(issuer: issuer,
                                             audience: audience,
                                             expires: DateTime.Now.AddHours(8),
                                             signingCredentials: credentials,
                                             claims: claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }

        public string GenerateRefreshToken(string email)
        {
            var key = _configuration["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var expirationTimeInMinutes = _configuration.GetValue<int>("Jwt:RefreshExpirationTimeInMinutes");

            var token = new JwtSecurityToken(
                issuer: _configuration["Issuer"],
                audience: _configuration["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationTimeInMinutes),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }

        public async Task<TokenValidationResultRecord> ValidateTokenAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return new TokenValidationResultRecord(false, "");

            var tokenParameters = GetTokenValidationParameters(_configuration);
            var validTokenResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(token, tokenParameters);

            if (!validTokenResult.IsValid)
                return new TokenValidationResultRecord(false, "");

            var userName = validTokenResult
                .Claims.FirstOrDefault(c => c.Key == ClaimTypes.NameIdentifier).Value as string ?? string.Empty;

            return new TokenValidationResultRecord(true, userName);

        }

        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            var tokenKey = Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty);

            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
            };
        }
    }
}
