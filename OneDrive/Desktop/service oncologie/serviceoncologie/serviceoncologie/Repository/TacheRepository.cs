using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public class TacheRepository : ITacheRepository
    {
        private readonly AppDbContext _context;

        public TacheRepository(AppDbContext context)
        {
            _context = context;
        }

        // Récupérer toutes les tâches
        public async Task<IEnumerable<Tache>> GetAllTachesAsync()
        {
            return await _context.Taches.ToListAsync();
        }

        // Récupérer une tâche par son ID
        public async Task<Tache> GetTacheByIdAsync(int id)
        {
            return await _context.Taches.FindAsync(id);
        }

        // Ajouter une nouvelle tâche
        public async Task<int> AddTacheAsync(Tache tache)
        {
            _context.Taches.Add(tache);
            return await _context.SaveChangesAsync();
        }

        // Mettre à jour une tâche existante
        public async Task UpdateTacheAsync(Tache tache)
        {
            _context.Taches.Update(tache); // <- ceci est OK maintenant car tache est déjà suivi
            await _context.SaveChangesAsync();
        }


        // Supprimer une tâche
        public async Task<int> DeleteTacheAsync(int id)
        {
            var tache = await _context.Taches.FindAsync(id);
            if (tache != null)
            {
                _context.Taches.Remove(tache);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        // Assigner une tâche à un utilisateur
        public async Task<int> AssignTacheToUserAsync(int userId, int tacheId)
        {
            // Vérifier si cette assignation existe déjà
            var existingAssignment = await _context.TacheUsers
                .FirstOrDefaultAsync(tu => tu.UserId == userId && tu.TacheId == tacheId);

            if (existingAssignment != null)
                return 0; // L'utilisateur a déjà cette tâche assignée

            var tacheUser = new TacheUser
            {
                UserId = userId,
                TacheId = tacheId,
                DateAdded = DateTime.Now // Ajout de la date d'ajout
            };

            _context.TacheUsers.Add(tacheUser);
            return await _context.SaveChangesAsync();
        }

        // Mettre à jour une assignation de tâche pour un utilisateur
        public async Task<int> UpdateTacheToUserAsync(int userId, int tacheId)
        {
            var existingTacheUser = await _context.TacheUsers
                .FirstOrDefaultAsync(tu => tu.UserId == userId && tu.TacheId == tacheId);

            if (existingTacheUser != null)
            {
                _context.TacheUsers.Update(existingTacheUser);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        // Supprimer une assignation de tâche pour un utilisateur
        public async Task<int> RemoveTacheFromUserAsync(int userId, int tacheId)
        {
            var tacheUser = await _context.TacheUsers
                .FirstOrDefaultAsync(tu => tu.UserId == userId && tu.TacheId == tacheId);

            if (tacheUser != null)
            {
                _context.TacheUsers.Remove(tacheUser);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        // Récupérer les tâches d'un utilisateur
        public async Task<IEnumerable<Tache>> GetTachesByUserIdAsync(int userId)
        {
            return await _context.TacheUsers
                .Where(tu => tu.UserId == userId)
                .Select(tu => tu.Tache)
                .ToListAsync();
        }

        // Remplacer une tâche d'un utilisateur
        public async Task<int> ReplaceTacheForUserAsync(int userId, int oldTacheId, int newTacheId)
        {
            var existingTacheUser = await _context.TacheUsers
                .FirstOrDefaultAsync(tu => tu.UserId == userId && tu.TacheId == oldTacheId);

            if (existingTacheUser != null)
            {
                // Supprimer l'ancienne tâche
                _context.TacheUsers.Remove(existingTacheUser);

                // Ajouter la nouvelle tâche
                var newTacheUser = new TacheUser
                {
                    UserId = userId,
                    TacheId = newTacheId,
                    DateAdded = DateTime.Now // Date d'ajout de la nouvelle tâche
                };

                _context.TacheUsers.Add(newTacheUser);
                return await _context.SaveChangesAsync();
            }
            return 0; // Aucune tâche trouvée à remplacer
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<Tache>> GetTachesByIdsAsync(List<int> tacheIds)
        {
            return await _context.Taches.Where(t => tacheIds.Contains(t.Id)).ToListAsync();
        }
        public async Task<List<int>> GetAllTacheIdsAsync()
        {
            return await _context.Taches.Select(t => t.Id).ToListAsync();
        }

        // ✅ Vérifier si une tâche est déjà assignée à l'utilisateur
        public async Task<bool> IsTacheAlreadyAssignedToUserAsync(int userId, int tacheId)
        {
            return await _context.TacheUsers
                .AnyAsync(ut => ut.UserId == userId && ut.TacheId == tacheId);
        }
        private readonly Dictionary<string, List<int>> roleTasks = new Dictionary<string, List<int>>
        {
            { "infirmier", new List<int> { 20 } },
            { "receptionniste", new List<int> { 8, 10, 13, 14, 15, 16} },
            { "administrateur", new List<int> { 1, 2, 4, 18, 12 } },
            { "medecin", new List<int> { 6, 7, 9, 11, 19 } }
        };

        public List<int> GetTasksByRole(string role)
        {
            role = role.ToLower(); // Rendre insensible à la casse
            return roleTasks.TryGetValue(role, out var tasks) ? tasks : null;
        }

    }
}