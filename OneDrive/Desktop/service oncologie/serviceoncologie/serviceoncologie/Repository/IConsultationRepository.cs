using System.Collections.Generic;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface IConsultationRepository
    {
        IEnumerable<Consultation> GetAll();
        Consultation? GetById(int id);
        Consultation? Create(Consultation consultation);
        void Update(Consultation consultation);
        void Delete(int id);
        IEnumerable<Patient> GetPatientsByMedecin(int medecinId);
        IEnumerable<Consultation> GetConsultationsByMedecin(int medecinId); // Nouvelle méthode
        IEnumerable<Consultation> GetConsultationsByDossier(int dossierId);
        IEnumerable<User> GetAllMedecins();
        (string? MedecinNom, string? MedecinPrenom, string? PatientNom, string? PatientPrenom)? GetNomPrenomMedecinEtPatient(int consultationId);



    }
}
