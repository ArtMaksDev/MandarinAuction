#region usings

using Common;
using MandarinAuction.App.DTOs.Users;
using MandarinAuction.App.Exceptions.Users.Auth;
using MandarinAuction.Domain.Models;
using MandarinAuction.Infrastructure;
using Microsoft.AspNetCore.Identity;

#endregion

namespace MandarinAuction.App.Services.Users.SignIn;

public class SignInService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher;

    public SignInService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Verify(UserIdentityDto userDto)
    {
        Argument.IsNotNullOrEmpty(userDto.Email, nameof(userDto.Email));
        Argument.IsNotNullOrEmpty(userDto.Password, nameof(userDto.Password));

        var user = await _unitOfWork.UserRepository.FindByEmail(userDto.Email);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userDto.Password);

        if (result is PasswordVerificationResult.Failed)
        {
            throw new AuthenticationFailedException();
        }

        return user.Id;
    }
}