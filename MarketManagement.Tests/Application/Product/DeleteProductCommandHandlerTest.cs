using MarketManagement.Application.Commands.Product.DeleteProduct;
using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Enums;
using MarketManagement.Domain.Repositories;
using MediatR;
using Moq;

namespace MarketManagement.Tests.Application.Product
{
    [Collection(nameof(DeleteProductCommandHandler))]
    public class DeleteProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly DeleteProductCommandHandler _deleteProductCommandHandler;

        public DeleteProductCommandHandlerTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _deleteProductCommandHandler = new DeleteProductCommandHandler(_productRepositoryMock.Object);
        }

        [Fact]
        [Trait("Application", "Delete Product - Command")]
        public async Task Handle_ShouldDeactivateProduct_WhenProductExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new ProductEntity("Product Name", 100, 90, CategoryEnum.Alimentos);

            _productRepositoryMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(product);

            var command = new DeleteProductCommand(id);

            // Act
            var result = await _deleteProductCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            Assert.False(product.IsActive);
            _productRepositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
            _productRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        [Trait("Application", "Delete Product - Command")]
        public async Task Handle_ShouldThrowException_WhenProductDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            _productRepositoryMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((ProductEntity)null);

            var command = new DeleteProductCommand(id);

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _deleteProductCommandHandler.Handle(command, CancellationToken.None));
            _productRepositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        }
    }
}
