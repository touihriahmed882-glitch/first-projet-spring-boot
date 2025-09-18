using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Repositories
{
    public class RdvRepository : IRdvRepository
    {
        private readonly AppDbContext _context;

        public RdvRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rdv>> GetAllRdvsAsync()
        {
            return await _context.Rdvs
                .Include(r => r.Medecin)
                .Include(r => r.Patient)
                .ToListAsync();
        }

        public async Task<Rdv?> GetRdvByIdAsync(int id)
        {
            return await _context.Rdvs
                .Include(r => r.Medecin)
                .Include(r => r.Patient)

                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Rdv>> GetRdvsByMedecinIdAsync(int medecinId)
        {
            return await _context.Rdvs
                .Where(r => r.MedecinId == medecinId)
                .ToListAsync();
        }

        public async Task<Rdv> AddRdvAsync(Rdv rdv)
        {
            // Vérifier que l'utilisateur est un médecin
            var medecin = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == rdv.MedecinId && u.Role == "Medecin");

            if (medecin == null)
            {
                throw new ArgumentException("L'utilisateur spécifié n'est pas un médecin.");
            }

            _context.Rdvs.Add(rdv);
            await _context.SaveChangesAsync();
            return rdv;
        }

        public async Task<Rdv?> UpdateRdvAsync(Rdv rdv)
        {
            var existingRdv = await _context.Rdvs.FindAsync(rdv.Id);
            if (existingRdv == null) return null;

            existingRdv.DateRdv = rdv.DateRdv;
            existingRdv.Etat = rdv.Etat;
            existingRdv.Observation = rdv.Observation;

            await _context.SaveChangesAsync();
            return existingRdv;
        }

        public async Task<bool> DeleteRdvAsync(int id)
        {
            var rdv = await _context.Rdvs.FindAsync(id);
            if (rdv == null) return false;

            _context.Rdvs.Remove(rdv);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Rdv>> GetRdvsByPatientIdAsync(int patientId)
        {
            return await _context.Rdvs
                .Where(r => r.PatientId == patientId)
                .Include(r => r.Medecin)
                .ToListAsync();
        }

    }
}
