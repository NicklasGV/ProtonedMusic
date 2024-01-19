using Microsoft.EntityFrameworkCore;
using ProtonedMusicAPI.Database;
using ProtonedMusicAPI.Interfaces.IOrderHistory;

namespace ProtonedMusicAPI.Repositories
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        private readonly DatabaseContext _context;

        public OrderHistoryRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Order?> FindByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(p => p.ProductOrder)
                .ThenInclude(pc => pc.Product)
                .FirstOrDefaultAsync(p => p.Id == orderId);
        }

        public async Task<Order> CreateOrder(Order newOrder)
        {
            _context.Orders.Add(newOrder);

            await _context.SaveChangesAsync();
            newOrder = await FindByIdAsync(newOrder.Id);
            return newOrder;
        }


        public async Task<List<Order>> GetOrdersByCustomerId(int customerId)
        {
            return await _context.Orders
                .Include(o => o.ProductOrder)
                .ThenInclude(i => i.Product)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
