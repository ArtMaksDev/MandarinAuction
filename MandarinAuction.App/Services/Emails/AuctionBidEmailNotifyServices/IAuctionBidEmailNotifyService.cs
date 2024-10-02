using MandarinAuction.Domain.Models;

namespace MandarinAuction.App.Services.Emails.AuctionBidEmailNotifyServices;

public interface IAuctionBidEmailNotifyService
{
    public Task SendBidRaised(string userEmail, string auctionName);
    public Task SendCheck(string userEmail, Auction auctionDto, bool isPurchased);
}