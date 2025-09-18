using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface IAdmissionRepository
    {
        IEnumerable<Admission> GetAll();
        Admission GetById(int id);
        void Add(Admission admission);
        void Update(Admission admission);
        IEnumerable<Consultation> GetConsultationsWithAdmissionObservation();
        Task<IEnumerable<Admission>> GetAdmissionsByDossierId(int dossierId);
        IEnumerable<(string Nom, string Prenom)> GetPatientNomPrenomByConsultationId(int consultationId);



        void Delete(int id);
    }
}
