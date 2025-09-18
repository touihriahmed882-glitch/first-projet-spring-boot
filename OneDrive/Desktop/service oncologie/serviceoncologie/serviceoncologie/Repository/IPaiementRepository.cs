using serviceoncologie.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Repository
{
    public interface IPaiementRepository
    {
        Task<IEnumerable<Paiement>> GetAllPaiementsAsync();
        Task<Paiement> GetPaiementByIdAsync(int id);
        Task<Paiement> AddPaiementAsync(Paiement paiement);
        Task<Paiement> UpdatePaiementAsync(Paiement paiement);
        Task<bool> DeletePaiementAsync(int id);
        Task<IEnumerable<Patient>> GetPatientsWithPaidStatusAsync();

    }
}
