namespace ProtonedMusicAPI.Database.Entities
{
    public class ProductOrder
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public bool IsDiscounted { get; set; } = false;
        public double DiscountProcent { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public int OrderId { get; set; }
    }
}
