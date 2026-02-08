using Microsoft.EntityFrameworkCore;
using QuickMart.Core.Entities;
using QuickMart.Core.Interfaces;
using QuickMart.Infrastructure.Data;

namespace QuickMart.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(QuickMartDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId && p.IsActive)
            .OrderBy(p => p.ProductName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.IsActive && 
                   (p.ProductName.Contains(searchTerm) || 
                    p.Description!.Contains(searchTerm) ||
                    p.Brand!.Contains(searchTerm)))
            .OrderBy(p => p.ProductName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetFeaturedProductsAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.IsFeatured && p.IsActive)
            .OrderBy(p => p.ProductName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.IsActive)
            .OrderBy(p => p.ProductName)
            .ToListAsync();
    }
}
