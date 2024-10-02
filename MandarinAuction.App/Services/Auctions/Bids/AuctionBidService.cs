using AutoMapper;
using MandarinAuction.App.DTOs.Auctions;
using MandarinAuction.App.Exceptions.Users.Auctions;
using MandarinAuction.App.Services.Emails.AuctionBidEmailNotifyServices;
using MandarinAuction.App.Validators.Auctions.Bids;
using MandarinAuction.Domain.Events;
using MandarinAuction.Domain.Events.Auctions.AuctionClose;
using MandarinAuction.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace MandarinAuction.App.Services.Auctions.Bids;

public class AuctionBidService : IAuctionBidService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuctionBidEmailNotifyService _auctionBidEmailNotifyService;
    private readonly IEventDispatcher<AuctionCloseEventArgs> _auctionCloseDispatcher;
    private readonly double _minStepPercent;

    public AuctionBidService(
        IUnitOfWork unitOfWork,
        IAuctionBidEmailNotifyService auctionBidEmailNotifyService,
        IEventDispatcher<AuctionCloseEventArgs> auctionCloseDispatcher,
        IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _auctionBidEmailNotifyService = auctionBidEmailNotifyService;
        _auctionCloseDispatcher = auctionCloseDispatcher;
        _minStepPercent = double.Parse(configuration["Bids:MinStepPercent"]);
    }

    public async Task RaiseBid(Guid userId, RaiseBidDto raiseBidDto)
    {
        var user = await _unitOfWork.UserRepository.Get(userId);
        var auction = await _unitOfWork.AuctionRepository.Get(raiseBidDto.AuctionId);

        AuctionBidValidator.ValidateUser(user);
        AuctionBidValidator.ValidateAuction(auction);
        AuctionBidValidator.ValidateBid(auction!, raiseBidDto.BidSum, _minStepPercent);

        var prevBidder = auction!.HighestBidder;

        auction.BidSum = raiseBidDto.BidSum;
        auction.HighestBidder = user;
        auction.HighestBidderId = user!.Id;

        await _unitOfWork.Complete();

        if (prevBidder != null)
        {
            await _auctionBidEmailNotifyService.SendBidRaised(prevBidder.Email, auction.Mandarin.Name);
        }
    }

    /// <summary>
    /// Закрытие ставки.
    /// </summary>
    /// <param name="auctionId">Идентификатор аукциона.</param>
    /// <param name="isPurchased">Выкуплен ли.</param>
    /// <param name="userId">За кем закроются ставки.</param>
    /// <returns></returns>
    /// <exception cref="AuctionNotFoundException"></exception>
    public async Task CloseBids(Guid auctionId, bool isPurchased, Guid? userId = null)
    {
        var auction = await _unitOfWork.AuctionRepository.Get(auctionId);

        if (auction == null)
        {
            throw new AuctionNotFoundException();
        }

        if (auction.IsClosed)
        {
            return;
        }

        if (userId != null)
        {
            auction.HighestBidder = await _unitOfWork.UserRepository.Get(userId.Value);
        }

        auction.IsClosed = true;

        await _unitOfWork.Complete();

        await _auctionCloseDispatcher.Dispatch(new AuctionCloseEventArgs { Auction = auction, IsPurchased = isPurchased });
    }
}