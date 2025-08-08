using System;
using Context.Models;
using Context.Repository;

namespace Services;

public class SaleService: ISaleService

{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;

    public SaleService(ISaleRepository saleRepository, IProductRepository productRepository)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Sale>> GetAllSalesAsync()
    {
        return await _saleRepository.GetAllAsync();
    }

    public async Task<Sale> GetSaleByIdAsync(int saleId)
    {
        return await _saleRepository.GetByIdAsync(saleId);
    }

    public async Task<IEnumerable<Sale>> GetSalesByClientIdAsync(int clientId)
    {
        return await _saleRepository.GetByClientIdAsync(clientId);
    }

    public async Task<IEnumerable<Sale>> GetSalesByProductIdAsync(int productId)
    {
        return await _saleRepository.GetByProductIdAsync(productId);
    }

    public async Task<Sale> CreateSaleAsync(Sale sale)
    {
        sale.CreatedAt = DateTime.UtcNow;
        return await _saleRepository.AddAsync(sale);
    }

    public async Task UpdateSaleAsync(Sale sale)
    {
        await _saleRepository.UpdateAsync(sale);
    }

    public async Task DeleteSaleAsync(int saleId)
    {
        await _saleRepository.DeleteAsync(saleId);
    }

    public async Task<bool> SaleExistsAsync(int saleId)
    {
        return await _saleRepository.ExistsAsync(saleId);
    }

}
