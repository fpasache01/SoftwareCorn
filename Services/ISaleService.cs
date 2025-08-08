using System;
using Context.Models;

namespace Services;

public interface ISaleService
{
        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task<Sale> GetSaleByIdAsync(int saleId);
        Task<IEnumerable<Sale>> GetSalesByClientIdAsync(int clientId);
        Task<IEnumerable<Sale>> GetSalesByProductIdAsync(int productId);
        Task<Sale> CreateSaleAsync(Sale sale);
        Task UpdateSaleAsync(Sale sale);
        Task DeleteSaleAsync(int saleId);
        Task<bool> SaleExistsAsync(int saleId);
}
