using System.ComponentModel.DataAnnotations.Schema;

namespace ProtonedMusic.Utility.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "Decimal(8,2)")]
        public decimal ProductPrice { get; set; } = 0;
        [Required]
        public string ProductDescription { get; set; } = string.Empty;

        public List<ProductCategoryModel> ProductCategories { get; set; } = new();

        //public PicturesModel? picture { get; set; }
    }
}
