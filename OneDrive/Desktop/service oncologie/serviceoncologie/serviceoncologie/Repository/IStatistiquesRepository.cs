using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface IStatistiquesRepository
    {
        Statistiques GetStatistiquesGenerales();
        int GetNombrePatientsParJour(DateTime date);
        int GetNombreConsultationsParJour(DateTime date);
        double GetPourcentageRole(string role);
        double GetPourcentageAdmissionsParConsultations();
        Dictionary<string, int> GetNombrePatientsParCategorie();
        Dictionary<string, int> GetNombreConsultationsParMaladie();

    }
}
