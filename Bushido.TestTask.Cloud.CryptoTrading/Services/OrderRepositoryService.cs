using Bushido.TestTask.Cloud.CryptoTrading.Interfaces;
using Bushido.TestTask.Cloud.CryptoTrading.Models;
using Microsoft.EntityFrameworkCore;

namespace Bushido.TestTask.Cloud.CryptoTrading.Services
{
    public class OrderRepositoryService : IOrderRepositoryService
    {
        private readonly CryptoTradingDbContext _context;

        public OrderRepositoryService(CryptoTradingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrder(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order;
        }

        public async Task<bool> PutOrder(string id, Order order)
        {
            if (id != order.Id)
                return false;

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                    return false;
                else
                    throw;
            }

            return true;
        }

        public async Task<Order> PostOrder(Order order)
        {
            order.Id = Guid.NewGuid().ToString();
            _context.Orders.Add(order);

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                if (OrderExists(order.Id))
                    return null;
                else
                    throw;
            }

            return await GetOrder(order.Id);
        }

        public async Task<bool> DeleteOrder(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool OrderExists(string id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
