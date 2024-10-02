using MandarinAuction.App.Exceptions.Users.Auctions;
using MandarinAuction.App.Exceptions.Users.Auth;
using MandarinAuction.Domain.Models;

namespace MandarinAuction.App.Validators.Auctions.Bids;

public class AuctionBidValidator
{
    public static void ValidateUser(User? user)
    {
        if (user == null)
        {
            throw new UserNotFoundException();
        }
    }

    public static void ValidateAuction(Auction? auction)
    {
        if (auction == null)
        {
            throw new AuctionNotFoundException();
        }

        if (auction.IsClosed)
        {
            throw new AuctionIsClosedException();
        }
    }

    public static void ValidateBid(Auction auction, double bidSum, double minStepPercent)
    {
        if (bidSum <= auction.BidSum)
        {
            throw new BidMustBeMoreException();
        }

        if (!IsMoreMinStep(auction.BidSum, bidSum, minStepPercent))
        {
            throw new BidSumNextStepLessException(minStepPercent);
        }
    }

    private static bool IsMoreMinStep(double currentBidAmount, double nextBidAmount, double minStepPercent)
    {
        return nextBidAmount > currentBidAmount / 100 * minStepPercent + currentBidAmount;
    }
}