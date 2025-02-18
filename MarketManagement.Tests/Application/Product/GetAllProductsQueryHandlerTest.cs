using MarketManagement.Application.Queries.Product.GetAll;
using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Enums;
using MarketManagement.Domain.Repositories;
using Moq;

namespace MarketManagement.Tests.Application.Product
{
    [Collection(nameof(GetAllProductsQuery))]
    public class GetAllProductsQueryHandlerTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly GetAllProductsQueryHandler _getAllProductsQueryHandler;

        public GetAllProductsQueryHandlerTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _getAllProductsQueryHandler = new GetAllProductsQueryHandler(_productRepositoryMock.Object);
        }

        [Fact]
        [Trait("Application", "Get All Products")]
        public async Task Handle_ShouldGetAllProducts()
        {
            // Arrange
            var product1 = new ProductEntity("Arroz", 10, 15, CategoryEnum.Alimentos);
            var product2 = new ProductEntity("Detergente", 5, 4, CategoryEnum.Limpeza);
            var products = new List<ProductEntity>() { product1, product2 };

            var command = new GetAllProductsQuery("");

            _productRepositoryMock
            .Setup(r => r.GetAllAsync())
             .ReturnsAsync(products);


            // Act
            var result = await _getAllProductsQueryHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            var resultProduct1 = result.FirstOrDefault(p => p.Name == "Arroz");
            var resultProduct2 = result.FirstOrDefault(p => p.Name == "Detergente");

            Assert.NotNull(resultProduct1);
            Assert.Equal(10, resultProduct1.CurrentPrice);
            Assert.Equal(15, resultProduct1.LastMonthPrice);
            Assert.Equal(CategoryEnum.Alimentos, resultProduct1.CategoryEnum);
            Assert.True(resultProduct1.IsActive);
            Assert.Equal(DateTime.Today, resultProduct1.CreatedAt.Date);

            Assert.NotNull(resultProduct2);
            Assert.Equal(5, resultProduct2.CurrentPrice);
            Assert.Equal(4, resultProduct2.LastMonthPrice);
            Assert.Equal(CategoryEnum.Limpeza, resultProduct2.CategoryEnum);
            Assert.True(resultProduct2.IsActive);
            Assert.Equal(DateTime.Today, resultProduct2.CreatedAt.Date);

            _productRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
