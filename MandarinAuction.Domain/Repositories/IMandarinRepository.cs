#region usings

using MandarinAuction.Domain.Models;

#endregion

namespace MandarinAuction.Domain.Repositories;

public interface IMandarinRepository : IRepository<Mandarin>
{
    public Task<IEnumerable<Mandarin>> GetExpiredMandarins();
    public Task RemoveRange(IEnumerable<Mandarin> mandarins);

}