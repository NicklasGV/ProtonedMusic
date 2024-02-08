namespace ProtonedMusicAPI.Database.Entities
{
    public class ArtistLink
    {
        [Key]
        public int Id { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public int LinkId { get; set; }
        public Link Link { get; set; }
    }
}
