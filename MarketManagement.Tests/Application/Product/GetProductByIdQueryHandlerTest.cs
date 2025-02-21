using MarketManagement.Application.Queries.Product.GetById;
using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Enums;
using MarketManagement.Domain.Repositories;
using Moq;

namespace MarketManagement.Tests.Application.Product
{
    [Collection(nameof(GetProductByIdQuery))]
    public class GetProductByIdQueryHandlerTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly GetProductByIdQueryHandler _getProductByIdQueryHandler;

        public GetProductByIdQueryHandlerTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _getProductByIdQueryHandler = new GetProductByIdQueryHandler(_productRepositoryMock.Object);
        }

        [Fact]
        [Trait("Application", "Get Product By Id")]
        public async Task Handle_ShouldGetProductById_WhenDataIsValid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Arroz";
            var currentPrice = 20;
            var lastMonthPrice = 15;
            var categoryEnum = CategoryEnum.Alimentos;

            var product = new ProductEntity(name, currentPrice, lastMonthPrice, categoryEnum);
            var command = new GetProductByIdQuery(id);

            _productRepositoryMock
             .Setup(r => r.GetByIdAsync(id))
             .ReturnsAsync(product);


            // Act
            var result = await _getProductByIdQueryHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Guid.Empty, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(currentPrice, result.CurrentPrice);
            Assert.Equal(lastMonthPrice, result.LastMonthPrice);
            Assert.Equal(categoryEnum, result.CategoryEnum);
            Assert.Equal(DateTime.Now.Date, result.CreatedAt.Date);
            Assert.Equal(DateTime.MinValue, result.UpdatedAt.Date);
            Assert.True(result.IsActive);
            _productRepositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        }
    }
}
