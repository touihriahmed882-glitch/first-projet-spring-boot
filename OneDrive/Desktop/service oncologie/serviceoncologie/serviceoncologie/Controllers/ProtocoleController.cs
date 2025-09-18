using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Repositories;
using serviceoncologie.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtocoleController : ControllerBase
    {
        private readonly IProtocoleRepository _protocoleRepository;

        public ProtocoleController(IProtocoleRepository protocoleRepository)
        {
            _protocoleRepository = protocoleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Protocole>>> GetAll()
        {
            var protocoles = await _protocoleRepository.GetAllAsync();
            return Ok(protocoles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Protocole>> GetById(int id)
        {
            var protocole = await _protocoleRepository.GetByIdAsync(id);
            if (protocole == null)
                return NotFound();
            return Ok(protocole);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Protocole protocole)
        {
            if (protocole == null)
                return BadRequest("Invalid data");

            await _protocoleRepository.AddAsync(protocole);
            return CreatedAtAction(nameof(GetById), new { id = protocole.Id }, protocole);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Protocole protocole)
        {
            if (id != protocole.Id)
                return BadRequest("ID mismatch");

            await _protocoleRepository.UpdateAsync(protocole);
            return NoContent();
        }
        [HttpGet("dossierProtocole/{dossierId}")]
        public async Task<ActionResult<IEnumerable<Protocole>>> GetProtocolesByDossier(int dossierId)
        {
            var Admissionss = await _protocoleRepository.GetProtocoleByDossierId(dossierId);
            return Ok(Admissionss);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _protocoleRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
