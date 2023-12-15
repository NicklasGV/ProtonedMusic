using Microsoft.EntityFrameworkCore;
using ProtonedMusicAPI.DTO.IOrderHistoryDTO;

namespace ProtonedMusicAPI.Repositories
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        private readonly IOrderHistoryRepository _orderHistoryRepository;

        public OrderHistoryRepository(IOrderHistoryRepository orderHistoryRepository)
        {
            _orderHistoryRepository = orderHistoryRepository;
        }

        //Skal laves om
        public async Task<Order> CreateOrder(int customerId, List<ItemProduct> items, OrderHistoryResponse orderHistoryResponse)
        {
            return new Order
            {
                CustomerId = customerId,
                Items = items,
                OrderDate = DateTime.Now,
                OrderNumber = orderHistoryResponse.OrderNumber,
                
            };
        }

        //Laves om
        public Task<Order> CreateOrder(int customerId, List<ItemProduct> items, string orderNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrdersByCustomerId(int customerId)
        {
            return await _orderHistoryRepository.GetOrdersById(customerId);
        }

        public async Task<Order> GetOrdersById(int orderId)
        {
            return await _orderHistoryRepository.GetOrdersById(orderId);
        }
    }
}
