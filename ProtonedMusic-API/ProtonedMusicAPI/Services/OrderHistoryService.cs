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
            _orderHistoryRepository = orderHistoryRepository ?? throw new ArgumentNullException(nameof(orderHistoryRepository));
        }

        public async Task<OrderHistoryResponse> CreateOrderAsync(OrderHistoryRequest newOrder)
        {
            var order = await _orderHistoryRepository.CreateOrder(MapOrderRequestToOrder(newOrder));
            if (order == null)
            {
                throw new ArgumentNullException();
            }
            return MapOrderToOrderHistoryResponse(order);
        }

        public async Task<OrderHistoryResponse> GetOrderByIdAsync(int orderId)
        {
            // Hent en bestemt ordre fra databasen
            Order orderById = await _orderHistoryRepository.GetOrdersById(orderId);

            // Lav en respons baseret på den hentede ordre
            OrderHistoryResponse response = MapOrderToOrderHistoryResponse(orderById);

            return response;
        }

        public async Task<OrderHistoryResponse> GetOrdersByCustomerIdAsync(int customerId)
        {
            var order = await _orderHistoryRepository.FindByIdAsync(customerId);

            if (order != null)
            {
                return MapOrderToOrderHistoryResponse(order);
            }

            return null;
        }


        // Hjælpefunktion til at mappe en Order til en OrderHistoryResponse
        public OrderHistoryResponse MapOrderToOrderHistoryResponse(Order order)
        {
            OrderHistoryResponse response = new OrderHistoryResponse
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                quantity = order.quantity
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

        // Hjælpefunktion til at beregne den samlede pris for alle elementer i en ordre
        public int CalculateTotalPrice(List<ItemProduct> items)
        {
            if (items == null || items.Count == 0)
            {
                return 0;
            }

            return (int)items.Sum(item => item.Product.Price * item.quantity);
        }

        // Hjælpefunktion til at beregne den samlede mængde af alle elementer i en ordre
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
