#region usings

using MandarinAuction.App.Mappings;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MandarinAuction.App.ServiceExtensions;

public static class MapperServiceExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MandarinProfile>();
            cfg.AddProfile<AuctionProfile>();
        });

        return services;
    }
}