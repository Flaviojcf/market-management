using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Enums;
using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services;
using Moq;

namespace MarketManagement.Tests.Application.Services
{
    [Collection(nameof(ProductValidateService))]
    public class ProductValidateServiceTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly ProductValidateService _ProductValidateService;

        public ProductValidateServiceTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _ProductValidateService = new ProductValidateService(_productRepository.Object);
        }

        [Fact]
        [Trait("Service", "Validate Create Product")]
        public async Task Handle_ShouldCreateProduct_WhenNameIsNotRegistered()
        {
            // Arrange
            var name = "Arroz";
            _productRepository.Setup(r => r.GetByNameAsync(name)).ReturnsAsync((ProductEntity)null);

            // Act
            var result = await _ProductValidateService.ValidateCreateProductAsync(name);

            // Assert
            Assert.True(result.IsValid);
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
            var result = await _ProductValidateService.ValidateCreateProductAsync(name);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e == $"O produto '{name}' já foi cadastrado");
        }


        [Fact]
        [Trait("Service", "Validate Update Product")]
        public async Task Handle_ShouldUpdateProduct_WhenProductExists()
        {
            // Arrange
            var name = "Arroz";
            var currentPrice = 20;
            var lastMonthPrice = 15;
            var categoryEnum = CategoryEnum.Alimentos;
            var existingProduct = new ProductEntity(name, currentPrice, lastMonthPrice, categoryEnum);
            _productRepository.Setup(r => r.GetByIdAsync(existingProduct.Id)).ReturnsAsync(existingProduct);

            // Act
            var result = await _ProductValidateService.ValidateUpdateProductAsync(existingProduct.Id);

            // Assert
            Assert.True(result.IsValid);
        }


        [Fact]
        [Trait("Service", "Validate Update Product")]
        public async Task Handle_ShouldNotUpdateProduct_WhenProductNotExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _productRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync((ProductEntity)null);

            // Act
            var result = await _ProductValidateService.ValidateUpdateProductAsync(productId);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}