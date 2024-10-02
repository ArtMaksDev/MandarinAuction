#region usings

using MandarinAuction.App.DTOs.Auctions;

#endregion

namespace MandarinAuction.UIModels.ViewModels;

public class AuctionsViewModel : AuthViewModel
{
    public IEnumerable<AuctionDto?> Auctions { get; set; } = null!;
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}