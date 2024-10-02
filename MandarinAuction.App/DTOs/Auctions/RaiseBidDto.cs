namespace MandarinAuction.App.DTOs.Auctions;

public class RaiseBidDto
{
    public Guid AuctionId { get; set; }
    public double BidSum { get; set; }
}