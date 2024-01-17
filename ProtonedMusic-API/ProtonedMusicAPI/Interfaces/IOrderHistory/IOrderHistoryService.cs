using ProtonedMusicAPI.DTO.IOrderHistoryDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProtonedMusicAPI.Interfaces.IOrderHistory
{
    public interface IOrderHistoryService
    {
        Task<OrderHistoryResponse> GetOrdersByCustomerIdAsync(int customerId);
        Task<OrderHistoryResponse> GetOrderByIdAsync(int orderId);
        Task<OrderHistoryResponse> CreateOrderAsync(OrderHistoryRequest request);
    }
}
