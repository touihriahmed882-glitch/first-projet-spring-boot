using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Repositories;
using serviceoncologie.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Controllers
{
    [Route("api/rdvs")]
    [ApiController]
    public class RdvController : ControllerBase
    {
        private readonly IRdvRepository _rdvRepository;
        private readonly IUserRepository _userRepository;


        public RdvController(IRdvRepository rdvRepository, IUserRepository userRepository)
        {
            _rdvRepository = rdvRepository;
            _userRepository = userRepository;


        }

        // Récupérer tous les RDVs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rdv>>> GetAllRdvs()
        {
            var rdvs = await _rdvRepository.GetAllRdvsAsync();
            return Ok(rdvs);
        }

        // Récupérer un RDV par son ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Rdv>> GetRdvById(int id)
        {
            var rdv = await _rdvRepository.GetRdvByIdAsync(id);
            if (rdv == null) return NotFound("Rendez-vous non trouvé");
            return Ok(rdv);
        }

        // Récupérer les RDVs d'un médecin
        [HttpGet("medecin/{medecinId}")]
        public async Task<ActionResult<IEnumerable<Rdv>>> GetRdvsByMedecin(int medecinId)
        {
            var rdvs = await _rdvRepository.GetRdvsByMedecinIdAsync(medecinId);
            return Ok(rdvs);
        }

        // Ajouter un nouveau RDV
        [HttpPost]
        public async Task<ActionResult<Rdv>> CreateRdv([FromBody] Rdv rdv)
        {
            if (rdv == null) return BadRequest("Données invalides.");

            // Vérifier que l'utilisateur est un médecin
            var medecin = await _userRepository.GetUserByIdAsync(rdv.MedecinId);
            if (medecin == null || medecin.Role != "Medecin")
            {
                return BadRequest("Seuls les médecins peuvent être assignés à un rendez-vous.");
            }

            var newRdv = await _rdvRepository.AddRdvAsync(rdv);
            return CreatedAtAction(nameof(GetRdvById), new { id = newRdv.Id }, newRdv);
        }


        // Mettre à jour un RDV
        [HttpPut("{id}")]
        public async Task<ActionResult<Rdv>> UpdateRdv(int id, [FromBody] Rdv rdv)
        {
            if (id != rdv.Id) return BadRequest("ID non valide");

            var updatedRdv = await _rdvRepository.UpdateRdvAsync(rdv);
            if (updatedRdv == null) return NotFound("Rendez-vous non trouvé");

            return Ok(updatedRdv);
        }

        // Supprimer un RDV
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRdv(int id)
        {
            var success = await _rdvRepository.DeleteRdvAsync(id);
            if (!success) return NotFound("Rendez-vous non trouvé");

            return NoContent();
        }
        // Récupérer l'ID, le nom et le prénom du patient associé à un RDV
        [HttpGet("{id}/patient")]
        public async Task<ActionResult> GetPatientForRdv(int id)
        {
            var rdv = await _rdvRepository.GetRdvByIdAsync(id);

            if (rdv == null) return NotFound("Rendez-vous non trouvé");

            var patient = rdv.Patient;

            if (patient == null) return NotFound("Aucun patient associé à ce rendez-vous");

            var patientInfo = new
            {
                PatientId = patient.Id,
                PatientNom = patient.Nom,
                PatientPrenom = patient.Prenom,

            };

            return Ok(patientInfo);
        }
        // Récupérer les RDVs d'un patient
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<Rdv>>> GetRdvsByPatientId(int patientId)
        {
            var rdvs = await _rdvRepository.GetRdvsByPatientIdAsync(patientId);

            if (rdvs == null || !rdvs.Any())
            {
                return NotFound("Aucun rendez-vous trouvé pour ce patient.");
            }

            return Ok(rdvs);
        }


    }
}
