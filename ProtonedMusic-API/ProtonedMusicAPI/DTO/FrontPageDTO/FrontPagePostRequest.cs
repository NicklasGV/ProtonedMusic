namespace ProtonedMusicAPI.DTO.FrontpageDTO
{
    public class FrontpagePostRequest
    {
        [Required]
        [StringLength(600, ErrorMessage = "Text cannot be longer than 500 characters")]
        public string Text { get; set; }
        [Required]
        public Banner Banner { get; set; }
        public string? FrontpagePicturePath { get; set; }
    }
}
