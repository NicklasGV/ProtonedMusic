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
        public async Task<List<Order?>> FindByIdAsync(int customerId)
        {
            return await _context.Orders
                .Include(p => p.ProductOrder)
                .Where(p => p.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Order> CreateOrder(Order newOrder)
        {
            _context.Orders.Add(newOrder);

            await _context.SaveChangesAsync();

            return newOrder;
        }


        public async Task<List<Order>> GetOrdersByCustomerId(int customerId)
        {
            return await _context.Orders
                .Include(o => o.ProductOrder)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
