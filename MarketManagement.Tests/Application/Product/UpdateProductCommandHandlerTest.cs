using MarketManagement.Application.Commands.Product.UpdateProduct;
using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Enums;
using MarketManagement.Domain.Exceptions;
using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services.Interfaces;
using MarketManagement.Domain.Validations;
using Moq;

namespace MarketManagement.Tests.Application.Product
{
    [Collection(nameof(UpdateProductCommand))]
    public class UpdateProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IProductValidateService> _productValidate;
        private readonly UpdateProductCommandHandler _updateProductCommandHandler;
        public UpdateProductCommandHandlerTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productValidate = new Mock<IProductValidateService>();
            _updateProductCommandHandler = new UpdateProductCommandHandler(_productRepositoryMock.Object, _productValidate.Object);
        }

        [Fact]
        [Trait("Application", "Update Product - Command")]
        public async Task Handle_ShouldUpdateProduct_WhenDataIsValid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new ProductEntity("Product Name", 100, 90, CategoryEnum.Limpeza);
            var validationResult = new ValidationResult();

            _productValidate.Setup(v => v.ValidateUpdateProductAsync(id))
                    .ReturnsAsync(validationResult);

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

            var command = new UpdateProductCommand(id, "Arroz", 10, 15, CategoryEnum.Alimentos);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _updateProductCommandHandler.Handle(command, CancellationToken.None));
            Assert.Contains($"O produto '{id}' não existe.", exception.Message);
            _productRepositoryMock.Verify(r => r.GetByIdAsync(id), Times.Never);
            Assert.False(validationResult.IsValid);
        }
    }
}
