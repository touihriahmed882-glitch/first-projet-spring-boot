using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using serviceoncologie.Data.Models;

[Route("api/patients")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientRepository _repository;

    public PatientController(IPatientRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<Patient>> GetAllPatients()
    {
        return await _repository.GetAllPatients();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatientById(int id)
    {
        var patient = await _repository.GetPatientById(id);
        if (patient == null) return NotFound();
        return Ok(patient);
    }


    [HttpPost]
    public async Task<ActionResult> AddPatient([FromBody] Patient patient)
    {
        await _repository.AddPatient(patient);
        return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient patient)
    {
        if (id != patient.Id) return BadRequest();

        await _repository.UpdatePatient(patient);
        return NoContent();
    }
    [HttpGet("dossier/{dossierId}")]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsByDossier(int dossierId)
    {
        var patients = await _repository.GetPatientsByDossierId(dossierId);
        return Ok(patients);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        await _repository.DeletePatient(id);
        return NoContent();
    }
}
