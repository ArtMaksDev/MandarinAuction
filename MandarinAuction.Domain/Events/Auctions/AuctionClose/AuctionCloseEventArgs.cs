#region usings

using MandarinAuction.Domain.Models;

#endregion

namespace MandarinAuction.Domain.Events.Auctions.AuctionClose;

public class AuctionCloseEventArgs
{
    public bool IsPurchased { get; set; }
    public Auction Auction { get; set; } = null!;
}