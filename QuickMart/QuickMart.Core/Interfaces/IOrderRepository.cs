using QuickMart.Core.Entities;

namespace QuickMart.Core.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
    Task<Order?> GetByOrderNumberAsync(string orderNumber);
    Task<Order?> GetOrderWithDetailsAsync(int orderId);
    Task<string> GenerateOrderNumberAsync();
}
