using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;

namespace serviceoncologie.Repositories
{
    public class AdmissionRepository : IAdmissionRepository
    {
        private readonly AppDbContext _context;

        public AdmissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Admission> GetAll()
        {
            return _context.Admissions
                .Include(a => a.Consultation)      // Inclure la consultation liée
                 .ThenInclude(c => c.Patient) // Inclure le patient de la consultation
                .Include(a => a.DecisionStaf)   // Inclure la commission staff liée
                .ToList();
        }

        public Admission GetById(int id)
        {
            return _context.Admissions
                .Include(a => a.Consultation)      // Charger la consultation liée
                .Include(a => a.DecisionStaf)   // Charger la commission staff liée
                .FirstOrDefault(a => a.Id == id);
        }

        public void Add(Admission admission)
        {
            // Récupérer la consultation associée à l'admission via le ConsultationId
            var consultation = _context.Consultations
                                       .FirstOrDefault(c => c.Id == admission.ConsultationId);

            // Si la consultation n'existe pas, renvoyer une erreur ou gérer cette situation
            if (consultation == null)
            {
                throw new Exception("Consultation non trouvée pour l'Admission.");
            }

            // Assigner automatiquement le DossierId de la Consultation à l'Admission
            admission.DossierId = consultation.DossierId;

            // Récupérer la DecisionStaf associée à l'admission via le DecisionStafId
            var decisionStaf = _context.DecisionStafs
                                       .FirstOrDefault(d => d.Id == admission.DecisionStafId);

            // Si la DecisionStaf n'existe pas, gérer cette situation
            if (decisionStaf == null)
            {
                throw new Exception("DecisionStaf non trouvée pour l'Admission.");
            }

           

            // Ajouter l'admission à la base de données
            _context.Admissions.Add(admission);
            _context.SaveChanges();
        }


        public void Update(Admission admission)
        {
            var existingAdmission = _context.Admissions.Find(admission.Id);
            if (existingAdmission != null)
            {
                existingAdmission.DateSortie = admission.DateSortie;
                existingAdmission.MotifSortie = admission.MotifSortie;
                
                
                

                _context.SaveChanges();
            }
        }



        public void Delete(int id)
        {
            var admission = _context.Admissions.Find(id);
            if (admission != null)
            {
                _context.Admissions.Remove(admission);
                _context.SaveChanges();
            }
        }
        public IEnumerable<Consultation> GetConsultationsWithAdmissionObservation()
        {
            return _context.Consultations
                .Where(c => _context.DecisionStafs
                    .Any(d => d.ConsultationId == c.Id && d.Observation == "Admission"))
                .ToList();
        }
        public async Task<IEnumerable<Admission>> GetAdmissionsByDossierId(int dossierId)
        {
            return await _context.Admissions
                .Where(p => p.DossierId == dossierId)
                .ToListAsync();
        }
        public IEnumerable<(string Nom, string Prenom)> GetPatientNomPrenomByConsultationId(int consultationId)
        {
            return _context.Consultations
                .Where(c => c.Id == consultationId)  // Chercher la consultation avec l'ID passé
                .Include(c => c.Patient)  // Inclure le patient associé à la consultation
                .Select(c => new
                {
                    c.Patient.Nom,
                    c.Patient.Prenom
                })
                .ToList()
                .Select(c => (c.Nom, c.Prenom));  // Retourner un tuple avec le nom et prénom du patient
        }
        public IEnumerable<int> GetDecisionsWithAdmission()
        {
            return _context.DecisionStafs
                .Where(d => d.Observation == "Admission")
                .Select(d => d.Id)
                .ToList();
        }


      

    }
}
    