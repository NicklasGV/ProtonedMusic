using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonedMusic.Utility.DTO
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        public List<ProductCategoryResponse> Categories { get; set; } = new();

    }

    public class ProductCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
