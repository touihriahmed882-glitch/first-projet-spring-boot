using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public class MedicamentRepository : IMedicamentRepository
    {
        private readonly AppDbContext _context;

        public MedicamentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medicament>> GetAllMedicaments()
        {
            return await _context.Medicaments.ToListAsync();
        }

        public async Task<Medicament> GetMedicamentById(int id)
        {
            return await _context.Medicaments.FindAsync(id);
        }

        public async Task AddMedicament(Medicament medicament)
        {
            await _context.Medicaments.AddAsync(medicament);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMedicament(Medicament medicament)
        {
            _context.Medicaments.Update(medicament);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedicament(int id)
        {
            var medicament = await _context.Medicaments.FindAsync(id);
            if (medicament != null)
            {
                _context.Medicaments.Remove(medicament);
                await _context.SaveChangesAsync();
            }
        }
    }
}
