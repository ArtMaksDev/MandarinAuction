using MandarinAuction.Domain.Models;

namespace MandarinAuction.App.Services.Auctions.Generators;

public interface IAuctionGenerator
{
    public Task<Auction> Generate();
}