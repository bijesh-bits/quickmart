using Microsoft.EntityFrameworkCore;
using QuickMart.Core.Entities;
using QuickMart.Core.Interfaces;
using QuickMart.Infrastructure.Data;

namespace QuickMart.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(QuickMartDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
    {
        return await _dbSet
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.Payment)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<Order?> GetByOrderNumberAsync(string orderNumber)
    {
        return await _dbSet
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.Payment)
            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
    }

    public async Task<Order?> GetOrderWithDetailsAsync(int orderId)
    {
        return await _dbSet
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.Payment)
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task<string> GenerateOrderNumberAsync()
    {
        var date = DateTime.UtcNow;
        var datePrefix = $"ORD-{date:yyyyMMdd}";
        
        var lastOrder = await _dbSet
            .Where(o => o.OrderNumber.StartsWith(datePrefix))
            .OrderByDescending(o => o.OrderNumber)
            .FirstOrDefaultAsync();

        if (lastOrder == null)
        {
            return $"{datePrefix}-0001";
        }

        var lastNumber = int.Parse(lastOrder.OrderNumber.Split('-').Last());
        return $"{datePrefix}-{(lastNumber + 1):D4}";
    }
}
