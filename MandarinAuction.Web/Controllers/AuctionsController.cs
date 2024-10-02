#region usings

using System.Security.Claims;
using MandarinAuction.App.DTOs.Auctions;
using MandarinAuction.App.Services.Auctions;
using MandarinAuction.App.Services.Auctions.Bids;
using MandarinAuction.UIModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace MandarinAuction.Web.Controllers;

public class AuctionsController : Controller
{
    private const int AuctionPageIndexDefault = 1;
    private const int AuctionPageSizeDefault = 9;

    private readonly IAuctionService _auctionService;
    private readonly IAuctionBidService _auctionBidService;

    public AuctionsController(IAuctionService auctionService, IAuctionBidService auctionBidService)
    {
        _auctionService = auctionService;
        _auctionBidService = auctionBidService;
    }

    public async Task<IActionResult> Index(
        int pageIndex = AuctionPageIndexDefault,
        int pageSize = AuctionPageSizeDefault,
        bool sortAsc = true)
    {
        return View(new AuctionsViewModel
        {
            Auctions = await _auctionService.GetAll(pageIndex, pageSize, sortAsc),
            CurrentPage = pageIndex,
            TotalPages = (int)Math.Ceiling(await _auctionService.Count() / (double)pageSize)
        });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> RaiseBid([FromBody] RaiseBidDto raiseBidDto)
    {
        await _auctionBidService.RaiseBid(
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
            raiseBidDto);

        return RedirectToAction("Index");
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Buy([FromBody] BuyDto buyDto)
    {
        await _auctionBidService.CloseBids(buyDto.AuctionId, true);

        return RedirectToAction("Index");
    }
}