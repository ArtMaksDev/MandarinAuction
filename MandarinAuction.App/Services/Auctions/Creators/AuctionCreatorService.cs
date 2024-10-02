using AutoMapper;
using MandarinAuction.App.Services.Auctions.Generators;
using MandarinAuction.Domain.Events;
using MandarinAuction.Domain.Events.Auctions.AuctionCreated;
using MandarinAuction.Domain.Models;
using MandarinAuction.Infrastructure;

namespace MandarinAuction.App.Services.Auctions.Creators;

public class AuctionCreatorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuctionGenerator _auctionGenerator;
    private readonly IEventDispatcher<AuctionCreatedEventArgs> _auctionCreatedDispatcher;
    private readonly IMapper _mapper;

    public AuctionCreatorService(
        IUnitOfWork unitOfWork,
        IAuctionGenerator auctionGenerator,
        IEventDispatcher<AuctionCreatedEventArgs> auctionCreatedDispatcher,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _auctionGenerator = auctionGenerator;
        _auctionCreatedDispatcher = auctionCreatedDispatcher;
        _mapper = mapper;
    }

    public async Task Create()
    {
        var auction = await _auctionGenerator.Generate();

        await _unitOfWork.MandarinRepository.Add(auction.Mandarin);
        await _unitOfWork.AuctionRepository.Add(auction);
        await _unitOfWork.Complete();

        await _auctionCreatedDispatcher.Dispatch(
            _mapper.Map<Auction, AuctionCreatedEventArgs>(auction));

    }
}