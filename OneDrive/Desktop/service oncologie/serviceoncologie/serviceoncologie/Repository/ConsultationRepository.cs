using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;

namespace serviceoncologie.Repositories
{
    public class ConsultationRepository : IConsultationRepository
    {
        private readonly AppDbContext _context;

        public ConsultationRepository(AppDbContext context)
        {
            _context = context;

        }

        public IEnumerable<Consultation> GetAll()
        {
            return _context.Consultations
                .Include(c => c.Patient)  // Inclure les informations du patient
                .ThenInclude(p => p.Paiements)  // Inclure les paiements associés au patient
                .Include(c => c.Medecin)  // Inclure les informations du médecin
                .Include(c => c.ConsultationMaladies)
                    .ThenInclude(cm => cm.Maladie)  // Inclure les maladies liées à la consultation
                .Where(c =>
                    c.Patient.Categorie == "Militaire" ||  // Inclure les patients militaires, indépendamment du paiement
                    (c.Patient.Categorie == "Civile" &&
                     c.Patient.Paiements.Any(p => p.Statut == "Payé"))  // Inclure les patients civils ayant payé
                )
                .ToList();
        }


        public Consultation? GetById(int id)
        {
            return _context.Consultations.Find(id);
        }

        public Consultation? Create(Consultation consultation)
        {
            // Vérifier que l'utilisateur est un médecin
            var medecin = _context.Users.FirstOrDefault(u => u.Id == consultation.MedecinId && u.Role == "Medecin");
            if (medecin == null)
            {
                return null; // L'utilisateur n'est pas un médecin
            }

            // Récupérer le patient
            var patient = _context.Patients.Find(consultation.PatientId);
            if (patient == null)
            {
                return null; // Patient inexistant
            }

            // Charger le RDV associé au patient (avec paiement inclus si besoin)
            var rdv = _context.Rdvs
                .Include(r => r.Paiement)
                .FirstOrDefault(r => r.PatientId == consultation.PatientId);

            if (patient.Categorie == "Militaire")
            {
                // Vérifier que le RDV existe et que son État est CONFIRME
                if (rdv == null || rdv.Etat != "CONFIRME")
                {
                    return null; // RDV manquant ou non confirmé
                }
            }
            else if (patient.Categorie == "Civile")
            {
                // Vérifier que le RDV et le paiement sont valides
                if (rdv == null || rdv.Paiement == null || rdv.Paiement.Statut != "Payé")
                {
                    return null; // Paiement manquant ou non payé
                }
            }

            // Remplir les infos depuis le patient
            
            consultation.DossierId = patient.DossierId;

            _context.Consultations.Add(consultation);
            _context.SaveChanges();
            return consultation;
        }


        public void Update(Consultation consultation)
        {
            var existingConsultation = _context.Consultations.Find(consultation.Id);
            if (existingConsultation != null)
            {
                existingConsultation.Observation = consultation.Observation;
                existingConsultation.Tension = consultation.Tension;
                existingConsultation.Temperature = consultation.Temperature;

                _context.SaveChanges();
            }
        }

        public IEnumerable<Patient> GetPatientsByMedecin(int medecinId)
        {
            return _context.Rdvs
                .Include(r => r.Paiement)  // Charger les paiements
                .Include(r => r.Patient)   // Charger les patients
                .Where(r => r.MedecinId == medecinId &&
                            (r.Patient.Categorie == "Militaire" ||  // Inclure les patients militaires
                            (r.Patient.Categorie == "Civile" && r.Paiement != null && r.Paiement.Statut == "Payé"))) // Inclure les civils payés
                .Select(r => r.Patient)
                .Distinct()
                .ToList();
        }



        public void Delete(int id)
        {
            var consultation = _context.Consultations.Find(id);
            if (consultation != null)
            {
                _context.Consultations.Remove(consultation);
                _context.SaveChanges();
            }
        }
        public IEnumerable<Consultation> GetConsultationsByMedecin(int medecinId)
        {
            return _context.Consultations
                .Where(c => c.MedecinId == medecinId)
                .Include(c => c.Patient) // Inclure les détails du patient
                .Include(c => c.ConsultationMaladies) // Inclure les maladies associées
                .ThenInclude(cm => cm.Maladie)
                .ToList();
        }
        public IEnumerable<Consultation> GetConsultationsByDossier(int dossierId)
        {
            return _context.Consultations
                .Where(c => c.DossierId == dossierId)
                .Include(c => c.Patient) // Charger les détails du patient
                .Include(c => c.Medecin) // Charger les détails du médecin
                .Include(c => c.ConsultationMaladies) // Charger les maladies associées
                .ThenInclude(cm => cm.Maladie)
                .ToList();
        }
        public (string? MedecinNom, string? MedecinPrenom, string? PatientNom, string? PatientPrenom)? GetNomPrenomMedecinEtPatient(int consultationId)
        {
            var consultation = _context.Consultations
                .Include(c => c.Medecin)  // Inclure le médecin
                .Include(c => c.Patient)  // Inclure le patient
                .FirstOrDefault(c => c.Id == consultationId);

            if (consultation == null || consultation.Medecin == null || consultation.Patient == null)
                return null;

            return (
                consultation.Medecin.Nom,
                consultation.Medecin.Prenom,
                consultation.Patient.Nom,
                consultation.Patient.Prenom
            );
        }
        public IEnumerable<User> GetAllMedecins()
        {
            return _context.Users
                .Where(u => u.Role == "Medecin")
                .Select(u => new User
                {
                    Id = u.Id,
                    Nom = u.Nom,
                    Prenom = u.Prenom
                })
                .ToList();
        }




    }
}
