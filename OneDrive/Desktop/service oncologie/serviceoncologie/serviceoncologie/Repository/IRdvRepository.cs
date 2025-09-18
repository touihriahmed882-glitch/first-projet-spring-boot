using serviceoncologie.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Repositories
{
    public interface IRdvRepository
    {
        Task<IEnumerable<Rdv>> GetAllRdvsAsync();
        Task<Rdv?> GetRdvByIdAsync(int id);
        Task<IEnumerable<Rdv>> GetRdvsByMedecinIdAsync(int medecinId);
        Task<Rdv> AddRdvAsync(Rdv rdv);
        Task<Rdv?> UpdateRdvAsync(Rdv rdv);
        Task<bool> DeleteRdvAsync(int id);
        Task<IEnumerable<Rdv>> GetRdvsByPatientIdAsync(int patientId);
    }
}
