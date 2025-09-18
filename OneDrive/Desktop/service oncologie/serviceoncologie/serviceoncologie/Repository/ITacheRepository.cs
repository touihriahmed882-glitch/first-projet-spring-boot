using System.Collections.Generic;
using System.Threading.Tasks;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface ITacheRepository
    {
        Task<IEnumerable<Tache>> GetAllTachesAsync();
        Task<Tache> GetTacheByIdAsync(int id);
        Task<int> AddTacheAsync(Tache tache);
        Task UpdateTacheAsync(Tache tache);
        Task<int> DeleteTacheAsync(int id);
        Task<int> AssignTacheToUserAsync(int userId, int tacheId);
        Task<int> UpdateTacheToUserAsync(int userId, int tacheId);
        Task<int> RemoveTacheFromUserAsync(int userId, int tacheId);
        Task<IEnumerable<Tache>> GetTachesByUserIdAsync(int userId);
        Task<int> ReplaceTacheForUserAsync(int userId, int oldTacheId, int newTacheId);
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<Tache>> GetTachesByIdsAsync(List<int> tacheIds);
        Task<List<int>> GetAllTacheIdsAsync();
        Task<bool> IsTacheAlreadyAssignedToUserAsync(int userId, int tacheId);
        List<int> GetTasksByRole(string role);







    }
}
