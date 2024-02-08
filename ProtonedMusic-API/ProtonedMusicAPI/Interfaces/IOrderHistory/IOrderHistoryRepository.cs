using ProtonedMusicAPI.DTO.IOrderHistoryDTO;

namespace ProtonedMusicAPI.Interfaces.IOrderHistory
{
    public interface IOrderHistoryRepository
    {
        Task<List<Order>> GetAllAsync(int customerId);
        Task<Order> FindByIdAsync(int orderId);
        Task<Order> CreateOrder(Order newOrder);
        Task<Order> UpdateProducts(int orderId, Order newProducts);
    }
}
