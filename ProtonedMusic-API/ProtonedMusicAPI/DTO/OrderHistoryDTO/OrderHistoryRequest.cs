namespace ProtonedMusicAPI.DTO.IOrderHistoryDTO
{
    public class OrderHistoryRequest
    {
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; } = new();
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
    }
}

