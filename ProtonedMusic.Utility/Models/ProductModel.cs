namespace ProtonedMusic.Utility.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string ProductCategory { get; set; } = string.Empty;
        [Required]
        public int ProductPrice { get; set; } = 0;
        [Required]
        public string ProductDescription { get; set; } = string.Empty;
    }
}
