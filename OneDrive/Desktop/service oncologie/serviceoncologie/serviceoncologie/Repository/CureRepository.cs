using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public class CureRepository : ICureRepository
    {
        private readonly AppDbContext _context;

        public CureRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cure>> GetAllCures()
        {
            return await _context.Cures
                .Include(c => c.Protocole)
                .Include(c => c.Medicament)
                .ToListAsync();
        }
        public async Task<CureAdmissionResponse> AddCureByDecisionStafIdAsync(int decisionStafId)
        {
            // Récupérer la décision de staff
            var decisionStaf = await _context.DecisionStafs
                .Include(d => d.Dossier)
                .Include(d => d.Protocole)
                .FirstOrDefaultAsync(d => d.Id == decisionStafId);

            if (decisionStaf == null)
                throw new Exception("Decision Staff non trouvée.");

            // Chercher une admission liée à la décision, qui n’a pas encore de cure
            var admission = await _context.Admissions
                .Where(a => a.DecisionStafId == decisionStafId)
                .FirstOrDefaultAsync(a => !_context.Cures.Any(c => c.AdmissionId == a.Id));

            if (admission == null)
                throw new Exception("Aucune admission disponible sans cure pour cette décision de staff.");

            // Déterminer l'intervalle selon le protocole
            int intervalleJour = decisionStaf.Protocole.NomProtocole switch
            {
                "Chimiothérapie" => 24,
                "Radiothérapie" => 30,
                _ => 7
            };

            // Récupérer le médicament selon le protocole
            Medicament medicament = decisionStaf.Protocole.NomProtocole switch
            {
                "Chimiothérapie" => await _context.Medicaments.FirstOrDefaultAsync(m => m.LibelleMedicament == "Ifosfamide"),
                "Radiothérapie" => await _context.Medicaments.FirstOrDefaultAsync(m => m.LibelleMedicament == "Cisplatine"),
                _ => null
            };

            if (medicament == null)
                throw new Exception("Médicament introuvable.");

            // Créer la cure
            var cure = new Cure
            {
                DateCure = admission.DateAdmission,
                Observation = "Observation à définir",
                MedicamentId = medicament.Id,
                DossierId = decisionStaf.DossierId,
                DecisionStafId = decisionStafId,
                ProtocoleId = decisionStaf.Protocole.Id,
                AdmissionId = admission.Id
            };

            // Ajouter et sauvegarder
            await _context.Cures.AddAsync(cure);
            await _context.SaveChangesAsync();

            return new CureAdmissionResponse
            {
                Cures = new List<Cure> { cure },
                Admissions = new List<Admission> { admission }
            };
        }






        public async Task<bool> ValiderCureAsync(int cureId)
        {
            // Récupérer la cure à valider
            var cure = await _context.Cures
                                     .Include(c => c.DecisionStaf)  // Inclure le protocole pour accéder au DossierId
                                     .FirstOrDefaultAsync(c => c.Id == cureId);

            if (cure == null)
                return false;

            // Vérifier que le DossierId de la cure correspond à celui du protocole
           

            // Si la validation est correcte, marquer la cure comme validée
            cure.EstValidee = true;
            _context.Cures.Update(cure);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Cure> GetCureById(int id)
        {
            return await _context.Cures
                .Include(c => c.Protocole)
                .Include(c => c.Medicament)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCure(Cure cure)
        {
            // Récupérer le protocole associé à la cure
            var protocole = await _context.Protocoles
                                          .FirstOrDefaultAsync(p => p.Id == cure.ProtocoleId);

            // Vérifier si le protocole existe
            if (protocole == null)
            {
                throw new Exception("Protocole non trouvé pour cette cure.");
            }

            // Assigner automatiquement le DossierId depuis le Protocole


            // Ajouter et enregistrer
            await _context.Cures.AddAsync(cure);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Cure>> GetCureByDossierId(int dossierId)
        {
            return await _context.Cures
                .Where(c => c.DossierId == dossierId && c.EstValidee)
                .ToListAsync();
        }



        public async Task UpdateCure(Cure cure)
        {
            _context.Cures.Update(cure);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCure(int id)
        {
            var cure = await _context.Cures.FindAsync(id);
            if (cure != null)
            {
                _context.Cures.Remove(cure);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Cure>> GetCuresByDecisionStafIdAsync(int decisionStafId)
        {
            return await _context.Cures
                .Where(c => c.DecisionStafId == decisionStafId)
                .OrderBy(c => c.AdmissionId)  // Tri ici par AdmissionId croissant
                .Include(c => c.Medicament)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cure>> GetCuresByAdmissionIdAsync(int admissionId)
        {
            // Récupérer l'admission et inclure la décision du staff pour obtenir le DecisionStafId
            var admission = await _context.Admissions
                .Include(a => a.DecisionStaf)
                .FirstOrDefaultAsync(a => a.Id == admissionId);

            if (admission == null)
                throw new Exception("Admission non trouvée.");

            // Récupérer toutes les cures associées au DecisionStafId de l'admission
            var cures = await _context.Cures
                .Where(c => c.DecisionStafId == admission.DecisionStafId)
                .ToListAsync();

            return cures;
        }





        public async Task<bool> DeleteCuresByDecisionStafIdAsync(int decisionStafId)
        {
            // Récupérer toutes les cures associées à la décision du staff
            var cures = await _context.Cures.Where(c => c.DecisionStafId == decisionStafId).ToListAsync();

            if (cures == null || cures.Count == 0)
                return false; // Pas de cures à supprimer

            // Supprimer toutes les cures
            _context.Cures.RemoveRange(cures);
            await _context.SaveChangesAsync();

            return true;
        }




    }
}
