using MarketManagement.Domain.Entities;

namespace MarketManagement.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity> GetByEmail(string email);
    }
}
