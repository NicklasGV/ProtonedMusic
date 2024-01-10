namespace ProtonedMusicAPI.DTO.IOrderHistoryDTO
{
    public class OrderHistoryResponse
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }

        public List<ItemProduct> Items { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }

    }
}
