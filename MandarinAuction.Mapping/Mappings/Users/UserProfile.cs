using AutoMapper;
using MandarinAuction.App.DTOs.Users;
using MandarinAuction.UIModels.ViewModels;

namespace MandarinAuction.UIModels.Mappings.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap< AuthViewModel, UserIdentityDto>();
        }
    }
}
