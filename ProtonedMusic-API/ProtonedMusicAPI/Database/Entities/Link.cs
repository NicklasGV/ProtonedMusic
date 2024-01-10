namespace ProtonedMusicAPI.Database.Entities
{
    public class Link
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Artist.Id")]
        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(200)")]
        public string LinkAddress { get; set; } = string.Empty;
    }
}
