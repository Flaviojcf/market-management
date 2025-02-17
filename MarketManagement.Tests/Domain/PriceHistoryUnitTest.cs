using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Exceptions;

namespace MarketManagement.Tests.Domain
{
    [Collection(nameof(PriceHistoryEntity))]
    public class PriceHistoryUnitTest
    {
        [Fact]
        [Trait("Domain", "Create Price History")]
        public void CreatePriceHistory_WithValidData_ReturnsPriceHistory()
        {
            // Arrange
            var price = 10;
            var date = DateTime.Now;
            var productId = Guid.NewGuid();

            // Act
            var priceHistory = new PriceHistoryEntity(price, date, productId);

            // Assert
            Assert.NotNull(priceHistory);
            Assert.Equal(Guid.Empty, priceHistory.Id);
            Assert.Equal(price, priceHistory.Price);
            Assert.Equal(date, priceHistory.Date);
            Assert.True(priceHistory.IsActive);
            Assert.Equal(DateTime.Now.Date, priceHistory.CreatedAt.Date);
            Assert.Equal(DateTime.MinValue, priceHistory.UpdatedAt.Date);
        }

        [Theory]
        [InlineData(0, "2024-01-01", "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01")]
        [InlineData(10, null, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01")]
        [InlineData(10, "2024-01-01", null)]
        [Trait("Domain", "Create Price History")]
        public void CreatePriceHistory_ShouldThrowDomainException_WhenInvalidDataIsProvided(int price, string date, string productId)
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => new PriceHistoryEntity(price, string.IsNullOrWhiteSpace(date) ? default : DateTime.Parse(date), string.IsNullOrWhiteSpace(productId) ? default : Guid.Parse(productId)));
        }

        [Fact]
        [Trait("Domain", "Update Price History")]
        public void DeActivePriceHistory_ReturnsDeActivePriceHistory()
        {
            // Arrange
            var price = 10;
            var date = DateTime.Now;
            var productId = Guid.NewGuid();

            // Act
            var priceHistory = new PriceHistoryEntity(price, date, productId);

            priceHistory.Deactivate();

            // Assert
            Assert.NotNull(priceHistory);
            Assert.False(priceHistory.IsActive);
            Assert.Equal(DateTime.Now.Date, priceHistory.UpdatedAt.Date);
        }

        [Fact]
        [Trait("Domain", "Update Price History")]
        public void ActivePriceHistory_ReturnsActivePriceHistory()
        {
            // Arrange
            var price = 10;
            var date = DateTime.Now;
            var productId = Guid.NewGuid();

            // Act
            var priceHistory = new PriceHistoryEntity(price, date, productId);
            priceHistory.Deactivate();
            priceHistory.Activate();

            // Assert
            Assert.NotNull(priceHistory);
            Assert.True(priceHistory.IsActive);
            Assert.Equal(DateTime.Now.Date, priceHistory.UpdatedAt.Date);
        }

        [Fact]
        [Trait("Domain", "Update Price History")]
        public void UpdatePriceHistory_WithValidData_ReturnsPriceHistory()
        {
            // Arrange
            var price = 10;
            var date = DateTime.Now;
            var productId = Guid.NewGuid();

            // Act
            var priceHistory = new PriceHistoryEntity(price, date, productId);

            priceHistory.Update(20, date, productId);

            // Assert
            Assert.NotNull(priceHistory);
            Assert.Equal(Guid.Empty, priceHistory.Id);
            Assert.Equal(20, priceHistory.Price);
            Assert.Equal(date, priceHistory.Date);
            Assert.Equal(productId, priceHistory.ProductId);
            Assert.True(priceHistory.IsActive);
            Assert.Equal(DateTime.Now.Date, priceHistory.CreatedAt.Date);
            Assert.Equal(DateTime.Now.Date, priceHistory.UpdatedAt.Date);
        }

        [Theory]
        [InlineData(0, "2024-01-01", "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01")]
        [InlineData(10, null, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01")]
        [InlineData(10, "2024-01-01", null)]
        [Trait("Domain", "Update Price History")]
        public void UpdatePriceHistory_ShouldThrowDomainException_WhenInvalidDataIsProvided(int price, string date, string productId)
        {
            // Arrange
            var priceHistoryEntity = new PriceHistoryEntity(10, DateTime.Now, Guid.NewGuid());

            // Act & Assert
            Assert.Throws<DomainException>(() => priceHistoryEntity.Update(price, string.IsNullOrWhiteSpace(date) ? default : DateTime.Parse(date), string.IsNullOrWhiteSpace(productId) ? default : Guid.Parse(productId)));
        }
    }
}
