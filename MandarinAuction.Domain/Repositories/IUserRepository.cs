#region usings

using MandarinAuction.Domain.Models;

#endregion

namespace MandarinAuction.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> FindByEmail(string email);
}