using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MarketManagement.Infrastructure.Persistance
{
    [ExcludeFromCodeCoverage]
    public class MarketManagementDbContext : DbContext
    {
        public MarketManagementDbContext(DbContextOptions<MarketManagementDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
