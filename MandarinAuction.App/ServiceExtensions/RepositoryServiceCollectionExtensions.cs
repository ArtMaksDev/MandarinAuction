using MandarinAuction.Domain.Repositories;
using MandarinAuction.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MandarinAuction.App.ServiceExtensions
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<IMandarinRepository, MandarinRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
