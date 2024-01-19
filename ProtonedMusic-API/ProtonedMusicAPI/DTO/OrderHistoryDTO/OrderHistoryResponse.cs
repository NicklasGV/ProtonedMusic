namespace ProtonedMusicAPI.DTO.IOrderHistoryDTO
{
    public class OrderHistoryResponse
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<ProductOrderResponse> Products { get; set; } = new();
        public int Quantity { get; set; } = 0;
    }

    public class ProductOrderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsDiscounted { get; set; } = false;
        public double DiscountProcent { get; set; } = 0;
    }
}
