using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Controllers
{
    [Route("api/stafmedecins")]
    [ApiController]
    public class StafMedecinController : ControllerBase
    {
        private readonly IStafMedecinRepository _stafMedecinRepository;

        public StafMedecinController(IStafMedecinRepository stafMedecinRepository)
        {
            _stafMedecinRepository = stafMedecinRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StafMedecin>>> GetAllStafMedecins()
        {
            return Ok(await _stafMedecinRepository.GetAllStafMedecinsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StafMedecin>> GetStafMedecin(int id)
        {
            var stafMedecin = await _stafMedecinRepository.GetStafMedecinByIdAsync(id);
            if (stafMedecin == null) return NotFound();
            return Ok(stafMedecin);
        }

        [HttpPost]
        public async Task<ActionResult> AddStafMedecin(StafMedecin stafMedecin)
        {
            await _stafMedecinRepository.AddStafMedecinAsync(stafMedecin);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStafMedecin(int id)
        {
            await _stafMedecinRepository.DeleteStafMedecinAsync(id);
            return Ok();
        }
    }
}
