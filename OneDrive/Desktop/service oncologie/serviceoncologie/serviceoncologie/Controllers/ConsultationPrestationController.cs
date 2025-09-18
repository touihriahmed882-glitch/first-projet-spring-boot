using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;

namespace serviceoncologie.Controllers
{
    [Route("api/consultation-prestations")]
    [ApiController]
    public class ConsultationPrestationController : ControllerBase
    {
        private readonly IConsultationPrestationRepository _repository;

        public ConsultationPrestationController(IConsultationPrestationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultationPrestation>>> GetAllConsultationPrestations()
        {
            var consultationPrestations = await _repository.GetAllConsultationPrestationsAsync();
            return Ok(consultationPrestations);
        }

        [HttpGet("by-consultation/{consultationId}")]
        public async Task<ActionResult<IEnumerable<PrestationMedicale>>> GetPrestationsByConsultation(int consultationId)
        {
            var prestations = await _repository.GetPrestationsByConsultationIdAsync(consultationId);
            return Ok(prestations);
        }

        [HttpPost]
        public async Task<IActionResult> AddConsultationPrestation([FromBody] ConsultationPrestation consultationPrestation)
        {
            // Ajout de la consultation prestation dans la base de données
            await _repository.AddConsultationPrestationAsync(consultationPrestation);

            // Récupérer l'objet avec l'ID généré après l'insertion
            var createdConsultationPrestation = await _repository.GetConsultationPrestationByIdAsync(consultationPrestation.Id);

            // Retourner l'objet créé avec l'ID auto-généré
            return CreatedAtAction(nameof(GetAllConsultationPrestations), new { id = createdConsultationPrestation.Id }, createdConsultationPrestation);
        }

        [HttpDelete("{consultationId}/{prestationId}")]
        public async Task<IActionResult> RemoveConsultationPrestation(int consultationId, int prestationId)
        {
            await _repository.RemoveConsultationPrestationAsync(consultationId, prestationId);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveConsultationPrestationById(int id)
        {
            await _repository.RemoveConsultationPrestationByIdAsync(id);
            return NoContent();
        }

    }
}
