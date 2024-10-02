#region usings

using Hangfire;
using Hangfire.SqlServer;
using MandarinAuction.App.Schedulers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MandarinAuction.App.ServiceExtensions;

public static class HangfireConfiguratorServiceExtensions
{
    private const string CfgDbName = "MsDbConnect";

    public static void AddHangfireStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(cfg =>
            cfg.UseSqlServerStorage(configuration.GetConnectionString(CfgDbName)));
        services.AddHangfireServer();

        ConfigureJobStorage(configuration);

        AddSchedulers(services);
    }

    private static void ConfigureJobStorage(IConfiguration configuration)
    {
        JobStorage.Current = new SqlServerStorage(configuration.GetConnectionString(CfgDbName));
    }

    private static void AddSchedulers(this IServiceCollection services)
    {
        services.AddScoped<MandarinCleanUpScheduler>();
    }
}