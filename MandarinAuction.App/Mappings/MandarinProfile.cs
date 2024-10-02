using AutoMapper;
using MandarinAuction.App.DTOs.Mandarins;
using MandarinAuction.Domain.Models;

namespace MandarinAuction.App.Mappings;

public class MandarinProfile : Profile
{
    public MandarinProfile()
    {
        CreateMap<Mandarin, MandarinDto>();
    }
}