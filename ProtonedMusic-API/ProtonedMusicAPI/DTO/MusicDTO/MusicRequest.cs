namespace ProtonedMusicAPI.DTO.MusicDTO
{
    public class MusicRequest
    {
        [StringLength(32, ErrorMessage = "First name cannot be longer than 32 chars")]
        public string SongName { get; set; }

        [StringLength(32, ErrorMessage = "Last name cannot be longer than 64 chars")]
        public string Artist { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Email cannot be longer than 50 chars")]
        public string Album { get; set; }

        public string? SongFilePath { get; set; }

        public string? SongPicturePath { get; set; }
    }
}
