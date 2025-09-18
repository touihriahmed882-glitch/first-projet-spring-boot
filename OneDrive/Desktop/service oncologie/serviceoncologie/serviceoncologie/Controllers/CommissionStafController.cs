using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviceoncologie.Controllers
{
    [Route("api/commissionstafs")]
    [ApiController]
    public class CommissionStafController : ControllerBase
    {
        private readonly ICommissionStafRepository _commissionStafRepository;

        public CommissionStafController(ICommissionStafRepository commissionStafRepository)
        {
            _commissionStafRepository = commissionStafRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommissionStaf>>> GetAllCommissions()
        {
            return Ok(await _commissionStafRepository.GetAllCommissionsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommissionStaf>> GetCommission(int id)
        {
            var commission = await _commissionStafRepository.GetCommissionByIdAsync(id);
            if (commission == null) return NotFound();
            return Ok(commission);
        }

        [HttpPost]
        public async Task<ActionResult> AddCommission(CommissionStaf commission)
        {
            await _commissionStafRepository.AddCommissionAsync(commission);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommission(int id)
        {
            await _commissionStafRepository.DeleteCommissionAsync(id);
            return Ok();
        }
    }
}
