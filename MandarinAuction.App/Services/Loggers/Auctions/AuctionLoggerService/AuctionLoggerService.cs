#region usings

using MandarinAuction.Domain.Events;
using MandarinAuction.Domain.Events.Auctions.AuctionCreated;
using Microsoft.Extensions.Logging;

#endregion

namespace MandarinAuction.App.Services.Loggers.Auctions.AuctionLoggerService;

public class AuctionLoggerService : IAuctionLoggerService, IEventListener<AuctionCreatedEventArgs>
{
    private readonly ILogger<AuctionLoggerService> _logger;

    public AuctionLoggerService(ILogger<AuctionLoggerService> logger)
    {
        _logger = logger;
    }

    public void LogAuctionCreated(Guid auctionId)
    {
        _logger.LogInformation("Аукцион с ID {auctionId} был создан успешно.", auctionId);
    }

    public Task Handle(AuctionCreatedEventArgs auctionCreatedEvent)
    {
        LogAuctionCreated(auctionCreatedEvent.Id);

        return Task.CompletedTask;
    }
}