using MarketManagement.Domain.Services.Interfaces;
using MarketManagement.Infrastructure.InternalServices;
using Microsoft.Extensions.Configuration;
using Moq;

namespace MarketManagement.Tests.Infrastructure
{
    [Collection(nameof(AuthService))]
    public class AuthServiceTest
    {
        private readonly IAuthService _authService;
        public readonly Mock<IConfiguration> _configuration;

        public AuthServiceTest()
        {
            _configuration = new Mock<IConfiguration>();
            _authService = new AuthService(_configuration.Object);
        }

        [Fact]
        [Trait("Infrastructure", "Create Hash Password")]
        public void CreatePassword_WithValidData_ReturnsHashPassword()
        {
            // Arrange
            var password = "p@ssw0rd";

            // Act
            var hashPassword = _authService.HashPassword(password);

            // Assert
            Assert.NotNull(hashPassword);
            Assert.NotEmpty(hashPassword);
            Assert.NotEqual(password, hashPassword);
        }
    }
}
