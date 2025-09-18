using System.Collections.Generic;
using System.Threading.Tasks;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface ICureRepository
    {
        Task<IEnumerable<Cure>> GetAllCures();
        Task<Cure> GetCureById(int id);
        Task AddCure(Cure cure);
        Task UpdateCure(Cure cure);
        Task<IEnumerable<Cure>> GetCureByDossierId(int dossierId);
        Task DeleteCure(int id);
        Task<CureAdmissionResponse> AddCureByDecisionStafIdAsync(int decisionStafId);
        Task<bool> ValiderCureAsync(int cureId);
        Task<List<Cure>> GetCuresByDecisionStafIdAsync(int decisionStafId);        
        Task<bool> DeleteCuresByDecisionStafIdAsync(int decisionStafId);
        Task<IEnumerable<Cure>> GetCuresByAdmissionIdAsync(int admissionId);







    }
}
