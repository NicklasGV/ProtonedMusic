namespace ProtonedMusicAPI.Database.NonDatabaseEntities
{
    public class CartItemData
    {
        public string Name { get; set; } = string.Empty;
        public int UnitAmount { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
