using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace MarketManagement.Infrastructure.Persistance.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ProductRepository : IProductRepository
    {
        public Task CreateAsync(ProductEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductEntity> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
