using System.Collections.Generic;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface IConsultationMaladieRepository
    {
        void AddMaladieToConsultation(int consultationId, int maladieId);
        void RemoveMaladieFromConsultation(int consultationId, int maladieId);
        IEnumerable<Maladie> GetMaladiesByConsultation(int consultationId);
        IEnumerable<ConsultationMaladie> GetAllConsultationMaladies();
        IEnumerable<ConsultationMaladie> GetConsultationMaladiesByMedecin(int medecinId);
        IEnumerable<ConsultationMaladie> GetConsultationMaladiesByDossier(int dossierId);
        IEnumerable<Maladie> GetMaladiesByDossier(int dossierId);


    }
}
