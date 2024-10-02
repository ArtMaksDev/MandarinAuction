using Common;
using System;
using System.Text.RegularExpressions;
using MandarinAuction.App.Exceptions.Users.Auth;
using MandarinAuction.App.DTOs.Users;

namespace MandarinAuction.App.Validators.Users;

public class UserIdentityValidator : IValidator<UserIdentityDto>
{
    private const int MinPasswordLength = 6;
    const string EmailPattern = @"^(([^<>()[\].,;:\s@""\\]+(\.[^<>()[\].,;:\s@""]+)*)|("".+""))@(([^<>()[\].,;:\s@""\\]+\.)+[^<>()[\].,;:\s@""]{2,})$";
    readonly string _passwordPattern = $"^.{{{MinPasswordLength},}}$";

    public void Validate(UserIdentityDto userIdentity)
    {
        Argument.IsNotNull(userIdentity, nameof(userIdentity));


        if (!Regex.IsMatch(userIdentity.Email, EmailPattern, RegexOptions.IgnoreCase))
        {
            throw new AuthenticationFailedException();
        }

        if (!Regex.IsMatch(userIdentity.Password, _passwordPattern))
        {
            throw new AuthenticationFailedException();
        }
    }
}