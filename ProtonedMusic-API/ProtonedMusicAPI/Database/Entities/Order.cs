namespace ProtonedMusicAPI.Database.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public List<ProductOrder> Items { get; set; }
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public int quantity { get; set; }
    }
}
