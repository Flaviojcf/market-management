using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MarketManagement.Infrastructure.Persistance
{
    public class MarketManagementDbContext : DbContext
    {
        public MarketManagementDbContext(DbContextOptions<MarketManagementDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
