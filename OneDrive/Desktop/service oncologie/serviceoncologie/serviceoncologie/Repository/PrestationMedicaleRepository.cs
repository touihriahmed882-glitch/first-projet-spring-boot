using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public class PrestationMedicaleRepository : IPrestationMedicaleRepository
    {
        private readonly AppDbContext _context;

        public PrestationMedicaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PrestationMedicale>> GetAllPrestationsAsync()
        {
            return await _context.PrestationsMedicales.ToListAsync();
        }

        public async Task<PrestationMedicale> GetPrestationByIdAsync(int id)
        {
            return await _context.PrestationsMedicales.FindAsync(id);
        }

        public async Task AddPrestationAsync(PrestationMedicale prestation)
        {
            await _context.PrestationsMedicales.AddAsync(prestation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePrestationAsync(PrestationMedicale prestation)
        {
            _context.PrestationsMedicales.Update(prestation);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePrestationAsync(int id)
        {
            var prestation = await _context.PrestationsMedicales.FindAsync(id);
            if (prestation != null)
            {
                _context.PrestationsMedicales.Remove(prestation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
