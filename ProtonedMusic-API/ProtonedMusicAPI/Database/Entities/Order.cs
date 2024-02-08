namespace ProtonedMusicAPI.Database.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User.Id")]
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        public List<ProductOrder> ProductOrder { get; set; } = new();
        public DateTime OrderDate { get; set; }
    }
}
