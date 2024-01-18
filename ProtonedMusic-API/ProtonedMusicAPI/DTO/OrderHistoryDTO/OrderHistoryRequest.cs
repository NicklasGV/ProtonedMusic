namespace ProtonedMusicAPI.DTO.IOrderHistoryDTO
{
    public class OrderHistoryRequest
    {
        public int CustomerId { get; set; }
        public List<int> ProductId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int quantity { get; set; }
    }
}
