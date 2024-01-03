namespace ProtonedMusicAPI.Interfaces
{
    public interface IOrderHistoryRepository
    {
        Task<Order> GetOrdersByCustomerId(string customerId);
        Task<Order> GetOrdersById(int orderId);
        Task<Order> CreateOrder(int customerId, List<ItemProduct> items, string orderNumber);
    }
}
