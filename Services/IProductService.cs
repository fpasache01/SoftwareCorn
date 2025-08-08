using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Context.Models;

namespace Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task<IEnumerable<Product>> SearchProductsByNameAsync(string name);
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task<bool> ProductExistsAsync(int productId);
        Task<bool> ProductNameExistsAsync(string productName);
    }
}