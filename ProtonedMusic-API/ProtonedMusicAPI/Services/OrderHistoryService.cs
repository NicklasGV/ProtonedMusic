using ProtonedMusicAPI.Database.Entities;
using ProtonedMusicAPI.DTO.ArtistDTO;
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

        public async Task<OrderHistoryResponse> CreateOrderAsync(OrderHistoryRequest newOrder)
        {
            int customerId = Convert.ToInt32(request.CustomerId);
            Order newOrder = await _orderHistoryRepository.CreateOrder(customerId, request.Items, request.OrderNumber);

            OrderHistoryResponse response = MapOrderToOrderHistoryResponse(newOrder);

            return response;
        }

        public async Task<OrderHistoryResponse> GetOrderByIdAsync(int orderId)
        {
            Order orderById = await _orderHistoryRepository.GetOrdersById(orderId);

            OrderHistoryResponse response = MapOrderToOrderHistoryResponse(orderById);

            return response;
        }

        public async Task<OrderHistoryResponse> GetOrdersByCustomerIdAsync(int customerId)
        {
            List<Order> customerOrders = await _orderHistoryRepository.GetOrdersByCustomerId(customerId);

            List<OrderHistoryResponse> responseList = new List<OrderHistoryResponse>();

            foreach (Order order in customerOrders)
            {
                return MapOrderToOrderHistoryResponse(order);
            }

            return null;
        }
        public OrderHistoryResponse MapOrderToOrderHistoryResponse(Order order)
        {
            OrderHistoryResponse response = new OrderHistoryResponse
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                price = CalculateTotalPrice(order.Items),
                Quantity = CalculateTotalQuantity(order.Items),
            };

            if (order.Items.Count > 0)
            {
                response.Products = order.Items.Select(x => new OrderItemsResponse
                {
                    Id = x.product.Id,
                    ProductName = x.product.Name,
                    price = x.product.Price,
                }).ToList();
            }

            return response;
        }

        private static Order MapOrderRequestToOrder(OrderHistoryRequest request)
        {
            Order order = new Order
            {
                Id = request.CustomerId,
                OrderNumber = request.OrderNumber,
                OrderDate = request.OrderDate,
                Items = request.ProductId.Select(s => new ProductOrder
                {
                    ProductId = s
                }).ToList(),
                quantity = request.quantity,
            };

            return order;
        }

        public int CalculateTotalPrice(List<ItemProduct> items)
        {
            if (items == null || items.Count == 0)
            {
                return 0;
            }

            return (int)items.Sum(item => item.Product.Price * item.quantity);
        }
        
        public int CalculateTotalQuantity(List<ItemProduct> items)
        {
            if (items == null || items.Count == 0)
            {
                return 0;
            }

            return items.Sum(item => item.quantity);
        }
    }
}
