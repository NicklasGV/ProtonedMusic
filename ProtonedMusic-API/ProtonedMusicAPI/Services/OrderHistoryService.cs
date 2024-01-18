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

        public async Task<OrderHistoryResponse> CreateOrderAsync(OrderHistoryRequest request)
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

        public async Task<List<OrderHistoryResponse>> GetOrdersByCustomerIdAsync(string customerId)
        {
            List<Order> customerOrders = await _orderHistoryRepository.GetOrdersByCustomerId(customerId);

            List<OrderHistoryResponse> responseList = new List<OrderHistoryResponse>();

            foreach (Order order in customerOrders)
            {
                OrderHistoryResponse response = MapOrderToOrderHistoryResponse(order);
                responseList.Add(response);
            }

            return responseList;
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
                response.Items = order.Items.Select(x => new OrderItemsResponse
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    OrderId = x.OrderId,
                    quantity = x.quantity,
                    ProductName = x.Product.Name,
                }).ToList();
            }

            return response;
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
