using MarketManagement.Domain.Constants;
using MarketManagement.Domain.Enums;
using MarketManagement.Domain.Exceptions;

namespace MarketManagement.Domain.Entities
{
    public sealed class ProductEntity : BaseEntity
    {
        public ProductEntity(string name, decimal currentPrice, decimal lastMonthPrice, CategoryEnum categoryEnum)
        {
            ValidateDomain(name, currentPrice, lastMonthPrice, categoryEnum);
            Name = name;
            CurrentPrice = currentPrice;
            LastMonthPrice = lastMonthPrice;
            CategoryEnum = categoryEnum;
        }

        public string Name { get; private set; }
        public decimal CurrentPrice { get; private set; }
        public decimal LastMonthPrice { get; private set; }
        public CategoryEnum CategoryEnum { get; private set; }

        public void Update(string name, decimal currentPrice, decimal lastMonthPrice, CategoryEnum categoryEnum)
        {
            ValidateDomain(name, currentPrice, lastMonthPrice, categoryEnum);
            Name = name;
            CurrentPrice = currentPrice;
            LastMonthPrice = lastMonthPrice;
            CategoryEnum = categoryEnum;
            UpdatedAt = DateTime.Now;
        }

        private static void ValidateDomain(string name, decimal currentPrice, decimal lastMonthPrice, CategoryEnum categoryEnum)
        {
            DomainException.When(string.IsNullOrEmpty(name), string.Format(DomainMessageConstant.messageFieldIsRequired, "name"));
            DomainException.When(currentPrice == 0, string.Format(DomainMessageConstant.messageFieldIsRequiredAndGreaterThan, "currentPrice", 0));
            DomainException.When(lastMonthPrice == 0, string.Format(DomainMessageConstant.messageFieldIsRequiredAndGreaterThan, "lastMonthPrice", 0));
            DomainException.When(!Enum.IsDefined(typeof(CategoryEnum), categoryEnum), string.Format(DomainMessageConstant.messageFieldIsRequired, "categoryEnum"));
        }
    }
}

