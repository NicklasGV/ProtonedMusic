namespace ProtonedMusicAPI.DTO.ImageDTO
{
    public class ImageRequest
    {
            
        [Required]
        [StringLength(50, ErrorMessage = "Filename cannot be longer than 50 characters")]
        public string FileName { get; set; }
        


    }
}
