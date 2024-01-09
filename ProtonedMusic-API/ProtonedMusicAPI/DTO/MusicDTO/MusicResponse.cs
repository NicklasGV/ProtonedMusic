namespace ProtonedMusicAPI.DTO.MusicDTO
{
    public class MusicResponse
    {
        public int Id { get; set; }
        public string SongName { get; set; }
        public List<MusicArtistResponse> Artist { get; set; }
        public string Album { get; set; }
        public string? SongFilePath { get; set; }
        public string? SongPicturePath { get; set; }
    }
    public class MusicArtistResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string? PicturePath { get; set; }
    }
}
