namespace MandarinAuction.App.Services.Loggers.Auctions.AuctionLoggerService;

public interface IAuctionLoggerService
{
    public void LogAuctionCreated(Guid auctionId);
}