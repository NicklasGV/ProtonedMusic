namespace ProtonedMusicAPI.Interfaces.IOrderHistory
{
    public interface IOrderHistoryRepository
    {
        Task<List<Order>> GetOrdersByCustomerId(int customerId); // Ændring her
        Task<Order> GetOrdersById(int orderId);
        Task<Order> FindByIdAsync(int orderId);
        Task<Order> CreateOrder(Order order);
    }
}
