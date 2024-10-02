namespace MandarinAuction.App.Exceptions.Users.Auth;

public class UserNotFoundException : Exception
{
    public override string Message => "Пользователь не был найден.";
}