namespace ProtonedMusicAPI.Repositories
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        public Task<Order> CreateOrder(int customerId, List<ItemProduct> items, string orderNumber)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrdersByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrdersById(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
