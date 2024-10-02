using MandarinAuction.UIModels.Mappings.Users;
using Microsoft.Extensions.DependencyInjection;

namespace MandarinAuction.UIModels.Mappings.DI;

public static class UiModelsMappingExtensions
{
    public static IServiceCollection AddUiMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<UserProfile>();
        });

        return services;
    }
}