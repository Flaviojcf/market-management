using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace MarketManagement.Infrastructure.Persistance.Repositories
{
    [ExcludeFromCodeCoverage]
    public class UserRepository : IUserRepository
    {
        public Task CreateAsync(UserEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
