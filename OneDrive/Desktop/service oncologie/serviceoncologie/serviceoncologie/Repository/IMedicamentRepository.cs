using System.Collections.Generic;
using System.Threading.Tasks;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface IMedicamentRepository
    {
        Task<IEnumerable<Medicament>> GetAllMedicaments();
        Task<Medicament> GetMedicamentById(int id);
        Task AddMedicament(Medicament medicament);
        Task UpdateMedicament(Medicament medicament);
        Task DeleteMedicament(int id);
    }
}
