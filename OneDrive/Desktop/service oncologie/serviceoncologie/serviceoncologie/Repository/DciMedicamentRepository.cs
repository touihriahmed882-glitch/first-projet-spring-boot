using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Repository
{
    public class DciMedicamentRepository : IDciMedicamentRepository
    {
        private readonly AppDbContext _context;

        public DciMedicamentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DciMedicament>> GetAllAsync()
        {
            return await _context.DciMedicaments
                .Include(dm => dm.Dci)
                .Include(dm => dm.Medicament)
                .ToListAsync();
        }

        public async Task<DciMedicament> GetByIdAsync(int id)
        {
            return await _context.DciMedicaments
                .Include(dm => dm.Dci)
                .Include(dm => dm.Medicament)
                .FirstOrDefaultAsync(dm => dm.Id == id);
        }

        public async Task AddAsync(DciMedicament dciMedicament)
        {
            await _context.DciMedicaments.AddAsync(dciMedicament);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DciMedicament dciMedicament)
        {
            _context.DciMedicaments.Update(dciMedicament);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DciMedicaments.FindAsync(id);
            if (entity != null)
            {
                _context.DciMedicaments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
