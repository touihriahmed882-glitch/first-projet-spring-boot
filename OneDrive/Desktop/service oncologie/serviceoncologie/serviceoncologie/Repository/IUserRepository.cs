using System.Threading.Tasks;
using System.Collections.Generic;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface IUserRepository
    {
        Task<User?> AuthenticateAsync(string username, string password);
        int InsertUser(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<int> UpdateUserAsync(User user);
        Task<int> DeleteUserAsync(int userId);
        Task<User> GetUserByIdAsync(int id);
        User? GetUserByUsername(string username);
        Task<User> GetUserByUsernameAsync(string username);
      
        Task UpdateAsync(User user);
      
    }
}
