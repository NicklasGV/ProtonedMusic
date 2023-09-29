namespace ProtonedMusic.Utility.Models
{
    public class ProductModel
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string ProductCategory { get; set; } = string.Empty;
        [Required]
        public int ProductPrice { get; set; } = 0;
        [Required]
        public string ProductDescription { get; set; } = string.Empty;
        //public PicturesModel? picture { get; set; }
    }
}
