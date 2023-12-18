namespace ProtonedMusicAPI.Database.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; } 
        public string OrderNumber { get; set; }
        public List<ItemProduct> Items { get; set; }
        [ForeignKey("User.Id")]
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
