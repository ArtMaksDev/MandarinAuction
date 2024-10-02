#region usings

using MandarinAuction.App.Schedulers;
using MandarinAuction.App.Services.Emails.AuctionBidEmailNotifyServices;
using MandarinAuction.App.Services.Loggers.Auctions.AuctionLoggerService;
using MandarinAuction.Domain.Events;
using MandarinAuction.Domain.Events.Auctions.AuctionClose;
using MandarinAuction.Domain.Events.Auctions.AuctionCreated;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MandarinAuction.App.ServiceExtensions;

public static class EventListenerCollectionExtensions
{
    public static IServiceCollection AddEventListeners(this IServiceCollection services)
    {
        services.AddScoped<IEventListener<AuctionCreatedEventArgs>, AuctionLoggerService>();
        services.AddScoped<IEventListener<AuctionCreatedEventArgs>, AuctionEndScheduler>();
        services.AddScoped<IEventDispatcher<AuctionCreatedEventArgs>, EventDispatcher<AuctionCreatedEventArgs>>();


        services.AddScoped<IEventListener<AuctionCloseEventArgs>, AuctionBidEmailNotifyService>();
        services.AddScoped<IEventDispatcher<AuctionCloseEventArgs>, EventDispatcher<AuctionCloseEventArgs>>();

        return services;
    }
}