using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;
using System.Collections.Generic;

namespace serviceoncologie.Controllers
{
    [Route("api/consultations")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        private readonly IConsultationRepository _repository;

        public ConsultationController(IConsultationRepository repository)
        {
            _repository = repository;
        }

        // GET: api/consultations
        [HttpGet]
        public ActionResult<IEnumerable<Consultation>> GetAll()
        {
            return Ok(_repository.GetAll());
        }
        [HttpGet("{id}/noms")]
        public IActionResult GetNomPrenomMedecinEtPatient(int id)
        {
            var result = _repository.GetNomPrenomMedecinEtPatient(id);
            if (result == null)
                return NotFound("Consultation, patient ou médecin non trouvés.");

            return Ok(new
            {
                MedecinNom = result?.MedecinNom,
                MedecinPrenom = result?.MedecinPrenom,
                PatientNom = result?.PatientNom,
                PatientPrenom = result?.PatientPrenom
            });
        }


        // GET: api/consultations/{id}
        [HttpGet("{id}")]
        public ActionResult<Consultation> GetById(int id)
        {
            var consultation = _repository.GetById(id);
            if (consultation == null)
                return NotFound();
            return Ok(consultation);
        }

        // POST: api/consultations
        [HttpPost]
        public ActionResult<Consultation> Create([FromBody] Consultation consultation)
        {
            var newConsultation = _repository.Create(consultation);
            if (newConsultation == null)
                return BadRequest("Le paiement du rendez-vous du patient n'est pas effectué.");
            return CreatedAtAction(nameof(GetById), new { id = newConsultation.Id }, newConsultation);
        }

        // PUT: api/consultations/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Consultation consultation)
        {
            var existingConsultation = _repository.GetById(id);
            if (existingConsultation == null)
                return NotFound();

            consultation.Id = id;
            _repository.Update(consultation);
            return NoContent();
        }

        // DELETE: api/consultations/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var consultation = _repository.GetById(id);
            if (consultation == null)
                return NotFound();

            _repository.Delete(id);
            return NoContent();
        }
        [HttpGet("patients/{medecinId}")]
        public ActionResult<IEnumerable<Patient>> GetPatientsByMedecin(int medecinId)
        {
            var patients = _repository.GetPatientsByMedecin(medecinId);
            if (patients == null || !patients.Any())
                return NotFound("Aucun patient trouvé pour ce médecin.");

            return Ok(patients);
        }
        // GET: api/consultations/medecin/{medecinId}
        [HttpGet("medecin/{medecinId}")]
        public ActionResult<IEnumerable<Consultation>> GetConsultationsByMedecin(int medecinId)
        {
            var consultations = _repository.GetConsultationsByMedecin(medecinId);
            if (consultations == null || !consultations.Any())
                return NotFound("Aucune consultation trouvée pour ce médecin.");

            return Ok(consultations);
        }
        // GET: api/consultations/dossier/{dossierId}
        [HttpGet("dossier/{dossierId}")]
        public ActionResult<IEnumerable<Consultation>> GetConsultationsByDossier(int dossierId)
        {
            var consultations = _repository.GetConsultationsByDossier(dossierId);
            if (consultations == null || !consultations.Any())
                return NotFound("Aucune consultation trouvée pour ce dossier.");

            return Ok(consultations);
        }
        [HttpGet("medecins")]
        public ActionResult<IEnumerable<User>> GetAllMedecins()
        {
            var medecins = _repository.GetAllMedecins();
            if (medecins == null || !medecins.Any())
                return NotFound("Aucun médecin trouvé.");

            return Ok(medecins);
        }




    }
}
