namespace ProtonedMusicAPI.DTO.Category
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<CategoryproductResponse> Products { get; set; } = new();
    }

    public class CategoryproductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
