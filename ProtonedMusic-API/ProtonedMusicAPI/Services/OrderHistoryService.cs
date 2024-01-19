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
                Quantity = order.Quantity
            };
            if (order.ProductOrder.Count > 0)
            {
                response.Products = order.ProductOrder.Select(x => new ProductOrderResponse
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    IsDiscounted = x.Product.IsDiscounted,
                    DiscountProcent = x.Product.DiscountProcent,
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
                Quantity = orderRequest.Quantity,
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

        public async Task<OrderHistoryResponse?> FindByIdAsync(int customerId)
        {
            var customer = await _orderHistoryRepository.FindByIdAsync(customerId);

            if (customer != null)
            {
                return MapOrderToOrderResponse(customer);
            }
            return null;
        }
    }
}