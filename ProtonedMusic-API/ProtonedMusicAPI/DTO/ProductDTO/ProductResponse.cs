namespace ProtonedMusicAPI.DTO.ProductDTO
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; } = 0;

        public string Description { get; set; } = string.Empty;
        public string? ProductPicturePath { get; set; }

        public List<ProductCategoryResponse> Categories { get; set; } = new();
        public bool IsDiscounted { get; set; } = false;
        public double DiscountProcent { get; set; } = 0;

    }

    public class ProductCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
