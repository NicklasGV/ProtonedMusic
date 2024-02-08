namespace ProtonedMusicAPI.DTO.ArtistDTO
{
    public class ArtistRequest
    {
        [StringLength(32, ErrorMessage = "Name cannot be longer than 32 chars")]
        public string Name { get; set; }

        public string Info { get; set; }

        public IFormFile? PictureFile { get; set; }
        public string? PicturePath { get; set; }

        public int UserId { get; set; } = new();
        public List<int> SongIds { get; set; } = new();
        public List<int> LinksIds { get; set; } = new();

    }
}
