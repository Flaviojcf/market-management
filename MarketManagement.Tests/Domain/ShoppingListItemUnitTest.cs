using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Exceptions;

namespace MarketManagement.Tests.Domain
{
    [Collection(nameof(ShoppingListItemEntity))]
    public class ShoppingListItemItemUnitTest
    {
        [Fact]
        [Trait("Domain", "Create Shopping List Item")]
        public void CreateShoppingListItem_WithValidData_ReturnsShoppingListItem()
        {
            // Arrange
            var quantity = 1;
            var totalPrice = 10;
            var productId = Guid.NewGuid();
            var shoppingListId = Guid.NewGuid();

            // Act
            var shoppingList = new ShoppingListItemEntity(quantity, totalPrice, productId, shoppingListId);

            // Assert
            Assert.NotNull(shoppingList);
            Assert.Equal(Guid.Empty, shoppingList.Id);
            Assert.Equal(quantity, shoppingList.Quantity);
            Assert.Equal(totalPrice, shoppingList.TotalPrice);
            Assert.Equal(productId, shoppingList.ProductId);
            Assert.Equal(shoppingListId, shoppingList.ShoppingListId);
            Assert.True(shoppingList.IsActive);
            Assert.Equal(DateTime.Now.Date, shoppingList.CreatedAt.Date);
            Assert.Equal(DateTime.MinValue, shoppingList.UpdatedAt.Date);
        }

        [Theory]
        [InlineData(0, 10, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01", "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c02")]
        [InlineData(1, 0, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01", "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c02")]
        [InlineData(1, 10, null, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c02")]
        [InlineData(1, 10, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01", null)]
        [Trait("Domain", "Create Shopping List Item")]
        public void CreateShoppingListItem_ShouldThrowDomainException_WhenInvalidDataIsProvided(int quantity, int totalPrice, string productId, string shoppingListId)
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => new ShoppingListItemEntity(quantity, totalPrice, string.IsNullOrWhiteSpace(productId) ? default : Guid.Parse(productId), string.IsNullOrWhiteSpace(shoppingListId) ? default : Guid.Parse(shoppingListId)));
        }

        [Fact]
        [Trait("Domain", "Update Shopping List Item")]
        public void DeActiveShoppingListItem_ReturnsDeActiveShoppingListItem()
        {
            // Arrange
            var quantity = 1;
            var totalPrice = 10;
            var productId = Guid.NewGuid();
            var shoppingListId = Guid.NewGuid();

            // Act
            var shoppingList = new ShoppingListItemEntity(quantity, totalPrice, productId, shoppingListId);

            shoppingList.Deactivate();

            // Assert
            Assert.NotNull(shoppingList);
            Assert.False(shoppingList.IsActive);
            Assert.Equal(DateTime.Now.Date, shoppingList.UpdatedAt.Date);
        }

        [Fact]
        [Trait("Domain", "Update Shopping List Item")]
        public void ActiveShoppingListItem_ReturnsActiveShoppingListItem()
        {
            // Arrange
            var quantity = 1;
            var totalPrice = 10;
            var productId = Guid.NewGuid();
            var shoppingListId = Guid.NewGuid();

            // Act
            var shoppingList = new ShoppingListItemEntity(quantity, totalPrice, productId, shoppingListId);
            shoppingList.Deactivate();
            shoppingList.Activate();

            // Assert
            Assert.NotNull(shoppingList);
            Assert.True(shoppingList.IsActive);
            Assert.Equal(DateTime.Now.Date, shoppingList.UpdatedAt.Date);
        }

        [Fact]
        [Trait("Domain", "Update Shopping List Item")]
        public void UpdateShoppingListItem_WithValidData_ReturnsShoppingListItem()
        {
            // Arrange
            var quantity = 1;
            var totalPrice = 10;
            var productId = Guid.NewGuid();
            var shoppingListId = Guid.NewGuid();

            // Act
            var shoppingList = new ShoppingListItemEntity(quantity, totalPrice, productId, shoppingListId);

            shoppingList.Update(2, 30, productId, shoppingListId);

            // Assert
            Assert.NotNull(shoppingList);
            Assert.Equal(Guid.Empty, shoppingList.Id);
            Assert.Equal(2, shoppingList.Quantity);
            Assert.Equal(30, shoppingList.TotalPrice);
            Assert.Equal(productId, shoppingList.ProductId);
            Assert.Equal(shoppingListId, shoppingList.ShoppingListId);
            Assert.True(shoppingList.IsActive);
            Assert.Equal(DateTime.Now.Date, shoppingList.CreatedAt.Date);
            Assert.Equal(DateTime.Now.Date, shoppingList.UpdatedAt.Date);
        }

        [Theory]
        [InlineData(0, 10, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01", "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c02")]
        [InlineData(1, 0, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01", "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c02")]
        [InlineData(1, 10, null, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c02")]
        [InlineData(1, 10, "d3b07384-d9a1-4a2d-bf43-bb6e5b7f5c01", null)]
        [Trait("Domain", "Update Shopping List Item")]
        public void UpdateShoppingListItem_ShouldThrowDomainException_WhenInvalidDataIsProvided(int quantity, int totalPrice, string productId, string shoppingListId)
        {
            // Arrange
            var shoppingList = new ShoppingListItemEntity(1, 10, Guid.NewGuid(), Guid.NewGuid());

            // Act & Assert
            Assert.Throws<DomainException>(() => new ShoppingListItemEntity(quantity, totalPrice, string.IsNullOrWhiteSpace(productId) ? default : Guid.Parse(productId), string.IsNullOrWhiteSpace(shoppingListId) ? default : Guid.Parse(shoppingListId)));
        }
    }
}
