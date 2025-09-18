using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Repositories
{
    public class CommissionStafRepository : ICommissionStafRepository
    {
        private readonly AppDbContext _context;

        public CommissionStafRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommissionStaf>> GetAllCommissionsAsync()
        {
            return await _context.CommissionStafs
                .Include(cs => cs.StafMedecins)
                .ThenInclude(sm => sm.User)
                .ToListAsync();
        }

        public async Task<CommissionStaf> GetCommissionByIdAsync(int id)
        {
            return await _context.CommissionStafs
                .Include(cs => cs.StafMedecins)
                .ThenInclude(sm => sm.User)
                .FirstOrDefaultAsync(cs => cs.Id == id);
        }

        public async Task AddCommissionAsync(CommissionStaf commission)
        {
            await _context.CommissionStafs.AddAsync(commission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommissionAsync(CommissionStaf commission)
        {
            _context.CommissionStafs.Update(commission);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommissionAsync(int id)
        {
            var commission = await _context.CommissionStafs.FindAsync(id);
            if (commission != null)
            {
                _context.CommissionStafs.Remove(commission);
                await _context.SaveChangesAsync();
            }
        }
    }
}
