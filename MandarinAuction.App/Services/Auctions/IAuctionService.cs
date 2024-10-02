using MandarinAuction.App.DTOs.Auctions;

namespace MandarinAuction.App.Services.Auctions;

public interface IAuctionService
{
    public Task<IEnumerable<AuctionDto?>> GetAll(
        int pageIndex,
        int pageSize,
        bool sortAsc);

    public Task<int> Count();
}