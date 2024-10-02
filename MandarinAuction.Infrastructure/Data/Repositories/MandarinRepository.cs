#region usings

using MandarinAuction.Domain.Models;
using MandarinAuction.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace MandarinAuction.Infrastructure.Data.Repositories;

public class MandarinRepository : IMandarinRepository
{
    private readonly MsDbContext _dbContext;

    public MandarinRepository(MsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Mandarin?> Get(Guid id)
    {
        return await _dbContext.Mandarins.FindAsync(id);
    }

    public async Task Add(Mandarin auction)
    {
        await _dbContext.Mandarins.AddAsync(auction);
    }

    public async Task Update(Mandarin auction)
    {
        _dbContext.Mandarins.Update(auction);

        await Task.CompletedTask;
    }

    public async Task<IEnumerable<Mandarin?>> GetAll()
    {
        return await _dbContext.Mandarins.ToArrayAsync();
    }
}