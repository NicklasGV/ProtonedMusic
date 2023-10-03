using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonedMusic.Utility.DTO
{
    public class ProductRequest
    {
        [Required]
        [StringLength(80, ErrorMessage = "Product Name cannot be longer than 80 characters")]
        public string Name { get; set; }

        [Required]
        [Range(1, 2000, ErrorMessage = "Price cannot be higher than 2000")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string Description { get; set; }

        public List<int> CategoryIds { get; set; } = new();
    }
}
