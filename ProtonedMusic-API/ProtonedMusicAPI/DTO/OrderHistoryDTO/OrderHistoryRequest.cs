namespace ProtonedMusicAPI.DTO.IOrderHistoryDTO
{
    public class OrderHistoryRequest
    {
        public string CustomerId { get; set; }
        public List<ItemProduct> Items { get; set; }
        public string OrderNumber { get; set; }
    }
}

