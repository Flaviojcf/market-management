using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services;
using MarketManagement.Domain.Services.Interfaces;
using MarketManagement.Infrastructure.InternalServices;
using MarketManagement.Infrastructure.Persistance;
using MarketManagement.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace MarketManagement.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer(configuration);
            services.AddRepositories();
            services.AddServices();
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserValidateService, UserValidateService>();
            services.AddScoped<IProductValidateService, ProductValidateService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }

        private static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MarketManagementDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
