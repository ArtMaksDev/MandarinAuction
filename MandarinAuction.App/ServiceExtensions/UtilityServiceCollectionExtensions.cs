#region usings

using MandarinAuction.App.Services.Mandarins.Generators;
using MandarinAuction.Domain.Models;
using MandarinAuction.Infrastructure;
using MandarinAuction.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MandarinAuction.App.ServiceExtensions;

public static class UtilityServiceCollectionExtensions
{
    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, MsUnitOfWork>();

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddValidators();

        services.AddScoped<IMandarinGenerator, MandarinGenerator>();

        services.AddLogServices();

        return services;
    }
}