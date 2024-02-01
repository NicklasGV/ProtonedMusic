using ProtonedMusicAPI.Database.Entities;
using ProtonedMusicAPI.DTO.IOrderHistoryDTO;
using ProtonedMusicAPI.Interfaces.IOrderHistory;

namespace ProtonedMusicAPI.Services
{
    public class OrderHistoryService : IOrderHistoryService
    {
        private readonly IOrderHistoryRepository _orderHistoryRepository;

        public OrderHistoryService(IOrderHistoryRepository orderHistoryRepository)
        {
            _orderHistoryRepository = orderHistoryRepository;
        }

        private static OrderHistoryResponse MapOrderToOrderResponse(Order order)
        {
            OrderHistoryResponse response = new()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
            };
            if (order.ProductOrder.Capacity > 0)
            {
                response.Products = order.ProductOrder.Select(x => new ProductOrderResponse
                {
                    Id = x.ProductId,
                    Name = x.Name,
                    Price = x.Price,
                    IsDiscounted = x.IsDiscounted,
                    DiscountProcent = x.DiscountProcent,
                    Quantity = x.Quantity,
                }).ToList();
            }
            return response;
        }
        private static Order MapOrderRequestToOrder(OrderHistoryRequest orderRequest)
        {
            return new Order
            {
                CustomerId = orderRequest.CustomerId,
                OrderDate = orderRequest.OrderDate,
                ProductOrder = orderRequest.ProductIds.Select(c => new ProductOrder
                {
                    ProductId = c
                }).ToList(),
            };
        }

        public async Task<OrderHistoryResponse> CreateOrderAsync(OrderHistoryRequest newOrder)
        {
            var order = await _orderHistoryRepository.CreateOrder(MapOrderRequestToOrder(newOrder));

            if (order == null)
            {
                throw new ArgumentNullException();
            }
            return MapOrderToOrderResponse(order);
        }

        public async Task<List<OrderHistoryResponse?>> FindByIdAsync(int customerId)
        {
            List<Order> orders = await _orderHistoryRepository.FindByIdAsync(customerId);
            if (orders == null)
            {
                throw new ArgumentNullException();
            }
            return orders.Select(MapOrderToOrderResponse).ToList();
        }
    }
}