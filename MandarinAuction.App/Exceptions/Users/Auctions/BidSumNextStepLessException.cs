namespace MandarinAuction.App.Exceptions.Users.Auctions;

public class BidSumNextStepLessException : UserException
{
    private readonly double _nextStepPercent;

    public BidSumNextStepLessException(double nextStepPercent)
    {
        _nextStepPercent = nextStepPercent;
    }

    public override string Message => $"Сумма следующей ставки должна быть больше {_nextStepPercent}%, чем текущая.";
}