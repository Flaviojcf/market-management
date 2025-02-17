using MarketManagement.Application.Commands.Product.CreateProduct;
using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Enums;
using MarketManagement.Domain.Repositories;
using Moq;

namespace MarketManagement.Tests.Application.Product
{
    [Collection(nameof(CreateProductCommandHandler))]
    public class CreateProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly CreateProductCommandHandler _createProductCommandHandler;

        public CreateProductCommandHandlerTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _createProductCommandHandler = new CreateProductCommandHandler(_productRepositoryMock.Object);
        }

        [Fact]
        [Trait("Application", "Product - Command")]
        public async Task Handle_ShouldCreateProduct_WhenDataIsValid()
        {
            // Arrange
            var name = "Arroz";
            var currentPrice = 20;
            var lastMonthPrice = 15;
            var categoryEnum = CategoryEnum.Alimentos;
            var command = new CreateProductCommand(name, currentPrice, lastMonthPrice, categoryEnum);
            _productRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<ProductEntity>())).Returns(Task.CompletedTask);

            // Act
            var result = await _createProductCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Guid.Empty, result);
            _productRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<ProductEntity>()), Times.Once);
        }
    }
}
