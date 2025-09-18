using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;

namespace serviceoncologie.Repositories
{
    public class DecisionStafRepository : IDecisionStafRepository
    {
        private readonly AppDbContext _context;

        public DecisionStafRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DecisionStaf> GetAll()
        {
            return _context.DecisionStafs
                .Include(d => d.Admissions)
                .Include(d => d.Consultation)
                    .ThenInclude(c => c.Patient)
                .Include(d => d.CommissionStaf)
                .Include(d => d.Dossier)
                .Include(d => d.Protocole) // à ajouter

                .ToList();
        }

        public DecisionStaf GetById(int id)
        {
            var decisionStaf = _context.DecisionStafs
                .Include(d => d.Admissions)
                .Include(d => d.Consultation)
                    .ThenInclude(c => c.Patient)
                .Include(d => d.CommissionStaf)
                .Include(d => d.Dossier)
                .Include(d => d.Protocole) // 🔥 Ajout ici
                .FirstOrDefault(d => d.Id == id);

            if (decisionStaf != null && decisionStaf.Consultation != null)
            {
                decisionStaf.DossierId = decisionStaf.Consultation.DossierId;
            }

            return decisionStaf;
        }


        public void Add(DecisionStaf decisionStaf)
        {
            var consultation = _context.Consultations
                                       .FirstOrDefault(c => c.Id == decisionStaf.ConsultationId);

            if (consultation != null)
            {
                decisionStaf.DossierId = consultation.DossierId;
            }
            else
            {
                throw new Exception("Consultation non trouvée pour l'ID spécifié.");
            }

            // Si tu veux forcer une liaison à un protocole, tu peux vérifier ici si l'ID est valide
            if (decisionStaf.ProtocoleId != null)
            {
                var protocole = _context.Protocoles.Find(decisionStaf.ProtocoleId);
                if (protocole == null)
                {
                    throw new Exception("Protocole spécifié invalide.");
                }
            }

            _context.DecisionStafs.Add(decisionStaf);
            _context.SaveChanges();
        }


        public void Update(DecisionStaf decisionStaf)
        {
            _context.DecisionStafs.Update(decisionStaf);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var decisionStaf = _context.DecisionStafs.Find(id);
            if (decisionStaf != null)
            {
                _context.DecisionStafs.Remove(decisionStaf);
                _context.SaveChanges();
            }
        }
        public IEnumerable<int> GetDecisionsWithAdmission()
        {
            return _context.DecisionStafs
                .Where(d => d.Observation == "Admission")
                .Select(d => d.Id)
                .ToList();
        }

        public Consultation GetConsultationByDecisionId(int id)
        {
            return _context.DecisionStafs
                .Where(d => d.Id == id && d.Observation == "Admission")
                .Include(d => d.Consultation)
                    .ThenInclude(c => c.Patient) // Ajout pour charger aussi le patient
                .Select(d => d.Consultation)
                .FirstOrDefault();
        }

        public IEnumerable<DecisionStaf> GetDecisionsByDossierId(int dossierId)
        {
            return _context.DecisionStafs
                .Where(d => d.DossierId == dossierId)
                .Include(d => d.Consultation)
                .Include(d => d.CommissionStaf)
                .Include(d => d.Admissions)
                .Include(d => d.Protocole) // 🔥 Ajout ici
                .ToList();
        }

    }
}
