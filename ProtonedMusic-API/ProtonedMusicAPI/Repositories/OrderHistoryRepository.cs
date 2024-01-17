using ProtonedMusicAPI.Database;
using ProtonedMusicAPI.Interfaces.IOrderHistory;

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

        public async Task<List<Order>> GetOrdersByCustomerId(int customerId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Order> GetOrdersById(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}
