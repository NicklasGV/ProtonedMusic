namespace ProtonedMusicAPI.DTO.IOrderHistoryDTO
{
    public class OrderHistoryRequest
    {
        public int CustomerId { get; set; }
        public List<ProductOrder> Products { get; set; } = new();
        public DateTime OrderDate { get; set; }
    }
}

