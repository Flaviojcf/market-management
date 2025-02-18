using MarketManagement.Domain.Entities;

namespace MarketManagement.Domain.Repositories
{
    public interface IProductRepository : IBaseRepository<ProductEntity>
    {
        Task<ProductEntity> GetByNameAsync(string name);
    }
}
