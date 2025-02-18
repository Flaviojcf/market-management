using MarketManagement.Application.Commands.Product.CreateProduct;
using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Enums;
using MarketManagement.Domain.Exceptions;
using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services.Interfaces;
using MarketManagement.Domain.Validations;
using Moq;

namespace MarketManagement.Tests.Application.Product
{
    [Collection(nameof(CreateProductCommandHandler))]
    public class CreateProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IProductValidate> _productValidate;
        private readonly CreateProductCommandHandler _createProductCommandHandler;

        public CreateProductCommandHandlerTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productValidate = new Mock<IProductValidate>();
            _createProductCommandHandler = new CreateProductCommandHandler(_productRepositoryMock.Object, _productValidate.Object);
        }

        [Fact]
        [Trait("Application", "Create Product - Command")]
        public async Task Handle_ShouldCreateProduct_WhenDataIsValid()
        {
            // Arrange
            var name = "Arroz";
            var currentPrice = 20;
            var lastMonthPrice = 15;
            var categoryEnum = CategoryEnum.Alimentos;
            var command = new CreateProductCommand(name, currentPrice, lastMonthPrice, categoryEnum);
            var validationResult = new ValidationResult();

            _productValidate.Setup(v => v.ValidateCreateProductAsync(command.Name))
                         .ReturnsAsync(validationResult);

            _productRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<ProductEntity>())).Returns(Task.CompletedTask);

            // Act
            var result = await _createProductCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Guid.Empty, result);
            _productRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<ProductEntity>()), Times.Once);
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        [Trait("Handler", "Create Product")]
        public async Task Handle_ShouldThrowException_WhenValidationFails()
        {
            // Arrange
            var command = new CreateProductCommand("Arroz", 20, 15, CategoryEnum.Alimentos);
            var validationResult = new ValidationResult();
            validationResult.AddError("O produto 'Arroz' já foi cadastrado");
            _productValidate.Setup(v => v.ValidateCreateProductAsync(command.Name))
                            .ReturnsAsync(validationResult);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _createProductCommandHandler.Handle(command, CancellationToken.None));

            Assert.Contains("O produto 'Arroz' já foi cadastrado", exception.Message);
            Assert.False(validationResult.IsValid);
            _productRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<ProductEntity>()), Times.Never);
        }
    }
}
