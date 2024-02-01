using ProtonedMusicAPI.DTO.IOrderHistoryDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProtonedMusicAPI.Interfaces.IOrderHistory
{
    public interface IOrderHistoryService
    {
        Task<OrderHistoryResponse> CreateOrderAsync(OrderHistoryRequest request);
        Task<List<OrderHistoryResponse?>> GetAllAsync(int orderId);
        Task<OrderHistoryResponse?> FindByIdAsync(int customerId);
    }
}
    

