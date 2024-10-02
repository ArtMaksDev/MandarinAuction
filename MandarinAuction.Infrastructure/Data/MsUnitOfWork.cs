#region usings

using MandarinAuction.Domain.Repositories;

#endregion

namespace MandarinAuction.Infrastructure.Data;

public class MsUnitOfWork : IUnitOfWork
{
    public IAuctionRepository AuctionRepository { get; }
    public IMandarinRepository MandarinRepository { get; }
    public IUserRepository UserRepository { get; }

    private readonly MsDbContext _msDbContext;

    public MsUnitOfWork(MsDbContext msDbContext, IAuctionRepository auctionRepository,
        IMandarinRepository mandarinRepository, IUserRepository userRepository)
    {
        AuctionRepository = auctionRepository;
        MandarinRepository = mandarinRepository;
        UserRepository = userRepository;
        _msDbContext = msDbContext;
    }

    public async Task<int> Complete()
    {
        return await _msDbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _msDbContext.Dispose();
    }
}