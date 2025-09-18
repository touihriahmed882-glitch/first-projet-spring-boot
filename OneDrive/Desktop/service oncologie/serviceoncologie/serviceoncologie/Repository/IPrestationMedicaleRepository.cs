using System.Collections.Generic;
using System.Threading.Tasks;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface IPrestationMedicaleRepository
    {
        Task<IEnumerable<PrestationMedicale>> GetAllPrestationsAsync();
        Task<PrestationMedicale> GetPrestationByIdAsync(int id);
        Task AddPrestationAsync(PrestationMedicale prestation);
        Task UpdatePrestationAsync(PrestationMedicale prestation);
        Task DeletePrestationAsync(int id);
    }
}
