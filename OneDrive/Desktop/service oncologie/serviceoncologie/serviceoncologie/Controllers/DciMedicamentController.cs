using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Controllers
{
    [Route("api/dci-medicaments")]
    [ApiController]
    public class DciMedicamentController : ControllerBase
    {
        private readonly IDciMedicamentRepository _dciMedicamentRepository;

        public DciMedicamentController(IDciMedicamentRepository dciMedicamentRepository)
        {
            _dciMedicamentRepository = dciMedicamentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DciMedicament>>> GetAll()
        {
            return Ok(await _dciMedicamentRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DciMedicament>> GetById(int id)
        {
            var dciMedicament = await _dciMedicamentRepository.GetByIdAsync(id);
            if (dciMedicament == null) return NotFound();
            return Ok(dciMedicament);
        }

        [HttpPost]
        public async Task<ActionResult> Create(DciMedicament dciMedicament)
        {
            await _dciMedicamentRepository.AddAsync(dciMedicament);
            return CreatedAtAction(nameof(GetById), new { id = dciMedicament.Id }, dciMedicament);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, DciMedicament dciMedicament)
        {
            if (id != dciMedicament.Id) return BadRequest();
            await _dciMedicamentRepository.UpdateAsync(dciMedicament);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _dciMedicamentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
