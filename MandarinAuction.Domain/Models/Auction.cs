namespace MandarinAuction.Domain.Models;

public class Auction
{
    public Guid Id { get; set; }
    public Mandarin Mandarin { get; set; } = null!;
    public Guid? HighestBidderId { get; set; }
    public User? HighestBidder { get; set; }
    public double BidSum { get; set; }
    public double BuySum { get; set; }
    public bool IsClosed { get; set; }
    public DateTime EndDate { get; set; }
}