using System.Collections.Generic;
using System.Threading.Tasks;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface IDciMedicamentRepository
    {
        Task<IEnumerable<DciMedicament>> GetAllAsync();
        Task<DciMedicament> GetByIdAsync(int id);
        Task AddAsync(DciMedicament dciMedicament);
        Task UpdateAsync(DciMedicament dciMedicament);
        Task DeleteAsync(int id);
    }
}
