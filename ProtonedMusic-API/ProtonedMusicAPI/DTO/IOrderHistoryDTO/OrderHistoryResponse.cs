namespace ProtonedMusicAPI.DTO.IOrderHistoryDTO
{
    public class OrderHistoryResponse
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemsResponse> Items { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }

    }

    public class OrderItemsResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int quantity { get; set; }
        public string ProductName { get; set; }
    }
}
