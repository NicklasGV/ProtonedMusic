namespace ProtonedMusicAPI.Database.NonDatabaseEntities
{
    public class CheckoutRequest
    {
        public List<CartItemData> CartItems { get; set; }
        public string CustomerEmail { get; set; }
    }
}
