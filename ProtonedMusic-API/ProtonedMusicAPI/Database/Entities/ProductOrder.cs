namespace ProtonedMusicAPI.Database.Entities
{
    public class ProductOrder
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Order.Id")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
