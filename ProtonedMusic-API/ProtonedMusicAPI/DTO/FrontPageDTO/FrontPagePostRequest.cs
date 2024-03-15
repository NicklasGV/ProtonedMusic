namespace ProtonedMusicAPI.DTO.FrontpageDTO
{
    public class FrontpagePostRequest
    {
        [StringLength(600, ErrorMessage = "Text cannot be longer than 500 characters")]
        public string? Text { get; set; }
        public Banner? Banner { get; set; }
        public IFormFile? PictureFile { get; set; }
        public string? FrontpagePicturePath { get; set; }
    }
}
