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
                    OrderId = x.OrderId,
                    Name = x.Name,
                    Price = x.Price,
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
            };
        }

        private static Order MapProductOrderRequestToOrder(int id , OrderHistoryRequest productOrderRequest)
        {
            return new Order
            {
                CustomerId = productOrderRequest.CustomerId,
                OrderDate = productOrderRequest.OrderDate,
                ProductOrder = productOrderRequest.Products.Select(c => new ProductOrder
                {
                    Id = c.Id,
                    OrderId = id,
                    Name = c.Name,
                    Price = c.Price,
                    Quantity = c.Quantity,
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

        public async Task<OrderHistoryResponse> UpdateProducts(int orderId, OrderHistoryRequest newProducts)
        {
            var order1 = MapProductOrderRequestToOrder(orderId, newProducts);
            Order order = await _orderHistoryRepository.UpdateProducts(orderId, order1);

            if (order != null)
            {
                return MapOrderToOrderResponse(order);
            }

            return null;
        }

        public async Task<List<OrderHistoryResponse?>> GetAllAsync(int customerId)
        {
            List<Order> orders = await _orderHistoryRepository.GetAllAsync(customerId);
            if (orders == null)
            {
                throw new ArgumentNullException();
            }
            return orders.Select(MapOrderToOrderResponse).ToList();
        }

        public Task<OrderHistoryResponse?> FindByIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }


    }
}