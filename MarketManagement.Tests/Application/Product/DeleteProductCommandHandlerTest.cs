using MarketManagement.Application.Commands.Product.DeleteProduct;
using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Enums;
using MarketManagement.Domain.Exceptions;
using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services.Interfaces;
using MarketManagement.Domain.Validations;
using MediatR;
using Moq;

namespace MarketManagement.Tests.Application.Product
{
    [Collection(nameof(DeleteProductCommandHandler))]
    public class DeleteProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IProductValidateService> _productValidate;
        private readonly DeleteProductCommandHandler _deleteProductCommandHandler;
        public DeleteProductCommandHandlerTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productValidate = new Mock<IProductValidateService>();
            _deleteProductCommandHandler = new DeleteProductCommandHandler(_productRepositoryMock.Object, _productValidate.Object);
        }

        [Fact]
        [Trait("Application", "Delete Product - Command")]
        public async Task Handle_ShouldDeactivateProduct_WhenProductExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new ProductEntity("Product Name", 100, 90, CategoryEnum.Alimentos);
            var validationResult = new ValidationResult();

            _productValidate.Setup(v => v.ValidateUpdateProductAsync(id))
                    .ReturnsAsync(validationResult);

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
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        [Trait("Application", "Delete Product - Command")]
        public async Task Handle_ShouldThrowException_WhenProductDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var validationResult = new ValidationResult();

            validationResult.AddError($"O produto '{id}' não existe.");

            _productValidate.Setup(v => v.ValidateUpdateProductAsync(id))
                    .ReturnsAsync(validationResult);

            var command = new DeleteProductCommand(id);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _deleteProductCommandHandler.Handle(command, CancellationToken.None));
            Assert.Contains($"O produto '{id}' não existe.", exception.Message);
            _productRepositoryMock.Verify(r => r.GetByIdAsync(id), Times.Never);
            Assert.False(validationResult.IsValid);
        }
    }
}
