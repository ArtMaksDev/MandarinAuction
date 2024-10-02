using MandarinAuction.Domain.Models;

namespace MandarinAuction.Domain.Events.Auctions.AuctionCreated;

public class AuctionCreatedEventArgs
{
    public Guid Id { get; set; }
    public Mandarin Mandarin { get; set; } = null!;
    public double BidSum { get; set; }
    public double BuySum { get; set; }
    public bool IsClosed { get; set; }
    public DateTime EndDate { get; set; }
}