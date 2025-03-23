using MarketManagement.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketManagement.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer(configuration);

            return services;
        }

        private static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MarketManagementDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
