using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Exceptions;

namespace MarketManagement.Tests.Domain
{
    [Collection(nameof(ShoppingListEntity))]
    public class ShoppingListUnitTest
    {
        [Fact]
        [Trait("Domain", "Create Shopping List")]
        public void CreateShoppingList_WithValidData_ReturnsShoppingList()
        {
            // Arrange
            var name = "Feira janeiro";
            var totalPrice = 10;
            var productId = Guid.NewGuid();

            // Act
            var shoppingList = new ShoppingListEntity(name, totalPrice, productId);

            // Assert
            Assert.NotNull(shoppingList);
            Assert.Equal(Guid.Empty, shoppingList.Id);
            Assert.Equal(name, shoppingList.Name);
            Assert.Equal(totalPrice, shoppingList.TotalPrice);
            Assert.Equal(productId, shoppingList.ProductId);
            Assert.True(shoppingList.IsActive);
            Assert.Equal(DateTime.Now.Date, shoppingList.CreatedAt.Date);
            Assert.Equal(DateTime.MinValue, shoppingList.UpdatedAt.Date);
        }

        [Theory]
        [InlineData("", 10, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01")]
        [InlineData("Feira janeiro", null, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01")]
        [InlineData("Feira janeiro", 10, null)]
        [Trait("Domain", "Create Shopping List")]
        public void CreateShoppingList_ShouldThrowDomainException_WhenInvalidDataIsProvided(string name, int totalPrice, string productId)
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => new ShoppingListEntity(name, totalPrice, string.IsNullOrWhiteSpace(productId) ? default : Guid.Parse(productId)));
        }

        [Fact]
        [Trait("Domain", "Update Shopping List")]
        public void DeActiveShoppingList_ReturnsDeActiveShoppingList()
        {
            // Arrange
            var name = "Feira janeiro";
            var totalPrice = 10;
            var productId = Guid.NewGuid();

            // Act
            var shoppingList = new ShoppingListEntity(name, totalPrice, productId);

            shoppingList.Deactivate();

            // Assert
            Assert.NotNull(shoppingList);
            Assert.False(shoppingList.IsActive);
            Assert.Equal(DateTime.Now.Date, shoppingList.UpdatedAt.Date);
        }

        [Fact]
        [Trait("Domain", "Update Shopping List")]
        public void ActiveShoppingList_ReturnsActiveShoppingList()
        {
            // Arrange
            var name = "Feira janeiro";
            var totalPrice = 10;
            var productId = Guid.NewGuid();

            // Act
            var shoppingList = new ShoppingListEntity(name, totalPrice, productId);
            shoppingList.Deactivate();
            shoppingList.Activate();

            // Assert
            Assert.NotNull(shoppingList);
            Assert.True(shoppingList.IsActive);
            Assert.Equal(DateTime.Now.Date, shoppingList.UpdatedAt.Date);
        }

        [Fact]
        [Trait("Domain", "Update Shopping List")]
        public void UpdateShoppingList_WithValidData_ReturnsShoppingList()
        {
            // Arrange
            var name = "Feira janeiro";
            var totalPrice = 10;
            var productId = Guid.NewGuid();

            // Act
            var shoppingList = new ShoppingListEntity(name, totalPrice, productId);

            shoppingList.Update("Feira fevereiro", 20, productId);

            // Assert
            Assert.NotNull(shoppingList);
            Assert.Equal(Guid.Empty, shoppingList.Id);
            Assert.Equal("Feira fevereiro", shoppingList.Name);
            Assert.Equal(20, shoppingList.TotalPrice);
            Assert.Equal(productId, shoppingList.ProductId);
            Assert.True(shoppingList.IsActive);
            Assert.Equal(DateTime.Now.Date, shoppingList.CreatedAt.Date);
            Assert.Equal(DateTime.Now.Date, shoppingList.UpdatedAt.Date);
        }

        [Theory]
        [InlineData("", 10, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01")]
        [InlineData("Feira janeiro", null, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01")]
        [InlineData("Feira janeiro", 10, null)]
        [Trait("Domain", "Update Shopping List")]
        public void UpdateShoppingList_ShouldThrowDomainException_WhenInvalidDataIsProvided(string name, int totalPrice, string productId)
        {
            // Arrange
            var shoppingListEntity = new ShoppingListEntity("Feira janeiro", 20, Guid.NewGuid());

            // Act & Assert
            Assert.Throws<DomainException>(() => shoppingListEntity.Update(name, totalPrice, string.IsNullOrWhiteSpace(productId) ? default : Guid.Parse(productId)));
        }
    }
}
