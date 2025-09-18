using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Controllers
{
    [Route("api/dcis")]
    [ApiController]
    public class DciController : ControllerBase
    {
        private readonly IDciRepository _dciRepository;

        public DciController(IDciRepository dciRepository)
        {
            _dciRepository = dciRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dci>>> GetAll()
        {
            return Ok(await _dciRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dci>> GetById(int id)
        {
            var dci = await _dciRepository.GetByIdAsync(id);
            if (dci == null) return NotFound();
            return Ok(dci);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Dci dci)
        {
            await _dciRepository.AddAsync(dci);
            return CreatedAtAction(nameof(GetById), new { id = dci.Id }, dci);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Dci dci)
        {
            if (id != dci.Id) return BadRequest();
            await _dciRepository.UpdateAsync(dci);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _dciRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
