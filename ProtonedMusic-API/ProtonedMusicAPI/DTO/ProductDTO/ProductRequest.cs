namespace ProtonedMusicAPI.DTO.ProductDTO
{
    public class ProductRequest
    {
        [Required]
        [StringLength(80, ErrorMessage = "Product Name cannot be longer than 80 characters")]
        public string Name { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Price cannot be higher than 10000")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(600, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string Description { get; set; }
        public IFormFile? PictureFile { get; set; }
        public string? ProductPicturePath { get; set; }

        public List<int> CategoryIds { get; set; } = new();
    }
}
