using MarketManagement.Domain.Constants;
using MarketManagement.Domain.Exceptions;

namespace MarketManagement.Domain.Entities
{
    public class ShoppingListItemEntity : BaseEntity
    {
        public ShoppingListItemEntity(int quantity, int totalPrice, Guid productId, Guid shoppingListId)
        {
            ValidateDomain(quantity, totalPrice, productId, shoppingListId);
            Quantity = quantity;
            TotalPrice = totalPrice;
            ProductId = productId;
            ShoppingListId = shoppingListId;
        }

        public int Quantity { get; private set; }
        public int TotalPrice { get; private set; }

        public Guid ProductId { get; private set; }
        public ProductEntity? Product { get; private set; }

        public Guid ShoppingListId { get; private set; }
        public ShoppingListEntity? ShoppingList { get; private set; }

        public void Update(int quantity, int totalPrice, Guid productId, Guid shoppingListId)
        {
            ValidateDomain(quantity, totalPrice, productId, shoppingListId);
            Quantity = quantity;
            TotalPrice = totalPrice;
            ProductId = productId;
            ShoppingListId = shoppingListId;
            UpdatedAt = DateTime.Now;
        }

        private static void ValidateDomain(int quantity, int totalPrice, Guid productId, Guid shoppingListId)
        {
            DomainException.When(quantity == 0, string.Format(DomainMessageConstant.messageFieldIsRequiredAndGreaterThan, "quantity", 0));
            DomainException.When(totalPrice == 0, string.Format(DomainMessageConstant.messageFieldIsRequiredAndGreaterThan, "totalPrice", 0));
            DomainException.When(productId == Guid.Empty, string.Format(DomainMessageConstant.messageFieldIsRequired, "productId"));
            DomainException.When(shoppingListId == Guid.Empty, string.Format(DomainMessageConstant.messageFieldIsRequired, "shoppingListId"));
        }
    }
}
