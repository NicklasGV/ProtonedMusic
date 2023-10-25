namespace ProtonedMusicAPI.DTO.ImageDTO
{
    public class ImageRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "ImageFile is required.")]
        public IFormFile ImageFile { get; set; }
    }


}
