namespace ProtonedMusicAPI.Database.Entities
{
    public class ArtistSong
    {
        [Key]
        public int Id { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public int MusicId { get; set; }
        public Music Music { get; set; }
    }
}
