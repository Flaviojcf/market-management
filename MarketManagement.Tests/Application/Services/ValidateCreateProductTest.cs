using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Enums;
using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services;
using Moq;

namespace MarketManagement.Tests.Application.Services
{
    [Collection(nameof(ValidateCreateProduct))]
    public class ValidateCreateProductTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly ValidateCreateProduct _validateCreateProduct;

        public ValidateCreateProductTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _validateCreateProduct = new ValidateCreateProduct(_productRepository.Object);
        }

        [Fact]
        [Trait("Service", "Validate Create Product")]
        public async Task Handle_ShouldNotCreateProduct_WhenNameIsRegistered()
        {
            // Arrange
            var name = "Arroz";
            var currentPrice = 20;
            var lastMonthPrice = 15;
            var categoryEnum = CategoryEnum.Alimentos;
            var existingProduct = new ProductEntity(name, currentPrice, lastMonthPrice, categoryEnum);

            _productRepository.Setup(r => r.GetByNameAsync(name)).ReturnsAsync(existingProduct);

            // Act
            var result = await _validateCreateProduct.ValidateCreateProductAsync(name);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e == $"O produto '{name}' já foi cadastrado");
        }
    }
}