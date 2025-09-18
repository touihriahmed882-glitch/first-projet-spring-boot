using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serviceoncologie.Repositories
{
    public class StafMedecinRepository : IStafMedecinRepository
    {
        private readonly AppDbContext _context;

        public StafMedecinRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StafMedecin>> GetAllStafMedecinsAsync()
        {
            return await _context.StafMedecins
                .Include(sm => sm.User)
                .Include(sm => sm.CommissionStaf)
                .Where(sm => sm.User.Role == "Medecin") // Vérifie que l'utilisateur est un médecin
                .ToListAsync();
        }

        public async Task<StafMedecin> GetStafMedecinByIdAsync(int id)
        {
            return await _context.StafMedecins
                .Include(sm => sm.User)
                .Include(sm => sm.CommissionStaf)
                .Where(sm => sm.User.Role == "Medecin")
                .FirstOrDefaultAsync(sm => sm.Id == id);
        }

        public async Task AddStafMedecinAsync(StafMedecin stafMedecin)
        {
            var user = await _context.Users.FindAsync(stafMedecin.userid);
            if (user == null || user.Role != "Medecin")
            {
                throw new System.Exception("L'utilisateur n'est pas un médecin.");
            }

            await _context.StafMedecins.AddAsync(stafMedecin);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStafMedecinAsync(int id)
        {
            var stafMedecin = await _context.StafMedecins
                .Include(sm => sm.User)
                .FirstOrDefaultAsync(sm => sm.Id == id);

            if (stafMedecin != null && stafMedecin.User.Role == "Medecin")
            {
                _context.StafMedecins.Remove(stafMedecin);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new System.Exception("Seuls les médecins peuvent être supprimés.");
            }
        }
    }
}
