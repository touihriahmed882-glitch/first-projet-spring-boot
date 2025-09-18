using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Repository
{
    public class PaiementRepository : IPaiementRepository
    {
        private readonly AppDbContext _context;

        public PaiementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paiement>> GetAllPaiementsAsync()
        {
            return await _context.Paiements
                .Include(p => p.Patient) // Inclure le patient
                .Include(p => p.Rdv)     // Inclure le rendez-vous
                .Where(p => p.Patient != null && p.Patient.Categorie == "Civile")
                .ToListAsync();
        }


        public async Task<Paiement> GetPaiementByIdAsync(int id)
        {
            return await _context.Paiements.FindAsync(id);
        }

        public async Task<Paiement> AddPaiementAsync(Paiement paiement)
        {
            _context.Paiements.Add(paiement);
            await _context.SaveChangesAsync();
            return paiement;
        }

        public async Task<Paiement> UpdatePaiementAsync(Paiement paiement)
        {
            _context.Paiements.Update(paiement);
            await _context.SaveChangesAsync();
            return paiement;
        }

        public async Task<bool> DeletePaiementAsync(int id)
        {
            var paiement = await _context.Paiements.FindAsync(id);
            if (paiement == null) return false;

            _context.Paiements.Remove(paiement);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Patient>> GetPatientsWithPaidStatusAsync()
        {
            // Récupérer les patients ayant un paiement "Payé"
            return await _context.Paiements
                .Where(p => p.Statut == "Payé")  // Filtrer les paiements avec statut "Payé"
                .Include(p => p.Patient)  // Inclure les informations du patient
                .Select(p => p.Patient)  // Sélectionner uniquement les patients associés
                .Distinct()  // Eviter les doublons de patients
                .ToListAsync();  // Exécuter la requête asynchrone
        }

    }
}
