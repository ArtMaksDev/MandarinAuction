using MandarinAuction.Domain.Models;
using MandarinAuction.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MandarinAuction.Infrastructure.Data.Repositories;

public class AuctionRepository : IAuctionRepository
{
    private const int AuctionPageIndexDefault = 1;
    private const int AuctionPageSizeDefault = 10;

    private readonly MsDbContext _dbContext;

    public AuctionRepository(MsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Auction?> Get(Guid id)
    {
        return await _dbContext.Auctions
            .Include(a => a.Mandarin)
            .Include(a => a.HighestBidder)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Add(Auction auction)
    {
        await _dbContext.Auctions.AddAsync(auction);
    }

    public async Task Update(Auction auction)
    {
        _dbContext.Auctions.Update(auction);

        await Task.CompletedTask;
    }

    public async Task<IEnumerable<Auction?>> GetAll()
    {
        return await GetAll(AuctionPageIndexDefault);
    }

    public async Task<IEnumerable<Auction?>> GetAll(
        int pageIndex,
        int pageSize = AuctionPageSizeDefault,
        bool sortAsc = true)
    {
        var query =
            _dbContext
                .Auctions
                .Include(a => a.Mandarin)
                .AsQueryable();

        if (pageSize > 0)
        {
            query = query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }

        return await query.ToArrayAsync();
    }

    public async Task<int> Count()
    {
        return await _dbContext.Auctions.CountAsync();
    }
}