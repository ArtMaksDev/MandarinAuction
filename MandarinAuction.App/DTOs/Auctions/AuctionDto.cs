using MandarinAuction.App.DTOs.Mandarins;

namespace MandarinAuction.App.DTOs.Auctions;

public class AuctionDto
{
    public Guid Id { get; set; }
    public Guid MandarinId { get; set; }
    public MandarinDto Mandarin { get; set; } = null!;
    public double BidSum { get; set; }
    public double BuySum { get; set; }
    public bool IsClosed { get; set; }
}