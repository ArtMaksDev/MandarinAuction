using MandarinAuction.App.Services.Mandarins.Generators;
using MandarinAuction.Domain.Models;

namespace MandarinAuction.App.Services.Auctions.Generators;

public class AuctionGenerator : IAuctionGenerator
{
    private readonly IMandarinGenerator _mandarinGenerator;
    private readonly Random _random;
    private const int MinBidSum = 100;
    private const int MaxBidSum = 3000;
    private const int EndHour = 24;

    public AuctionGenerator(IMandarinGenerator mandarinGenerator, Random random)
    {
        _mandarinGenerator = mandarinGenerator;
        _random = random;
    }
    public async Task<Auction> Generate()
    {

        var auction = new Auction
        {
            BidSum = _random.Next(MinBidSum, MaxBidSum),
            IsClosed = false,
            Mandarin = await _mandarinGenerator.Generate(),
            EndDate = DateTime.Now.AddHours(EndHour),
        };

        auction.BuySum = _random.Next((int)auction.BidSum, 10000);

        return auction;
    }
}