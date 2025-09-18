using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;

namespace serviceoncologie.Controllers
{
    [Route("api/cures")]
    [ApiController]
    public class CureController : ControllerBase
    {
        private readonly ICureRepository _cureRepository;

        public CureController(ICureRepository cureRepository)
        {
            _cureRepository = cureRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cure>>> GetAllCures()
        {
            return Ok(await _cureRepository.GetAllCures());
        }
        [HttpPost("protocole/{id}")]
        public async Task<IActionResult> AddByProtocole(int id)
        {
            var cures = await _cureRepository.AddCureByDecisionStafIdAsync(id);
            return Ok(cures);
        }
        // Récupérer une cure par l'ID d'admission
        [HttpGet("byAdmission/{admissionId}")]
        public async Task<IActionResult> GetCuresByAdmissionId(int admissionId)
        {
            try
            {
                // Récupérer les cures associées à l'admission
                var cures = await _cureRepository.GetCuresByAdmissionIdAsync(admissionId);

                // Vérifier si des cures ont été trouvées
                if (cures == null || !cures.Any())
                    return NotFound(new { message = "Aucune cure trouvée pour cette admission." });

                // Retourner les cures
                return Ok(cures);
            }
            catch (Exception ex)
            {
                // En cas d'erreur, retourner un message d'erreur générique
                return BadRequest(new { message = $"Erreur : {ex.Message}" });
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Cure>> GetCureById(int id)
        {
            var cure = await _cureRepository.GetCureById(id);
            if (cure == null) return NotFound();
            return Ok(cure);
        }
        [HttpPut("{id}/valider")]
        public async Task<IActionResult> ValiderCure(int id)
        {
            var result = await _cureRepository.ValiderCureAsync(id);

            if (!result)
                return NotFound(new { message = "Cure non trouvée." });

            return Ok(new { message = "Cure validée avec succès." });
        }

        [HttpPost]
        public async Task<ActionResult> AddCure([FromBody] Cure cure)
        {
            await _cureRepository.AddCure(cure);
            return CreatedAtAction(nameof(GetCureById), new { id = cure.Id }, cure);
        }
        [HttpGet("protocole/{decisionStafId}")]
        public async Task<ActionResult<IEnumerable<Cure>>> GetCuresByProtocoleId( int decisionStafId)
        {
            var cures = await _cureRepository.GetCuresByDecisionStafIdAsync( decisionStafId);

            if (cures == null || !cures.Any())
            {
                return NotFound(new { message = "Aucune cure trouvée pour ce protocole." });
            }

            return Ok(cures);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCure(int id, [FromBody] Cure cure)
        {
            if (id != cure.Id) return BadRequest();
            await _cureRepository.UpdateCure(cure);
            return NoContent();
        }
        [HttpGet("dossierCure/{dossierId}")]
        public async Task<ActionResult<IEnumerable<Cure>>> GetCuresByDossier(int dossierId)
        {
            var Admissionss = await _cureRepository.GetCureByDossierId(dossierId);
            return Ok(Admissionss);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCure(int id)
        {
            await _cureRepository.DeleteCure(id);
            return NoContent();
        }
        [HttpDelete("protocole/{decisionStafId}")]
        public async Task<IActionResult> DeleteCuresByProtocole(int decisionStafId)
        {
            var result = await _cureRepository.DeleteCuresByDecisionStafIdAsync(decisionStafId);

            if (!result)
                return NotFound(new { message = "Aucune cure trouvée pour ce protocole." });

            return Ok(new { message = "Cures supprimées avec succès." });
        }




    }



}

