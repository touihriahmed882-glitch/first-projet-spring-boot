using serviceoncologie.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Repository
{
    public interface IProtocoleRepository
    {
        Task<IEnumerable<Protocole>> GetAllAsync();
        Task<Protocole> GetByIdAsync(int id);
        Task AddAsync(Protocole protocole);
        Task UpdateAsync(Protocole protocole);
        Task<IEnumerable<Protocole>> GetProtocoleByDossierId(int dossierId);
        Task DeleteAsync(int id);
    }
}
