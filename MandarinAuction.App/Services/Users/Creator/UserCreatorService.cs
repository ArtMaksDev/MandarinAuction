#region usings

using Common;
using MandarinAuction.App.DTOs.Users;
using MandarinAuction.Domain.Models;
using MandarinAuction.Infrastructure;
using Microsoft.AspNetCore.Identity;

#endregion

namespace MandarinAuction.App.Services.Users.Creator;

public class UserCreatorService : IUserCreatorService<UserIdentityDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserCreatorService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Create(UserIdentityDto data)
    {
        Argument.IsNotNull(data, nameof(data));

        var id = Guid.NewGuid();

        var user = new User
        {
            Email = data.Email,
            Id = id
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, data.Password);

        await _unitOfWork.UserRepository.Add(user);
        await _unitOfWork.Complete();

        return id;
    }
}