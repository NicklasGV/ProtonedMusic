namespace ProtonedMusicAPI.Database.Entities
{
    public class Artist
    {
        [ForeignKey("User.Id")]
        public int Id { get; set; }
        public User User { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Info { get; set; }

        public string? PicturePath { get; set; }

        

        public List<ArtistSong> Songs { get; set; } = new List<ArtistSong>();

        public List<Link> Links { get; set; } = new List<Link>();

    }
}
