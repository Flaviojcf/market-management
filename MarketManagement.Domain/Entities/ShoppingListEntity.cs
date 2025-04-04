﻿using MarketManagement.Domain.Constants;
using MarketManagement.Domain.Exceptions;

namespace MarketManagement.Domain.Entities
{
    public sealed class ShoppingListEntity : BaseEntity
    {
        public ShoppingListEntity(string name, decimal totalPrice, Guid productId)
        {
            ValidateDomain(name, totalPrice, productId);
            Name = name;
            TotalPrice = totalPrice;
            ProductId = productId;
        }

        public string Name { get; private set; }
        public decimal TotalPrice { get; private set; }

        public Guid ProductId { get; private set; }
        public ProductEntity? Product { get; private set; }

        public void Update(string name, decimal totalPrice, Guid productId)
        {
            ValidateDomain(name, totalPrice, productId);
            Name = name;
            TotalPrice = totalPrice;
            ProductId = productId;
            UpdatedAt = DateTime.Now;
        }

        private static void ValidateDomain(string name, decimal totalPrice, Guid productId)
        {
            DomainException.When(string.IsNullOrEmpty(name), string.Format(DomainMessageConstant.messageFieldIsRequired, "name"));
            DomainException.When(totalPrice == 0, string.Format(DomainMessageConstant.messageFieldIsRequiredAndGreaterThan, "totalPrice", 0));
            DomainException.When(productId == Guid.Empty, string.Format(DomainMessageConstant.messageFieldIsRequired, "productId"));
        }
    }
}
