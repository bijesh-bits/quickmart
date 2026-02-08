using QuickMart.Core.Entities;

namespace QuickMart.Core.Interfaces;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart?> GetByUserIdAsync(int userId);
    Task<Cart?> GetCartWithItemsAsync(int userId);
}
