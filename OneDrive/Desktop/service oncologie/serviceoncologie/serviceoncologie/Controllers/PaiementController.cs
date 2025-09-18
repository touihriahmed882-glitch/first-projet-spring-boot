using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Controllers
{
    [Route("api/paiements")]
    [ApiController]
    public class PaiementController : ControllerBase
    {
        private readonly IPaiementRepository _paiementRepository;

        public PaiementController(IPaiementRepository paiementRepository)
        {
            _paiementRepository = paiementRepository;
        }

        // GET: api/paiements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paiement>>> GetAllPaiements()
        {
            try
            {
                var paiements = await _paiementRepository.GetAllPaiementsAsync();
                return Ok(paiements);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Erreur serveur: {ex.Message}");
            }
        }

        // GET: api/paiements/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Paiement>> GetPaiementById(int id)
        {
            try
            {
                var paiement = await _paiementRepository.GetPaiementByIdAsync(id);
                if (paiement == null)
                {
                    return NotFound("Paiement non trouvé.");
                }
                return Ok(paiement);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Erreur serveur: {ex.Message}");
            }
        }

        // POST: api/paiements
        [HttpPost]
        public async Task<ActionResult<Paiement>> CreatePaiement([FromBody] Paiement paiement)
        {
            if (paiement == null)
            {
                return BadRequest("Données invalides.");
            }

            try
            {
                var newPaiement = await _paiementRepository.AddPaiementAsync(paiement);
                return CreatedAtAction(nameof(GetPaiementById), new { id = newPaiement.Id }, newPaiement);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Erreur serveur: {ex.Message}");
            }
        }

        // PUT: api/paiements/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Paiement>> UpdatePaiement(int id, [FromBody] Paiement paiement)
        {
            if (id != paiement.Id)
            {
                return BadRequest("ID non valide");
            }

            try
            {
                var updatedPaiement = await _paiementRepository.UpdatePaiementAsync(paiement);
                if (updatedPaiement == null)
                {
                    return NotFound("Paiement non trouvé");
                }

                return Ok(updatedPaiement);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Erreur serveur: {ex.Message}");
            }
        }

        // DELETE: api/paiements/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaiement(int id)
        {
            try
            {
                var success = await _paiementRepository.DeletePaiementAsync(id);
                if (!success)
                {
                    return NotFound("Paiement non trouvé");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Erreur serveur: {ex.Message}");
            }
        }
        [HttpGet("patients-payes")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsWithPaidStatus()
        {
            try
            {
                var patients = await _paiementRepository.GetPatientsWithPaidStatusAsync();
                if (patients == null || !patients.Any())
                {
                    return NotFound("Aucun patient trouvé avec un paiement Payé.");
                }

                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur: {ex.Message}");
            }
        }
    }
}
