#region usings

using MandarinAuction.Domain.Repositories;

#endregion

namespace MandarinAuction.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    public IAuctionRepository AuctionRepository { get; }
    public IUserRepository UserRepository { get; }
    public IMandarinRepository MandarinRepository { get; }

    Task<int> Complete();
}