using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Repository
{
    public class ProtocoleRepository : IProtocoleRepository
    {
        private readonly AppDbContext _context;

        public ProtocoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Protocole>> GetAllAsync()
        {
            return await _context.Protocoles
        .Include(p => p.Cures) // Inclure aussi la collection Cure
        .ToListAsync();
        }

        public async Task<Protocole> GetByIdAsync(int id)
        {
            return await _context.Protocoles.FindAsync(id);
        }

        public async Task AddAsync(Protocole protocole)
        {
            // Ajouter et enregistrer
            await _context.Protocoles.AddAsync(protocole);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Protocole>> GetProtocoleByDossierId(int dossierId)
        {
            return await _context.Protocoles
                .ToListAsync();
        }


        public async Task UpdateAsync(Protocole protocole)
        {
            _context.Protocoles.Update(protocole);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var protocole = await _context.Protocoles.FindAsync(id);
            if (protocole != null)
            {
                _context.Protocoles.Remove(protocole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
