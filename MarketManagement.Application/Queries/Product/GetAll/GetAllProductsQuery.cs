using MarketManagement.Domain.Entities;
using MediatR;

namespace MarketManagement.Application.Queries.Product.GetAll
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductEntity>>
    {
        public GetAllProductsQuery(string query)
        {
            Query = query;
        }

        public string Query { get; set; }
    }
}
