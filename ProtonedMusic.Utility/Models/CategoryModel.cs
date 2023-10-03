using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonedMusic.Utility.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(80)")]
        public string Name { get; set; } = string.Empty;
        public List<ProductCategoryModel> ProductCategories { get; set; } = new List<ProductCategoryModel>();
        public List<Categoryproduct> Products { get; set; } = new();
    }

    public class Categoryproduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "Decimal(8,2)")]
        public decimal Price { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
    }
}
