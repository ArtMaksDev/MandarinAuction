#region usings

using Hangfire;
using MandarinAuction.App.Services.Auctions.Creators;
using Microsoft.Extensions.Configuration;

#endregion

namespace MandarinAuction.App.Schedulers;

public class CreateAuctionScheduler
{
    public static void Start(IConfiguration configuration)
    {
        RecurringJob.AddOrUpdate<AuctionCreatorService>(
            "AuctionCreator",
            a => a.Create(),
            configuration["Hangfire:CreateAuctionCron"]);
    }
}