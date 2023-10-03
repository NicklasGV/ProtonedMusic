using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonedMusic.Repository.Database.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(80)")]
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; } = 0;
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; } = string.Empty;
        public List<ProductCategory> ProductCategories { get; set; } = new();
    }
}
