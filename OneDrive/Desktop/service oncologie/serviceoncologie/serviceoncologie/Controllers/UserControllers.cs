using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Models;
using serviceoncologie.Repository;
using System.Threading.Tasks;

namespace serviceoncologie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControllers : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        // Injection de dépendance du UserRepository
        public UserControllers(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _userRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Identifiants invalides" });
            }

            var response = new
            {
                message = $"Bienvenue {user.Username}!",
                id = user.Id,
                role = user.Role,
                nom = user.Nom,
                prenom = user.Prenom,
                photoProfil = user.PhotoProfil // Assurez-vous que cette valeur est bien envoyée

            };

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest(new { error = "Données utilisateur manquantes." });
            }

            // Vérification des champs un par un avec messages spécifiques
            if (string.IsNullOrWhiteSpace(user.Username))
                return Unauthorized(new { message = "Le champ 'Username' est obligatoire." });

            if (string.IsNullOrWhiteSpace(user.Password))
                return Unauthorized(new { error = "Le champ 'Password' est obligatoire." });

            if (string.IsNullOrWhiteSpace(user.Role))
                return Unauthorized(new { error = "Le champ 'Role' est obligatoire." });

            if (string.IsNullOrWhiteSpace(user.Nom))
                return Unauthorized(new { error = "Le champ 'Nom' est obligatoire." });

            if (string.IsNullOrWhiteSpace(user.Prenom))
                return Unauthorized(new { error = "Le champ 'Prenom' est obligatoire." });

            if (user.DateDeNaissance == default)
                return Unauthorized(new { error = "Le champ 'Date de naissance' est obligatoire." });

            // Vérification si la date de naissance est aujourd'hui
            if (user.DateDeNaissance.Date == DateTime.Today)
            {
                return Unauthorized(new { error = "La date de naissance ne peut pas être aujourd'hui." });
            }

            // Vérification si l'utilisateur a moins de 30 ans
            var age = DateTime.Today.Year - user.DateDeNaissance.Year;
            if (user.DateDeNaissance.Date > DateTime.Today.AddYears(-age)) age--; // Si l'anniversaire n'est pas encore passé cette année
            if (age < 30)
            {
                return Unauthorized(new { error = "L'utilisateur doit avoir au moins 30 ans pour s'inscrire." });
            }

            if (string.IsNullOrWhiteSpace(user.NCIN))
                return Unauthorized(new { error = "Le champ 'NCIN' est obligatoire." });

            if (string.IsNullOrWhiteSpace(user.Adresse))
                return Unauthorized(new { error = "Le champ 'Adresse' est obligatoire." });

            if (string.IsNullOrWhiteSpace(user.Telephone))
                return Unauthorized(new { error = "Le champ 'Téléphone' est obligatoire." });

            if (string.IsNullOrWhiteSpace(user.Sexe))
                return Unauthorized(new { error = "Le champ 'Sexe' est obligatoire." });

            // Vérification si le Username existe déjà
            var existingByUsername = await _userRepository.GetUserByUsernameAsync(user.Username);
            if (existingByUsername != null)
            {
                return Unauthorized(new { error = "Ce nom d'utilisateur est déjà utilisé." });
            }

            // Vérification si l'utilisateur existe déjà par Username + Password
            var existingByUsernamePassword = await _userRepository.AuthenticateAsync(user.Username, user.Password);
            if (existingByUsernamePassword != null)
            {
                return Unauthorized(new { error = "Un utilisateur avec le même nom d'utilisateur et mot de passe existe déjà." });
            }

            // Vérification si l'utilisateur existe déjà par Nom + Prénom
          

            // Génération d'un ID unique si besoin
            if (user.Id == 0)
            {
                user.Id = new Random().Next(1, 100000);
            }

            // Tentative d'enregistrement de l'utilisateur
            var result = _userRepository.InsertUser(user);

            if (result > 0)
            {
                return Ok(new { message = "Utilisateur enregistré avec succès." });
            }
            else
            {
                return StatusCode(500, new { error = "Erreur lors de l'enregistrement de l'utilisateur." });
            }
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();

            if (users == null || !users.Any())
            {
                return NotFound("Aucun utilisateur trouvé.");
            }

            return Ok(users);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                if (user == null || user.Id != id)
                {
                    return BadRequest("Les données utilisateur sont incorrectes.");
                }

                var existingUser = await _userRepository.GetUserByIdAsync(id);
                if (existingUser == null)
                {
                    return NotFound("Utilisateur non trouvé.");
                }

                existingUser.Nom = user.Nom;
                existingUser.Prenom = user.Prenom;
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.Telephone = user.Telephone;
                existingUser.Role = user.Role;
                existingUser.Adresse = user.Adresse;
                existingUser.SituationSocial = user.SituationSocial;
                existingUser.PhotoProfil = user.PhotoProfil;


                var result = await _userRepository.UpdateUserAsync(existingUser);

                if (result > 0)
                {
                    return Ok("Utilisateur mis à jour avec succès.");
                }
                else
                {
                    return StatusCode(500, "Erreur lors de la mise à jour.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            return Ok(user);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userRepository.DeleteUserAsync(id);

            if (result > 0)
            {
                return Ok("Utilisateur supprimé avec succès.");
            }
            else
            {
                return NotFound("Utilisateur non trouvé.");
            }
        }
        [HttpGet("current/{username}")]
        public IActionResult GetCurrentUser(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound(new { message = "Utilisateur non trouvé" });
            }
            return Ok(user);
        }
    }
}
