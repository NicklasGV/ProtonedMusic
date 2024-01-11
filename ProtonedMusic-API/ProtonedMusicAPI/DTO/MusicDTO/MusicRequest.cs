namespace ProtonedMusicAPI.DTO.MusicDTO
{
    public class MusicRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "Song name cannot be longer than 32 chars")]
        public string SongName { get; set; }

        [Required]
        public List<int> Artist { get; set; } = new();

        [Required]
        [StringLength(50, ErrorMessage = "Album cannot be longer than 50 chars")]
        public string Album { get; set; }

        public IFormFile? SongFile { get; set; }
        public string? SongFilePath { get; set; }

        public IFormFile? PictureFile { get; set; }
        public string? SongPicturePath { get; set; }
    }
}
