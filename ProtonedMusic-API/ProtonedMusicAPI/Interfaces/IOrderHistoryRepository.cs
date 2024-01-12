namespace ProtonedMusicAPI.Interfaces
{
    public interface IOrderHistoryRepository
    {
        Task<List<Order>> GetOrdersByCustomerId(string customerId); // Ændring her
        Task<Order> GetOrdersById(int orderId);
        Task<Order> GetOrdersByPaymentId(string paymentId); // Tilføj denne metode
        Task<Order> CreateOrder(int customerId, List<ItemProduct> items, string orderNumber);
    }
}
