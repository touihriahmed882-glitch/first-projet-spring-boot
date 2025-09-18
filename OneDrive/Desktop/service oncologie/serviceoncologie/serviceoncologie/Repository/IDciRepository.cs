using System.Collections.Generic;
using System.Threading.Tasks;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface IDciRepository
    {
        Task<IEnumerable<Dci>> GetAllAsync();
        Task<Dci> GetByIdAsync(int id);
        Task AddAsync(Dci dci);
        Task UpdateAsync(Dci dci);
        Task DeleteAsync(int id);
    }
}
