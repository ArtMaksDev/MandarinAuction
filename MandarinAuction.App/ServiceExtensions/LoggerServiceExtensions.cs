#region usings

using MandarinAuction.App.Services.Loggers.Auctions.AuctionLoggerService;
using MandarinAuction.App.Services.Loggers.Mandarins.MandarinLoggerService;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MandarinAuction.App.ServiceExtensions;

public static class LoggerServiceExtensions
{
    public static IServiceCollection AddLogServices(this IServiceCollection services)
    {
        services.AddScoped<IAuctionLoggerService, AuctionLoggerService>();
        services.AddScoped<IMandarinLoggerService, MandarinLoggerService>();

        return services;
    }
}