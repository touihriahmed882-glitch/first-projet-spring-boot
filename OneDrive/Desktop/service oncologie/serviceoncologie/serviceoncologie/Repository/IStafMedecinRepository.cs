using serviceoncologie.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Repositories
{
    public interface IStafMedecinRepository
    {
        Task<IEnumerable<StafMedecin>> GetAllStafMedecinsAsync();
        Task<StafMedecin> GetStafMedecinByIdAsync(int id);
        Task AddStafMedecinAsync(StafMedecin stafMedecin);
        Task DeleteStafMedecinAsync(int id);
    }
}
