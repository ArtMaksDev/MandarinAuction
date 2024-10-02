namespace MandarinAuction.App.Exceptions.Users.Auctions;

public class AuctionNotFoundException : Exception
{
    public override string Message => "Аукцион не был найден.";
}