using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProtonedMusicAPI.Database.Entities;

namespace ProtonedMusicAPI.Services
{
    public class OrderHistoryService : IOrderHistoryRepository
    {
        private readonly IOrderHistoryRepository _orderHistoryRepository;

        public OrderHistoryService(IOrderHistoryRepository orderHistoryRepository)
        {
            _orderHistoryRepository = orderHistoryRepository;
        }

        public async Task<Order> CreateOrder(int customerId, List<ItemProduct> items, string orderNumber)
        {
            Order newOrder = new Order
            {
                CustomerId = customerId,
                Items = items,
                OrderNumber = orderNumber,
                OrderDate = DateTime.Now,
            };
            return newOrder;
        }

        public async Task<Order> GetOrdersByCustomerId(int customerId)
        {
            Order customerOrders = await _orderHistoryRepository.GetOrdersByCustomerId(customerId);
            return customerOrders;
        }

        public async Task<Order> GetOrdersById(int orderId)
        {
            Order orderById = await _orderHistoryRepository.GetOrdersById(orderId);
            return orderById;
        }
    }
}
