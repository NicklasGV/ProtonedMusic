using ProtonedMusicAPI.DTO.IOrderHistoryDTO;

namespace ProtonedMusicAPI.Services
{
    public class OrderHistoryService : IOrderHistoryService
    {
        private readonly IOrderHistoryRepository _orderHistoryRepository;

        public OrderHistoryService(IOrderHistoryRepository orderHistoryRepository)
        {
            _orderHistoryRepository = orderHistoryRepository ?? throw new ArgumentNullException(nameof(orderHistoryRepository));
        }

        public async Task<OrderHistoryResponse> CreateOrderAsync(OrderHistoryRequest request)
        {
            // Opret en ny ordre og gem den i databasen
            int customerId = Convert.ToInt32(request.CustomerId); // Konverter customerId fra string til int
            Order newOrder = await _orderHistoryRepository.CreateOrder(customerId, request.Items, request.OrderNumber);

            // Lav en respons baseret på den oprettede ordre
            OrderHistoryResponse response = MapOrderToOrderHistoryResponse(newOrder);

            return response;
        }

        public async Task<OrderHistoryResponse> GetOrderByIdAsync(int orderId)
        {
            // Hent en bestemt ordre fra databasen
            Order orderById = await _orderHistoryRepository.GetOrdersById(orderId);

            // Lav en respons baseret på den hentede ordre
            OrderHistoryResponse response = MapOrderToOrderHistoryResponse(orderById);

            return response;
        }

        public async Task<OrderHistoryResponse> GetOrdersByCustomerIdAsync(string customerId)
        {
            // Hent alle ordrer for en bestemt kunde fra databasen
            Order customerOrders = await _orderHistoryRepository.GetOrdersByCustomerId(customerId);

            // Lav en respons baseret på de hentede ordrer
            OrderHistoryResponse response = MapOrderToOrderHistoryResponse(customerOrders);

            return response;
        }


        // Hjælpefunktion til at mappe en Order til en OrderHistoryResponse
        public OrderHistoryResponse MapOrderToOrderHistoryResponse(Order order)
        {
            if (order == null)
            {
                return null;
            }

            return new OrderHistoryResponse
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                Items = order.Items, // Dette kan også kræve en separat mapping af ItemProduct til ItemProductResponse
                price = CalculateTotalPrice(order.Items),
                quantity = CalculateTotalQuantity(order.Items)
            };
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
