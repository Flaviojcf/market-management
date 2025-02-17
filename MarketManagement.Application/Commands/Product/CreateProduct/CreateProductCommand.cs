using MarketManagement.Domain.Enums;
using MediatR;

namespace MarketManagement.Application.Commands.Product.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public CreateProductCommand(string name, int currentPrice, int lastMonthPrice, CategoryEnum categoryEnum)
        {
            Name = name;
            CurrentPrice = currentPrice;
            LastMonthPrice = lastMonthPrice;
            CategoryEnum = categoryEnum;
        }

        public string Name { get; set; }
        public int CurrentPrice { get; set; }
        public int LastMonthPrice { get; set; }
        public CategoryEnum CategoryEnum { get; set; }
    }
}
