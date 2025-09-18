using serviceoncologie.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Repositories
{
    public interface ICommissionStafRepository
    {
        Task<IEnumerable<CommissionStaf>> GetAllCommissionsAsync();
        Task<CommissionStaf> GetCommissionByIdAsync(int id);
        Task AddCommissionAsync(CommissionStaf commission);
        Task UpdateCommissionAsync(CommissionStaf commission);
        Task DeleteCommissionAsync(int id);
    }
}
