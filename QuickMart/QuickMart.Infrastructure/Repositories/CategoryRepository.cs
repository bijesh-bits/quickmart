using Microsoft.EntityFrameworkCore;
using QuickMart.Core.Entities;
using QuickMart.Core.Interfaces;
using QuickMart.Infrastructure.Data;

namespace QuickMart.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(QuickMartDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Category>> GetActiveCategoriesAsync()
    {
        return await _dbSet
            .Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.CategoryName)
            .ToListAsync();
    }
}
