using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context.Models;
using Microsoft.EntityFrameworkCore;

namespace Context.Repository
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllAsync();
        Task<Sale> GetByIdAsync(int SaleId);
        Task<IEnumerable<Sale>> GetByClientIdAsync(int clientId);
        Task<IEnumerable<Sale>> GetByProductIdAsync(int productId);
        Task<Sale> AddAsync(Sale Sale);
        Task UpdateAsync(Sale Sale);
        Task DeleteAsync(int SaleId);
        Task<bool> ExistsAsync(int SaleId);
    }

    public class SaleRepository : ISaleRepository
    {
        private readonly Context.Models.Context _context;

        public SaleRepository(Context.Models.Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Sale> GetByIdAsync(int SaleId)
        {
            return await _context.Sales.FindAsync(SaleId);
        }

        public async Task<IEnumerable<Sale>> GetByClientIdAsync(int clientId)
        {
            return await _context.Sales
                .Where(s => s.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetByProductIdAsync(int productId)
        {
            return await _context.Sales
                .Where(s => s.ProductId == productId)
                .ToListAsync();
        }

        public async Task<Sale> AddAsync(Sale Sale)
        {
            if (Sale == null)
            {
                throw new ArgumentNullException(nameof(Sale));
            }

            Sale.CreatedAt = DateTime.UtcNow;
            await _context.Sales.AddAsync(Sale);
            await _context.SaveChangesAsync();
            return Sale;
        }

        public async Task UpdateAsync(Sale Sale)
        {
            if (Sale == null)
            {
                throw new ArgumentNullException(nameof(Sale));
            }

            _context.Sales.Update(Sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int SaleId)
        {
            var Sale = await _context.Sales.FindAsync(SaleId);
            if (Sale != null)
            {
                _context.Sales.Remove(Sale);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int SaleId)
        {
            return await _context.Sales.AnyAsync(e => e.SaleId == SaleId);
        }
    }
}