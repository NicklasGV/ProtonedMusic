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

        public async Task<Order> CreateOrder(int customerId, List<ItemProduct> items, string orderNumber)
        {
            Order newOrder = new Order
            {
                CustomerId = customerId,
                OrderNumber = orderNumber,
                OrderDate = DateTime.Now,  
                
            };

            foreach (var item in items)
            {
                ItemProduct orderItem = new ItemProduct
                {
                    ProductId = item.ProductId,
                    quantity = item.quantity,
                    OrderId = item.OrderId,
                };

                newOrder.Items.Add(orderItem);
            }

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
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

    }
}
