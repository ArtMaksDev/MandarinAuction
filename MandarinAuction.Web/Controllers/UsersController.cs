#region usings

using AutoMapper;
using MandarinAuction.App.DTOs.Users;
using MandarinAuction.App.Services.Users.Creator;
using MandarinAuction.App.Services.Users.JwtToken;
using MandarinAuction.App.Services.Users.SignIn;
using MandarinAuction.App.Validators;
using MandarinAuction.UIModels.ViewModels;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace MandarinAuction.Web.Controllers;

public class UsersController : Controller
{
    private readonly SignInService _signInService;
    private readonly IUserCreatorService<UserIdentityDto> _userCreator;
    private readonly JwtTokenService _tokenService;
    private readonly IValidator<UserIdentityDto> _userIdentityValidator;
    private readonly IMapper _mapper;

    public UsersController(
        SignInService signInService,
        IUserCreatorService<UserIdentityDto> userCreator,
        JwtTokenService tokenService,
        IValidator<UserIdentityDto> userIdentityValidator,
        IMapper mapper
    )
    {
        _signInService = signInService;
        _userCreator = userCreator;
        _tokenService = tokenService;
        _userIdentityValidator = userIdentityValidator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody]AuthViewModel authViewModel, [FromQuery] string? returnUrl)
    {
        var userIdentity = _mapper.Map<UserIdentityDto>(authViewModel);

        _userIdentityValidator.Validate(userIdentity);

        return
            AuthorizeAndRedirect(
                userIdentity,
                await _signInService.Verify(userIdentity),
                returnUrl);
    }

    [HttpPost]
    public async Task<IActionResult> Registration([FromBody]AuthViewModel authViewModel, [FromQuery] string? returnUrl)
    {
        var userIdentity = _mapper.Map<UserIdentityDto>(authViewModel);

        _userIdentityValidator.Validate(userIdentity);

        return
            AuthorizeAndRedirect(
                userIdentity,
                await _userCreator.Create(userIdentity),
                returnUrl);
    }

    private IActionResult AuthorizeAndRedirect(UserIdentityDto userDto, Guid userId, string? returnUrl)
    {
        _tokenService.SaveCookie(
            HttpContext,
            _tokenService.GenerateToken(userDto, userId)
        );

        return RedirectToLocalUrl(returnUrl);
    }

    private IActionResult RedirectToLocalUrl(string? returnUrl)
    {
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return Redirect("/");
    }
}