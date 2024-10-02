using AutoMapper;
using MandarinAuction.App.DTOs.Auctions;
using MandarinAuction.App.Exceptions.Users.Auctions;
using MandarinAuction.App.Exceptions.Users.Auth;
using MandarinAuction.App.Services.Loggers.Auctions.AuctionLoggerService;
using MandarinAuction.Domain.Models;
using MandarinAuction.Infrastructure;
using Microsoft.Extensions.Logging;

namespace MandarinAuction.App.Services.Auctions;

public class AuctionService : IAuctionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<AuctionLoggerService> _logger;

    public AuctionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AuctionLoggerService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<IEnumerable<AuctionDto?>> GetAll(
        int pageIndex,
        int pageSize,
        bool sortAsc)
    {
        return
            _mapper.Map<IEnumerable<Auction?>, IEnumerable<AuctionDto?>>(
                await _unitOfWork.AuctionRepository.GetAll(
                    pageIndex, pageSize, sortAsc));
    }

    public async Task<int> Count()
    {
        return await _unitOfWork.AuctionRepository.Count();
    }
}