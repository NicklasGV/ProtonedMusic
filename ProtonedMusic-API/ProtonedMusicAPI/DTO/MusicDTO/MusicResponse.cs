namespace ProtonedMusicAPI.DTO.MusicDTO
{
    public class MusicResponse
    {
        public int Id { get; set; }
        public string SongName { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string? SongFilePath { get; set; }
        public string? SongPicturePath { get; set; }
    }
}
