

using Microsoft.EntityFrameworkCore;
using Sales.Application.Interfaces;
using Sales.Domain.Entities;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;
        
        public SaleRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Sales.FindAsync(id);

            if (entity is not null) 
                _context.Sales.Remove(entity);
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales.Include(s => s.Items).ToListAsync();
        }

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            return await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sale sale) => _context.Sales.Update(sale);
    }
}
