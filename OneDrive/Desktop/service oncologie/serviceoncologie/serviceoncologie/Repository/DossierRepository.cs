using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;
using System.Linq;

namespace serviceoncologie.Repository
{
    public class DossierRepository : IDossierRepository
    {
        private readonly AppDbContext _context;

        public DossierRepository(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Ajouter un dossier
        public void AjouterDossier(Dossier dossier)
        {
            _context.Dossiers.Add(dossier);
            _context.SaveChanges();
        }

        // ✅ Supprimer un dossier
        public void SupprimerDossier(int id)
        {
            var dossier = _context.Dossiers.Find(id);
            if (dossier != null)
            {
                _context.Dossiers.Remove(dossier);
                _context.SaveChanges();
            }
        }

        // ✅ Récupérer un dossier avec détails des consultations et décisions staf
        public Dossier? ObtenirDossierAvecDetails(int id)
        {
            var dossier = _context.Dossiers
                .Include(d => d.Consultations)
                .Include(d => d.DecisionStafs)
                .Include(d => d.Patients)
                .Include(d => d.Admissions)
                .Include(d => d.Cures)
                .FirstOrDefault(d => d.Id == id);

            if (dossier != null)
            {
                dossier.Cures = dossier.Cures?.Where(c => c.EstValidee).ToList();
            }

            return dossier;
        }

        public Dossier? ObtenirDossierParNomPrenomPatient(string nom, string prenom)
        {
            return _context.Dossiers
                .Include(d => d.Patients)
                .Where(d => d.Patients.Any(p => p.Nom == nom && p.Prenom == prenom))
                .Select(d => new Dossier
                {
                    Id = d.Id,
                    // autres propriétés à copier si nécessaires...
                    Patients = d.Patients,
                    Cures = d.Cures.Where(c => c.EstValidee).ToList() // uniquement les cures validées
                })
                .FirstOrDefault();
        }



        public async Task<IEnumerable<Dossier>> GetAllDossiers()
        {
            return await _context.Dossiers
                .Include(d => d.Consultations)
                .Include(d => d.DecisionStafs)
                .Include(d => d.Patients)
                .Include(d => d.Admissions)
                .ToListAsync();
        }
    }
}
