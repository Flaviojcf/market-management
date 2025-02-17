using MarketManagement.Domain.Enums;
using MediatR;

namespace MarketManagement.Application.Commands.Product.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public UpdateProductCommand(Guid id, string name, int currentPrice, int lastMonthPrice, CategoryEnum categoryEnum)
        {
            Id = id;
            Name = name;
            CurrentPrice = currentPrice;
            LastMonthPrice = lastMonthPrice;
            CategoryEnum = categoryEnum;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CurrentPrice { get; set; }
        public int LastMonthPrice { get; set; }
        public CategoryEnum CategoryEnum { get; set; }
    }
}
