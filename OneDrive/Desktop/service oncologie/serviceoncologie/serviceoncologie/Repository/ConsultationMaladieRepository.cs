using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;

namespace serviceoncologie.Repositories
{
    public class ConsultationMaladieRepository : IConsultationMaladieRepository
    {
        private readonly AppDbContext _context;

        public ConsultationMaladieRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddMaladieToConsultation(int consultationId, int maladieId)
        {
            // Vérifier si l'association existe déjà
            bool exists = _context.ConsultationMaladies
                .Any(cm => cm.ConsultationId == consultationId && cm.MaladieId == maladieId);

            if (!exists)
            {
                // Récupérer la consultation et son DossierId
                var consultation = _context.Consultations
                    .FirstOrDefault(c => c.Id == consultationId);

                if (consultation == null)
                {
                    // Retourner une erreur si la consultation n'existe pas
                    throw new InvalidOperationException("Consultation non trouvée.");
                }

                // Créer l'association avec le DossierId de la consultation
                var association = new ConsultationMaladie
                {
                    ConsultationId = consultationId,
                    MaladieId = maladieId,
                    DossierId = consultation.DossierId // Récupérer le DossierId de la consultation
                };

                _context.ConsultationMaladies.Add(association);
                _context.SaveChanges();
            }
        }


        public IEnumerable<ConsultationMaladie> GetConsultationMaladiesByDossier(int dossierId)
        {
            return _context.ConsultationMaladies
                .Include(cm => cm.Consultation)
                .Include(cm => cm.Maladie)
                .Where(cm => cm.DossierId == dossierId)
                .ToList();
        }
    


        public void RemoveMaladieFromConsultation(int consultationId, int maladieId)
        {
            var association = _context.ConsultationMaladies
                .FirstOrDefault(cm => cm.ConsultationId == consultationId && cm.MaladieId == maladieId);

            if (association != null)
            {
                _context.ConsultationMaladies.Remove(association);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Maladie> GetMaladiesByConsultation(int consultationId)
        {
            return _context.ConsultationMaladies
                .Where(cm => cm.ConsultationId == consultationId)
                .Select(cm => cm.Maladie)
                .ToList();
        }
        public IEnumerable<ConsultationMaladie> GetAllConsultationMaladies()
        {
            return _context.ConsultationMaladies
                .Include(cm => cm.Consultation)
                .Include(cm => cm.Maladie)
                .ToList();
        }
        public IEnumerable<ConsultationMaladie> GetConsultationMaladiesByMedecin(int medecinId)
        {
            return _context.ConsultationMaladies
                .Include(cm => cm.Consultation)
                .Include(cm => cm.Maladie)
                .Where(cm => cm.Consultation.MedecinId == medecinId)
                .ToList();
        }
        public IEnumerable<Maladie> GetMaladiesByDossier(int dossierId)
        {
            return _context.ConsultationMaladies
                .Where(cm => cm.DossierId == dossierId)
                .Select(cm => cm.Maladie)
                .Distinct()
                .ToList();
        }



    }
}
