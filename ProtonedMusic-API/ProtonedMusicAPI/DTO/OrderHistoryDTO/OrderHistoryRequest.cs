namespace ProtonedMusicAPI.DTO.IOrderHistoryDTO
{
    public class OrderHistoryRequest
    {
        public int CustomerId { get; set; }
        public List<ProductOrderRequest> Products { get; set; } = new();
        public DateTime OrderDate { get; set; }
    }

    public class ProductOrderRequest
    {
        public int Id { get; set; }
        [ForeignKey("Order.Id")]
        public int OrderId { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 0;
    }
}

