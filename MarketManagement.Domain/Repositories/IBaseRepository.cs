namespace MarketManagement.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task SaveChangesAsync();
    }
}
