using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Context.Models;
using Context.Repository;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetByIdAsync(productId);
        }

        public async Task<IEnumerable<Product>> SearchProductsByNameAsync(string name)
        {
            return await _productRepository.GetByNameAsync(name);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            return await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {       
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int productId)
        {         
            await _productRepository.DeleteAsync(productId);
        }

        public async Task<bool> ProductExistsAsync(int productId)
        {
            return await _productRepository.ExistsAsync(productId);
        }

        public async Task<bool> ProductNameExistsAsync(string productName)
        {
            return await _productRepository.NameExistsAsync(productName);
        }
    }
}