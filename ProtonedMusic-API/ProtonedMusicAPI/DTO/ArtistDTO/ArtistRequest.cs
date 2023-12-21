namespace ProtonedMusicAPI.DTO.ArtistDTO
{
    public class ArtistRequest
    {
        [StringLength(32, ErrorMessage = "Name cannot be longer than 32 chars")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Info line one cannot be longer than 50 chars")]
        public string Info { get; set; }

        public IFormFile? PictureFile { get; set; }
        public string? PicturePath { get; set; }

        public int User { get; set; } = new();
        public List<int> SongIds { get; set; } = new();
        public List<ArtistLinksRequest> Links { get; set; } = new();

    }

    public class ArtistLinksRequest
    {
        public int ArtistId { get; set; }
        public string Title { get; set; }
        public string LinkAddress { get; set; }
    }
}
