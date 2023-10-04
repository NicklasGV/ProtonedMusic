using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProtonedMusicAPI.Database.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(80)")]
        public string Name { get; set; } = string.Empty;
        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
