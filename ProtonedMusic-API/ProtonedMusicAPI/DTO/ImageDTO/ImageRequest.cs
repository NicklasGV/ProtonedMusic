namespace ProtonedMusicAPI.DTO.ImageDTO
{
    public class ImageRequest
    {
        [Required]
        [StringLength(20, ErrorMessage = "Product Name cannot be longer than 20 characters")]
        public string Name { get; set; }
            
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FilePatch { get; set; }
        


    }
}
