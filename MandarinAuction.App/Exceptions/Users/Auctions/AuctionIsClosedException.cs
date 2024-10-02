namespace MandarinAuction.App.Exceptions.Users.Auctions;

public class AuctionIsClosedException : UserException
{
    public override string Message => "Аукцион был закрыт.";
}