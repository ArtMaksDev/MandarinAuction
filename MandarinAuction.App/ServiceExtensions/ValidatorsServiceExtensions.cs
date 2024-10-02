#region usings

using MandarinAuction.App.DTOs.Users;
using MandarinAuction.App.Validators;
using MandarinAuction.App.Validators.Users;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MandarinAuction.App.ServiceExtensions;

public static class ValidatorsServiceExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<UserIdentityDto>, UserIdentityValidator>();

        return services;
    }
}