using MandarinAuction.App.DTOs.Auctions;

namespace MandarinAuction.App.Services.Auctions.Bids;

public interface IAuctionBidService
{
    public Task RaiseBid(Guid userId, RaiseBidDto raiseBidDto);
    public Task CloseBids(Guid auctionId, bool isPurchased, Guid? userId = null);
}