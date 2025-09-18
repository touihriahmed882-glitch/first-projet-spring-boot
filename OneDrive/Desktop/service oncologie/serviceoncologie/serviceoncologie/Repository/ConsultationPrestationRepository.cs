using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public class ConsultationPrestationRepository : IConsultationPrestationRepository
    {
        private readonly AppDbContext _context;

        public ConsultationPrestationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConsultationPrestation>> GetAllConsultationPrestationsAsync()
        {
            return await _context.ConsultationPrestations
                .Include(cp => cp.PrestationMedicale)
                .Include(cp => cp.Consultation)
                                    .ThenInclude(c => c.Patient) // Inclure le patient de la consultation

                .ToListAsync();
        }

        public async Task<IEnumerable<PrestationMedicale>> GetPrestationsByConsultationIdAsync(int consultationId)
        {
            return await _context.ConsultationPrestations
                .Where(cp => cp.ConsultationId == consultationId)
                .Select(cp => cp.PrestationMedicale)
                .ToListAsync();
        }

        public async Task AddConsultationPrestationAsync(ConsultationPrestation consultationPrestation)
        {
            await _context.ConsultationPrestations.AddAsync(consultationPrestation);
            await _context.SaveChangesAsync();
        }
        public async Task<ConsultationPrestation> GetConsultationPrestationByIdAsync(int id)
        {
            return await _context.ConsultationPrestations
                .Include(cp => cp.PrestationMedicale)
                .Include(cp => cp.Consultation)
                .FirstOrDefaultAsync(cp => cp.Id == id);
        }


        public async Task RemoveConsultationPrestationAsync(int consultationId, int prestationId)
        {
            var consultationPrestation = await _context.ConsultationPrestations
                .FirstOrDefaultAsync(cp => cp.ConsultationId == consultationId && cp.PrestationMedicaleId == prestationId);

            if (consultationPrestation != null)
            {
                _context.ConsultationPrestations.Remove(consultationPrestation);
                await _context.SaveChangesAsync();
            }
        }
        public async Task RemoveConsultationPrestationByIdAsync(int id)
        {
            var consultationPrestation = await _context.ConsultationPrestations.FindAsync(id);
            if (consultationPrestation != null)
            {
                _context.ConsultationPrestations.Remove(consultationPrestation);
                await _context.SaveChangesAsync();
            }
        }

    }
}
