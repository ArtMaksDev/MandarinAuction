using MandarinAuction.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MandarinAuction.Infrastructure.Data;

public class MsDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Mandarin> Mandarins { get; set; } = null!;
    public DbSet<Auction> Auctions { get; set; } = null!;

    public MsDbContext(DbContextOptions<MsDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}