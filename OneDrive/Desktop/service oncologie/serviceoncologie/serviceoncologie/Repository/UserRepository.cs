using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace serviceoncologie.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            return await _db.Users
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();
        }

        public int InsertUser(User user)
        {
            _db.Users.Add(user);
            return _db.SaveChanges();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            _db.Users.Update(user);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteUserAsync(int userId)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                return 0;
            }

            _db.Users.Remove(user);
            return await _db.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public User? GetUserByUsername(string username)
        {
            return _db.Users.FirstOrDefault(u => u.Username == username);
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task UpdateAsync(User user)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Username = user.Username ?? existingUser.Username;
                existingUser.Telephone = user.Telephone ?? existingUser.Telephone;
                existingUser.Adresse = user.Adresse ?? existingUser.Adresse;
                existingUser.SituationSocial = user.SituationSocial ?? existingUser.SituationSocial;

                await _db.SaveChangesAsync(); // Sauvegarde les modifications dans la base de données
            }
        }


        // Optionnel : Méthode pour récupérer un utilisateur par son nom d'utilisateur
        
    }


}

