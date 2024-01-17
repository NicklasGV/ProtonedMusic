using ProtonedMusicAPI.Database;

namespace ProtonedMusicAPI.Repositories
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        private readonly DatabaseContext _context;

        public OrderHistoryRepository(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Order> CreateOrder(Order newOrder)
        {
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            newOrder = await FindByIdAsync(newOrder.Id);
            return newOrder;
        }

        public async Task<Order> FindByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(a => a.Customer)
                .Include(a => a.Items)
                .FirstOrDefaultAsync(u => u.Id == orderId);
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
