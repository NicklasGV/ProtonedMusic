namespace ProtonedMusicAPI.Database.Entities
{
    public class ProductOrder
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
