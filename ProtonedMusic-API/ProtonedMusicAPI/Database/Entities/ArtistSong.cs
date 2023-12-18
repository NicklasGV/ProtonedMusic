namespace ProtonedMusicAPI.Database.Entities
{
    public class ArtistSong
    {
        [Key]
        public int Id { get; set; }

        public int Artist_Id { get; set; }
        public Artist Artist { get; set; }

        public int music_Id { get; set; }
        public Music Music { get; set; }
    }
}
