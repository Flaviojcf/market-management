using MarketManagement.Domain.Constants;
using MarketManagement.Domain.Exceptions;

namespace MarketManagement.Domain.Entities
{
    public sealed class PriceHistoryEntity : BaseEntity
    {
        public PriceHistoryEntity(decimal price, DateTime date, Guid productId)
        {
            ValidateDomain(price, date, productId);
            Price = price;
            Date = date;
            ProductId = productId;
        }

        public decimal Price { get; private set; }
        public DateTime Date { get; private set; }

        public Guid ProductId { get; private set; }
        public ProductEntity? Product { get; private set; }


        public void Update(decimal price, DateTime date, Guid productId)
        {
            ValidateDomain(price, date, productId);
            Price = price;
            Date = date;
            ProductId = productId;
            UpdatedAt = DateTime.Now;
        }

        private static void ValidateDomain(decimal price, DateTime date, Guid productId)
        {
            DomainException.When(price == 0, string.Format(DomainMessageConstant.messageFieldIsRequiredAndGreaterThan, "price", 0));
            DomainException.When(date == DateTime.MinValue, string.Format(DomainMessageConstant.messageFieldIsRequired, "date"));
            DomainException.When(productId == Guid.Empty, string.Format(DomainMessageConstant.messageFieldIsRequired, "productId"));
        }
    }
}
