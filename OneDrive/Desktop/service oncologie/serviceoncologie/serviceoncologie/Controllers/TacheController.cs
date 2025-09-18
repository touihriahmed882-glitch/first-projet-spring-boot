using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace serviceoncologie.Controllers
{
    [Route("api/taches")]
    [ApiController]
    public class TacheController : ControllerBase
    {
        private readonly ITacheRepository _tacheRepository;

        public TacheController(ITacheRepository tacheRepository)
        {
            _tacheRepository = tacheRepository;
        }

        // Récupérer toutes les tâches
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Tache>>> GetAllTaches()
        {
            var taches = await _tacheRepository.GetAllTachesAsync();
            return Ok(taches);
        }

        // Récupérer une tâche par ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Tache>> GetTacheById(int id)
        {
            var tache = await _tacheRepository.GetTacheByIdAsync(id);
            if (tache == null)
                return NotFound();

            return Ok(tache);
        }

        // Ajouter une nouvelle tâche
        [HttpPost("add")]
        public async Task<ActionResult> AddTache([FromBody] Tache tache)
        {
            if (tache == null)
                return BadRequest("Données invalides.");

            await _tacheRepository.AddTacheAsync(tache);
            return CreatedAtAction(nameof(GetTacheById), new { id = tache.Id }, tache);
        }

        // Mettre à jour une tâche
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTache(int id, [FromBody] Tache tache)
        {
            if (tache == null)
                return BadRequest("Données invalides.");

            var existingTache = await _tacheRepository.GetTacheByIdAsync(id);
            if (existingTache == null)
                return NotFound();

            // Met à jour uniquement les champs modifiables
            existingTache.Libelle = tache.Libelle;

            await _tacheRepository.UpdateTacheAsync(existingTache);
            return NoContent();
        }



        // Supprimer une tâche
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTache(int id)
        {
            var result = await _tacheRepository.DeleteTacheAsync(id);
            if (result == 0)
                return NotFound();

            return NoContent();
        }

        // Assigner une tâche à un utilisateur
        [HttpPost("assign/{userId}/{tacheId}")]
        public async Task<IActionResult> AssignTacheToUser(int userId, int tacheId)
        {
            if (userId <= 0 || tacheId <= 0)
            {
                Console.WriteLine($"❌ Erreur : ID utilisateur ({userId}) ou ID tâche ({tacheId}) invalide.");
                return BadRequest("Les IDs utilisateur et tâche doivent être valides.");
            }

            var user = await _tacheRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                Console.WriteLine($"❌ Erreur : Utilisateur {userId} non trouvé.");
                return NotFound($"Utilisateur avec l'ID {userId} non trouvé.");
            }

            var tache = await _tacheRepository.GetTacheByIdAsync(tacheId);
            if (tache == null)
            {
                Console.WriteLine($"❌ Erreur : Tâche {tacheId} non trouvée.");
                return NotFound($"Tâche avec l'ID {tacheId} non trouvée.");
            }

            Dictionary<string, List<int>> roleTaches = new Dictionary<string, List<int>>
    {
        { "administrateur", new List<int> { 1, 2, 3, 4, 5, 18, 12, } },
        { "medecin", new List<int> { 6, 7, 9, 11, 19 } },
        { "infirmier", new List<int> { 20 } },
        { "receptionniste", new List<int> { 8, 10, 13, 14, 15, 16} }
    };

            List<int> tacheIds;

            if (string.IsNullOrEmpty(user.Role))
            {
                Console.WriteLine($"ℹ️ Utilisateur {userId} sans rôle. Toutes les tâches sont accessibles.");
                tacheIds = await _tacheRepository.GetAllTacheIdsAsync();
            }
            else
            {
                string role = user.Role.ToLower();

                if (!roleTaches.ContainsKey(role))
                {
                    Console.WriteLine($"❌ Erreur : Rôle {user.Role} inconnu.");
                    return BadRequest($"Rôle '{user.Role}' inconnu. Impossible d'assigner la tâche.");
                }

                tacheIds = roleTaches[role];
            }

            if (!tacheIds.Contains(tacheId))
            {
                Console.WriteLine($"❌ Erreur : Rôle {user.Role} ne peut pas recevoir la tâche {tacheId}.");
                return BadRequest($"L'utilisateur (Rôle: '{user.Role ?? "Aucun"}') ne peut pas être assigné à la tâche {tacheId}.");
            }

            bool isAlreadyAssigned = await _tacheRepository.IsTacheAlreadyAssignedToUserAsync(userId, tacheId);
            if (isAlreadyAssigned)
            {
                Console.WriteLine($"❌ Erreur : Tâche {tacheId} déjà assignée à l'utilisateur {userId}.");
                return BadRequest($"La tâche {tacheId} est déjà assignée à l'utilisateur {userId}.");
            }

            var result = await _tacheRepository.AssignTacheToUserAsync(userId, tacheId);
            if (result == 0)
            {
                Console.WriteLine($"❌ Erreur : Impossible d'assigner la tâche {tacheId} à l'utilisateur {userId}.");
                return BadRequest("Impossible d'assigner la tâche.");
            }

            Console.WriteLine($"✅ Succès : Tâche {tacheId} assignée à l'utilisateur {userId}.");
            return Ok($"Tâche {tacheId} assignée avec succès à l'utilisateur {userId} ({user.Role ?? "Aucun rôle"}).");
        }



        // Mettre à jour l'assignation d'une tâche à un utilisateur


        [HttpDelete("assign/{userId}/{tacheId}")]
        public async Task<ActionResult> RemoveTacheFromUser(int userId, int tacheId)
        {
            var result = await _tacheRepository.RemoveTacheFromUserAsync(userId, tacheId);
            if (result == 0)
                return NotFound();

            return Ok("Assignation supprimée.");
        }
        [HttpGet("user/{userId}/taches")]
        public async Task<IActionResult> GetTachesByUserId(int userId)
        {
            var taches = await _tacheRepository.GetTachesByUserIdAsync(userId);
            if (taches == null || !taches.Any())
            {
                return NotFound($"Aucune tâche trouvée pour l'utilisateur avec l'ID {userId}.");
            }
            return Ok(taches);
        }
        [HttpPut("user/{userId}/replace/{oldTacheId}/{newTacheId}")]
        public async Task<IActionResult> ReplaceTacheForUser(int userId, int oldTacheId, int newTacheId)
        {
            if (userId <= 0 || oldTacheId <= 0 || newTacheId <= 0)
            {
                return BadRequest("Les IDs utilisateur et tâches doivent être valides.");
            }

            var user = await _tacheRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Utilisateur avec l'ID {userId} non trouvé.");
            }

            var oldTache = await _tacheRepository.GetTacheByIdAsync(oldTacheId);
            if (oldTache == null)
            {
                return NotFound($"Ancienne tâche avec l'ID {oldTacheId} non trouvée.");
            }

            var newTache = await _tacheRepository.GetTacheByIdAsync(newTacheId);
            if (newTache == null)
            {
                return NotFound($"Nouvelle tâche avec l'ID {newTacheId} non trouvée.");
            }

            // Vérifier si l'utilisateur possède déjà l'ancienne tâche
            var userTaches = await _tacheRepository.GetTachesByUserIdAsync(userId);
            if (!userTaches.Any(t => t.Id == oldTacheId))
            {
                return BadRequest($"L'utilisateur {userId} ne possède pas la tâche {oldTacheId}.");
            }

            // Vérifier si la nouvelle tâche est autorisée pour le rôle de l'utilisateur
            Dictionary<string, List<int>> roleTaches = new Dictionary<string, List<int>>
    {
        { "administrateur", new List<int> { 1, 2, 3, 4, 5, 18, 12 } },
        { "medecin", new List<int> { 6, 7, 9, 11, 19 } },
        { "infirmier", new List<int> { 20 } },
        { "receptionniste", new List<int> { 8, 10, 13, 14, 15, 16} }
    };

            string role = user.Role.ToLower();
            if (!roleTaches.ContainsKey(role) || !roleTaches[role].Contains(newTacheId))
            {
                return BadRequest($"Le rôle '{user.Role}' ne peut pas être assigné à la tâche {newTacheId}.");
            }

            var result = await _tacheRepository.ReplaceTacheForUserAsync(userId, oldTacheId, newTacheId);
            if (result > 0)
            {
                return Ok($"Tâche {oldTacheId} remplacée par {newTacheId} pour l'utilisateur {userId}.");
            }

            return BadRequest("Erreur lors du remplacement de la tâche.");
        }

        [HttpGet("user/{userId}/taches-by-role")]
        public async Task<IActionResult> GetTachesByUserRole(int userId)
        {
            var user = await _tacheRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Utilisateur avec l'ID {userId} non trouvé.");
            }

            List<int> tacheIds;
            switch (user.Role.ToLower())
            {
                case "administrateur":
                    tacheIds = new List<int> { 1, 2, 3, 4, 5, 18, 12 };
                    break;
                case "medecin":
                    tacheIds = new List<int> { 6, 7, 9, 11, 19 };
                    break;
                case "infirmier":
                    tacheIds = new List<int> { 20 };
                    break;
                case "receptionniste":
                    tacheIds = new List<int> { 8, 10, 13, 14, 15, 16};
                    break;
                default:
                    return BadRequest("Rôle utilisateur inconnu.");
            }

            var taches = await _tacheRepository.GetTachesByIdsAsync(tacheIds);
            return Ok(taches);
        }
        [HttpGet("role/{role}")]
        public IActionResult GetTasksByRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return BadRequest("Le rôle est requis.");
            }

            var tasks = _tacheRepository.GetTasksByRole(role);

            if (tasks == null)
            {
                return NotFound($"Aucune tâche trouvée pour le rôle : {role}");
            }

            return Ok(tasks);
        }




    }
}