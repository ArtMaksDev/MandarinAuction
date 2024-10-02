using MandarinAuction.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Common;
using MandarinAuction.App.Exceptions.Servers;

namespace MandarinAuction.App.Services.Mandarins.Generators;

public class MandarinGenerator : IMandarinGenerator
{
    private readonly Random _random;
    private readonly string _mandarinUnsplashUrl;
    private const int MinNameNumber = 1;
    private const int MaxNameNumber = 100;
    private const int DefaultExpirationDay = 7;
    private const string DefaultMandarinName = "Мандаринка";
    private const string DefaultDescription = "Lorem Ipsum - это текст-рыба, часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной рыбой для текстов на латинице с начала XVI века.";


    public MandarinGenerator(IConfiguration configuration, Random random)
    {
        _random = random;
        _mandarinUnsplashUrl = $"{configuration["Unsplash:Url"]}/random?query=mandarin-oranges&client_id={configuration["Unsplash:Key"]}";
    }
    public async Task<Mandarin> Generate()
    {
        return await Generate(DefaultMandarinName + $" N {_random.Next(MinNameNumber, MaxNameNumber)}");
    }

    public async Task<Mandarin> Generate(
        string? name,
        string? description = null,
        DateTime? expirationDate = null)
    {
        Argument.IsNotNullOrEmpty(name, nameof(name));

        var mandarin = new Mandarin
        {
            Id = Guid.NewGuid(),
            Name = name!,
            Description = description ?? DefaultDescription,
            ImageUrl = await GetRandomMandarinImageUrl(),
            ExpirationDate = expirationDate ?? DateTime.UtcNow.AddDays(DefaultExpirationDay),
        };

        return mandarin;
    }

    private async Task<string> GetRandomMandarinImageUrl()
    {
        var response = await new HttpClient().GetAsync(_mandarinUnsplashUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new ServerException();
        }

        var json = await response.Content.ReadAsStringAsync();
        var imageUrl =
            JsonDocument
            .Parse(json)
            .RootElement
            .GetProperty("urls")
            .GetProperty("regular").GetString();

        return imageUrl!;
    }
}