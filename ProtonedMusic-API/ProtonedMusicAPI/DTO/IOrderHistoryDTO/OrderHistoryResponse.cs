namespace ProtonedMusicAPI.DTO.IOrderHistoryDTO
{
    public class OrderHistoryResponse
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        // public string OrderStatus { get; set; } 
        public List<ItemProduct> Items { get; set; }
    }
}
