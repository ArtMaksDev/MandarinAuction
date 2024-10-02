#region usings

using System.ComponentModel.DataAnnotations;

#endregion

namespace MandarinAuction.UIModels.ViewModels;

public class AuthViewModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}