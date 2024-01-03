namespace ProtonedMusicAPI.Repositories
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        private readonly DatabaseContext _context;

        public OrderHistoryRepository(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return newOrder;
        }

        public async Task<Order> GetOrdersByCustomerId(string customerId)
        {
            // Returner alle ordrer for en bestemt kunde
            return await _context.Orders
                .Include(o => o.Items) // Inkluder relationen til Items i ordren
                .FirstOrDefaultAsync(o => o.CustomerId.ToString() == customerId);
        }

        public async Task<Order> GetOrdersById(int orderId)
        {
            // Returner en specifik ordre baseret på ID
            return await _context.Orders
                .Include(o => o.Items) // Inkluder relationen til Items i ordren
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}
