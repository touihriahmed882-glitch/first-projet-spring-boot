using System.Collections.Generic;
using System.Threading.Tasks;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface IConsultationPrestationRepository
    {
        Task<IEnumerable<ConsultationPrestation>> GetAllConsultationPrestationsAsync();
        Task<IEnumerable<PrestationMedicale>> GetPrestationsByConsultationIdAsync(int consultationId);
        Task AddConsultationPrestationAsync(ConsultationPrestation consultationPrestation);
        Task RemoveConsultationPrestationAsync(int consultationId, int prestationId);
        Task<ConsultationPrestation> GetConsultationPrestationByIdAsync(int id);
        Task RemoveConsultationPrestationByIdAsync(int id);

    }
}
