#region usings

using MandarinAuction.Domain.Models;

#endregion

namespace MandarinAuction.Domain.Repositories;

public interface IAuctionRepository : IRepository<Auction>
{
    public Task<IEnumerable<Auction?>> GetAll(int pageIndex, int pageSize, bool sortAsc);

    public Task<int> Count();
}