using AutoMapper;
using MandarinAuction.App.DTOs.Auctions;
using MandarinAuction.Domain.Events.Auctions.AuctionClose;
using MandarinAuction.Domain.Events.Auctions.AuctionCreated;
using MandarinAuction.Domain.Models;

namespace MandarinAuction.App.Mappings;

public class AuctionProfile : Profile
{
    public AuctionProfile()
    {
        CreateMap<Auction, AuctionDto>().ReverseMap();
        CreateMap<Auction, AuctionCreatedEventArgs>();
        CreateMap<Auction, AuctionCloseEventArgs>();
    }
}