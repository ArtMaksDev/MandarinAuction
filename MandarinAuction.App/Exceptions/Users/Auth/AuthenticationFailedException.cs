namespace MandarinAuction.App.Exceptions.Users.Auth;

public class AuthenticationFailedException : UserException
{
    public override string Message => "Неправильный логин или пароль.";
}