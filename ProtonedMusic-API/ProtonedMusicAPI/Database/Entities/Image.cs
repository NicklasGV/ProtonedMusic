namespace ProtonedMusicAPI.Database.Entities
{
    public class Image
    {
        public int? ImageId { get; set; }
        public string? PublicId { get; set; } = string.Empty;
        public string? ImageName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
    }
}
