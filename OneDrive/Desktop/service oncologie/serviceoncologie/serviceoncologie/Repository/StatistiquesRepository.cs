using System;
using System.Linq;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public class StatistiquesRepository : IStatistiquesRepository
    {
        private readonly AppDbContext _context;

        public StatistiquesRepository(AppDbContext context)
        {
            _context = context;
        }

        public Statistiques GetStatistiquesGenerales()
        {
            return new Statistiques
            {
                NombreTotalPatients = _context.Patients.Count(),
                NombreTotalConsultations = _context.Consultations.Count(),
                NombreTotalMedecins = _context.Users.Count(u => u.Role == "Médecin"),
                NombreTotalRdvs = _context.Rdvs.Count(),
                NombreRdvsConfirmes = _context.Rdvs.Count(r => r.Etat == "Confirmé"),
                NombreRdvsAnnules = _context.Rdvs.Count(r => r.Etat == "Annulé"),
                NombrePatientsAujourdHui = GetNombrePatientsParJour(DateTime.Today),
                NombreConsultationsAujourdHui = GetNombreConsultationsParJour(DateTime.Today)
            };
        }

        public int GetNombrePatientsParJour(DateTime date)
        {
            return _context.Patients.Count(p => p.DateNaissance.Date == date.Date);
        }

        public int GetNombreConsultationsParJour(DateTime date)
        {
            return _context.Consultations.Count(c => c.DateConsultation.Date == date.Date);
        }
        public double GetPourcentageRole(string role)
        {
            int totalUsers = _context.Users.Count();
            if (totalUsers == 0) return 0;

            int roleCount = _context.Users.Count(u => u.Role == role);
            return (double)roleCount / totalUsers * 100;
        }

        // 🔹 Calcul du pourcentage des admissions par rapport aux consultations
        public double GetPourcentageAdmissionsParConsultations()
        {
            int totalConsultations = _context.Consultations.Count();
            if (totalConsultations == 0) return 0;

            int totalAdmissions = _context.Admissions.Count();
            return (double)totalAdmissions / totalConsultations * 100;
        }
        public Dictionary<string, int> GetNombrePatientsParCategorie()
        {
            var statistiques = _context.Patients
                .GroupBy(p => p.Categorie)
                .Select(g => new { Categorie = g.Key, Nombre = g.Count() })
                .ToDictionary(x => x.Categorie, x => x.Nombre);

            return statistiques;
        }
        public Dictionary<string, int> GetNombreConsultationsParMaladie()
        {
            var statistiques = _context.ConsultationMaladies
                .GroupBy(cm => cm.Maladie!.Nom)
                .Select(g => new { Maladie = g.Key, Nombre = g.Count() })
                .ToDictionary(g => g.Maladie, g => g.Nombre);

            return statistiques;
        }

    }
}
