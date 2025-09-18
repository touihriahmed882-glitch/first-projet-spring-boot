using serviceoncologie.Data.Models;
using System.Collections.Generic;

namespace serviceoncologie.Repository
{
    public interface IDossierRepository
    {
        void AjouterDossier(Dossier dossier);
        void SupprimerDossier(int id);
        Dossier? ObtenirDossierAvecDetails(int id);
        Task<IEnumerable<Dossier>> GetAllDossiers(); // ✅ Ajout de la méthode GetAll
        Dossier? ObtenirDossierParNomPrenomPatient(string nom, string prenom);


    }
}
