namespace MandarinAuction.Domain.Models;

public class Mandarin
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public DateTime ExpirationDate { get; set; }
}