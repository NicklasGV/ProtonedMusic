namespace ProtonedMusicAPI.Database.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; } 
        public string OrderNumber { get; set; }
        public List<ItemProduct> Items { get; set; }
       // public string Status { get; set; } // do not use yes, too sickness in the headness
        [ForeignKey("User.Id")]
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
