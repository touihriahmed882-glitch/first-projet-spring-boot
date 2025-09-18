using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;

namespace serviceoncologie.Controllers
{
    [Route("api/medicaments")]
    [ApiController]
    public class MedicamentController : ControllerBase
    {
        private readonly IMedicamentRepository _medicamentRepository;

        public MedicamentController(IMedicamentRepository medicamentRepository)
        {
            _medicamentRepository = medicamentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicament>>> GetAllMedicaments()
        {
            return Ok(await _medicamentRepository.GetAllMedicaments());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Medicament>> GetMedicamentById(int id)
        {
            var medicament = await _medicamentRepository.GetMedicamentById(id);
            if (medicament == null) return NotFound();
            return Ok(medicament);
        }

        [HttpPost]
        public async Task<ActionResult> AddMedicament([FromBody] Medicament medicament)
        {
            await _medicamentRepository.AddMedicament(medicament);
            return CreatedAtAction(nameof(GetMedicamentById), new { id = medicament.Id }, medicament);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMedicament(int id, [FromBody] Medicament medicament)
        {
            if (id != medicament.Id) return BadRequest();
            await _medicamentRepository.UpdateMedicament(medicament);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMedicament(int id)
        {
            await _medicamentRepository.DeleteMedicament(id);
            return NoContent();
        }
    }
}
