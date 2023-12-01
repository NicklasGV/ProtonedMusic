namespace ProtonedMusicAPI.Database.Entities
{
    public class Music
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string SongName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Artist { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Album { get; set; }

        public string? SongFilePath { get; set; }
        public string? SongPicturePath { get; set; }

    }
}
