namespace MandarinAuction.App.Exceptions.Servers;

public class ServerException : Exception
{
    public override string Message => "Произошла ошибка сервера";
}