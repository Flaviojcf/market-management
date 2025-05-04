using MarketManagement.Application.Commands.User.CreateUser;

namespace MarketManagement.API
{
    public static class Extensions
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInjectionForMediatR();

            return services;
        }

        private static IServiceCollection AddInjectionForMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateUserCommand).Assembly));

            return services;
        }
    }
}
