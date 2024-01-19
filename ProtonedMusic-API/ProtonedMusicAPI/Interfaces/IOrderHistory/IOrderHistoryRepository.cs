using ProtonedMusicAPI.DTO.IOrderHistoryDTO;

namespace ProtonedMusicAPI.Interfaces.IOrderHistory
{
    public interface IOrderHistoryRepository
    {
        Task<Order> FindByIdAsync(int customerId);
        Task<Order> CreateOrder(Order newOrder);
    }
}
