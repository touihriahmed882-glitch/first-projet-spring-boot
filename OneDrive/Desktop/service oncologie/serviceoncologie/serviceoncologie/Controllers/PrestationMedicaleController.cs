using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;

namespace serviceoncologie.Controllers
{
    [Route("api/prestations")]
    [ApiController]
    public class PrestationMedicaleController : ControllerBase
    {
        private readonly IPrestationMedicaleRepository _repository;

        public PrestationMedicaleController(IPrestationMedicaleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrestationMedicale>>> GetAllPrestations()
        {
            var prestations = await _repository.GetAllPrestationsAsync();
            return Ok(prestations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrestationMedicale>> GetPrestation(int id)
        {
            var prestation = await _repository.GetPrestationByIdAsync(id);
            if (prestation == null)
                return NotFound("Prestation non trouvée.");

            return Ok(prestation);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrestation([FromBody] PrestationMedicale prestation)
        {
            await _repository.AddPrestationAsync(prestation);
            return CreatedAtAction(nameof(GetPrestation), new { id = prestation.Id }, prestation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrestation(int id, [FromBody] PrestationMedicale prestation)
        {
            if (id != prestation.Id)
                return BadRequest("ID non valide.");

            await _repository.UpdatePrestationAsync(prestation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestation(int id)
        {
            await _repository.DeletePrestationAsync(id);
            return NoContent();
        }
    }
}
