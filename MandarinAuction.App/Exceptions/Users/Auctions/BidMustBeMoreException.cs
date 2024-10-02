namespace MandarinAuction.App.Exceptions.Users.Auctions;

public class BidMustBeMoreException : UserException
{
    public override string Message => "Ставка должна быть выше текущей.";

}