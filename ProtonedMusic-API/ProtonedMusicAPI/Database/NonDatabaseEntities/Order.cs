namespace ProtonedMusicAPI.Database.NonDatabaseEntities
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string[] Items { get; set; }
        public string Status { get; set; }
    }
}
