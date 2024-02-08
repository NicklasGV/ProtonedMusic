using Microsoft.EntityFrameworkCore;
using ProtonedMusicAPI.Database;
using ProtonedMusicAPI.Database.Entities;
using ProtonedMusicAPI.Interfaces.IOrderHistory;
using Stripe;
using System.Collections.Generic;

namespace ProtonedMusicAPI.Repositories
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        private readonly DatabaseContext _context;

        public OrderHistoryRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Order?>> GetAllAsync(int customerId)
        {
            return await _context.Orders
                .Include(p => p.ProductOrder)
                .Where(p => p.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Order> FindByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(p => p.ProductOrder)
                .FirstOrDefaultAsync(p => p.Id == orderId);
        }

        public async Task<Order> UpdateProducts(int orderId, Order updateOrder)
        {
            Order order = await FindByIdAsync(orderId);
            if (order != null)
            {
                List<ProductOrder> ProductOrders = updateOrder.ProductOrder.Select(i => new ProductOrder { OrderId = orderId, Name = i.Name, Price = i.Price, Quantity = i.Quantity }).ToList();
                _context.ProductOrders.AddRange(ProductOrders);

                await _context.SaveChangesAsync();

                order = await FindByIdAsync(orderId);
            }
            return order;
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
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
