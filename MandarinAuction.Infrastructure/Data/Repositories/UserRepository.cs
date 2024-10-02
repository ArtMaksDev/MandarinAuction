#region usings

using MandarinAuction.Domain.Models;
using MandarinAuction.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace MandarinAuction.Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MsDbContext _dbContext;

    public UserRepository(MsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> Get(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task Add(User auction)
    {
        await _dbContext.Users.AddAsync(auction);
    }

    public async Task Update(User auction)
    {
        _dbContext.Users.Update(auction);

        await Task.CompletedTask;
    }

    public async Task<IEnumerable<User?>> GetAll()
    {
        return await _dbContext.Users.ToArrayAsync();
    }

    public async Task<User?> FindByEmail(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}