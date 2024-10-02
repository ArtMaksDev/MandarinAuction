#region usings

using Hangfire;
using MandarinAuction.App.Services.Auctions.Bids;
using MandarinAuction.Domain.Events;
using MandarinAuction.Domain.Events.Auctions.AuctionCreated;
using Microsoft.Extensions.Logging;

#endregion

namespace MandarinAuction.App.Schedulers;

public class AuctionEndScheduler : IEventListener<AuctionCreatedEventArgs>
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly ILogger<AuctionEndScheduler> _logger;
    private readonly IAuctionBidService _auctionBidService;

    public AuctionEndScheduler(
        IBackgroundJobClient backgroundJobClient, 
        ILogger<AuctionEndScheduler> logger, 
        IAuctionBidService auctionBidService)
    {
        _backgroundJobClient = backgroundJobClient;
        _logger = logger;
        _auctionBidService = auctionBidService;
    }

    public async Task Handle(AuctionCreatedEventArgs auctionCreatedEvent)
    {
        _backgroundJobClient.Schedule(
            () => CloseBids(auctionCreatedEvent.Id), auctionCreatedEvent.EndDate);

        _logger.LogInformation("Заведена задача на закрытие аукциона {auctionCreatedEvent.Id} на дату: {auctionCreatedEvent.EndDate}", 
            auctionCreatedEvent.Id, 
            auctionCreatedEvent.EndDate);

        await Task.CompletedTask;
    }

    public async Task CloseBids(Guid auctionId)
    {
        await _auctionBidService.CloseBids(auctionId, false);

        _logger.LogInformation("Ставки для аукциона {auctionId} успешно закрыты.", auctionId);

    }
}