namespace ProtonedMusicAPI.DTO.IOrderHistoryDTO
{
    public class OrderHistoryRequest
    {
        public string CustomerId { get; set; }
        public List<int> ProductId { get; set; }
        public string OrderNumber { get; set; }
    }
}
