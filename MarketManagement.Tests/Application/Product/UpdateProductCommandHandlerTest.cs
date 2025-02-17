using MarketManagement.Application.Commands.Product.UpdateProduct;
using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Enums;
using MarketManagement.Domain.Repositories;
using Moq;

namespace MarketManagement.Tests.Application.Product
{
    [Collection(nameof(UpdateProductCommand))]
    public class UpdateProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly UpdateProductCommandHandler _updateProductCommandHandler;
        public UpdateProductCommandHandlerTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _updateProductCommandHandler = new UpdateProductCommandHandler(_productRepositoryMock.Object);
        }

        [Fact]
        [Trait("Application", "Update Product - Command")]
        public async Task Handle_ShouldUpdateProduct_WhenDataIsValid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new ProductEntity("Product Name", 100, 90, CategoryEnum.Limpeza);

            _productRepositoryMock
                         .Setup(r => r.GetByIdAsync(id))
                         .ReturnsAsync(product);

            var command = new UpdateProductCommand(id, "Arroz", 10, 15, CategoryEnum.Alimentos);

            // Act
            var result = await _updateProductCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("Arroz", product.Name);
            Assert.Equal(10, product.CurrentPrice);
            Assert.Equal(15, product.LastMonthPrice);
            Assert.Equal(CategoryEnum.Alimentos, product.CategoryEnum);

            _productRepositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
            _productRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

    }
}
