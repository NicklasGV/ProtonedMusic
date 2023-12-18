using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProtonedMusicAPI.Database.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(80)")]
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; } = 0;
        [Column(TypeName = "nvarchar(600)")]
        public string Description { get; set; } = string.Empty;
        public string? ProductPicturePath { get; set; }
        public List<ProductCategory> ProductCategories { get; set; } = new();
        public bool IsDiscounted { get; set; } = false;
        public double DiscountProcent { get; set; } = 0;
    }
}
