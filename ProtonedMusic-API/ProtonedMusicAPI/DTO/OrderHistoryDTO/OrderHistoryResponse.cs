namespace ProtonedMusicAPI.DTO.IOrderHistoryDTO
{
    public class OrderHistoryResponse
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<ProductOrderResponse> Products { get; set; } = new();
    }

    public class ProductOrderResponse
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
