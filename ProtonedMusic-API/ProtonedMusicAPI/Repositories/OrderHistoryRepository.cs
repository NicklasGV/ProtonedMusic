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
            if (string.IsNullOrWhiteSpace(orderNumber))
            {
                throw new ArgumentException("Invalid orderNumber");
            }

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

        public async Task<List<Order>> GetOrdersByCustomerId(string customerId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.CustomerId.ToString() == customerId)
                .ToListAsync();
        }

        public async Task<Order> GetOrdersById(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order> GetOrdersByPaymentId(string paymentId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.PaymentId == paymentId);
        }
    }
}
