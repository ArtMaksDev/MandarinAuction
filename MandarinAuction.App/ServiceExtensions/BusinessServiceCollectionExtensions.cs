using MandarinAuction.App.DTOs.Users;
using MandarinAuction.App.Services.Auctions;
using MandarinAuction.App.Services.Auctions.Bids;
using MandarinAuction.App.Services.Auctions.Creators;
using MandarinAuction.App.Services.Auctions.Generators;
using MandarinAuction.App.Services.Emails.AuctionBidEmailNotifyServices;
using MandarinAuction.App.Services.Users.Creator;
using MandarinAuction.App.Services.Users.SignIn;

using Microsoft.Extensions.DependencyInjection;

namespace MandarinAuction.App.ServiceExtensions
{
    public static class BusinessServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserCreatorService<UserIdentityDto>, UserCreatorService>();
            services.AddScoped<SignInService>();

            services.AddScoped<IAuctionBidEmailNotifyService, AuctionBidEmailNotifyService>();

            services.AddSingleton<Random>();

            services.AddScoped<IAuctionService, AuctionService>();
            services.AddScoped<IAuctionBidService, AuctionBidService>();
            services.AddScoped<IAuctionGenerator, AuctionGenerator>();
            services.AddScoped<AuctionCreatorService>();

            services.AddEventListeners();

            return services;
        }
    }

}
