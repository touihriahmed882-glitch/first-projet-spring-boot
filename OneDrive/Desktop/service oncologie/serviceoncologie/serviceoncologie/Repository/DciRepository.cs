using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Repository
{
    public class DciRepository : IDciRepository
    {
        private readonly AppDbContext _context;

        public DciRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dci>> GetAllAsync()
        {
            return await _context.Dcis.ToListAsync();
        }

        public async Task<Dci> GetByIdAsync(int id)
        {
            return await _context.Dcis.FindAsync(id);
        }

        public async Task AddAsync(Dci dci)
        {
            await _context.Dcis.AddAsync(dci);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Dci dci)
        {
            _context.Dcis.Update(dci);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dci = await _context.Dcis.FindAsync(id);
            if (dci != null)
            {
                _context.Dcis.Remove(dci);
                await _context.SaveChangesAsync();
            }
        }
    }
}
