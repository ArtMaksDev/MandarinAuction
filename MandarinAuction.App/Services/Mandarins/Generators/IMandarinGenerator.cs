using MandarinAuction.Domain.Models;

namespace MandarinAuction.App.Services.Mandarins.Generators;

public interface IMandarinGenerator
{
    public Task<Mandarin> Generate();

}