using ProtonedMusicAPI.DTO.IOrderHistoryDTO;

namespace ProtonedMusicAPI.Interfaces
{
    public interface IOrderHistoryService
    {
        Task<OrderHistoryResponse> GetOrdersByCustomerIdAsync(string customerId);
        Task<OrderHistoryResponse> GetOrderByIdAsync(int orderId);
        Task<OrderHistoryResponse> CreateOrderAsync(OrderHistoryRequest request);
    }
}
