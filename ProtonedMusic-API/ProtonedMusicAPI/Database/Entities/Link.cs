namespace ProtonedMusicAPI.Database.Entities
{
    public class Link
    {
        [Key]
        public int Id { get; set; }

        public List<ArtistLink> Artist { get; set; } = new List<ArtistLink>();

        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(200)")]
        public string LinkAddress { get; set; } = string.Empty;
    }
}
