#region usings

using Microsoft.Extensions.Configuration;

#endregion

namespace MandarinAuction.App.Schedulers;

public static class JobsScheduler
{
    public static void Start(IConfiguration configuration)
    {
        CreateAuctionScheduler.Start(configuration);
    }
}