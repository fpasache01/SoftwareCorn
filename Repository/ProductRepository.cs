using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context.Models;
using Microsoft.EntityFrameworkCore;

namespace Context.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int productId);
        Task<IEnumerable<Product>> GetByNameAsync(string productName);
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int productId);
        Task<bool> ExistsAsync(int productId);
        Task<bool> NameExistsAsync(string productName);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly Context.Models.Context _context;

        public ProductRepository(Context.Models.Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                return Enumerable.Empty<Product>();

            return await _context.Products
                .Where(p => p.ProductName != null && p.ProductName.Contains(productName))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> AddAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await GetByIdAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int productId)
        {
            return await _context.Products
                .AnyAsync(p => p.ProductId == productId);
        }

        public async Task<bool> NameExistsAsync(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                return false;

            return await _context.Products
                .AnyAsync(p => p.ProductName != null && p.ProductName.ToLower() == productName.ToLower());
        }
    }
}