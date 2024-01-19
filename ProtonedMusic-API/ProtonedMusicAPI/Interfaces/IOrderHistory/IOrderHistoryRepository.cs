using ProtonedMusicAPI.DTO.IOrderHistoryDTO;

namespace ProtonedMusicAPI.Interfaces.IOrderHistory
{
    public interface IOrderHistoryRepository
    {
        Task<List<Order>> GetOrdersByCustomerId(string customerId);
        Task<Order> GetOrdersById(int orderId);
        Task<Order> CreateOrder(int customerId, List<ItemProduct> items, string orderNumber);


    }
}
