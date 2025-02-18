using MarketManagement.Domain.Entities;
using MediatR;

namespace MarketManagement.Application.Queries.Product.GetById
{
    public class GetProductByIdQuery : IRequest<ProductEntity>
    {
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
