namespace MandarinAuction.App.DTOs.Mandarins
{
    public class MandarinDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
