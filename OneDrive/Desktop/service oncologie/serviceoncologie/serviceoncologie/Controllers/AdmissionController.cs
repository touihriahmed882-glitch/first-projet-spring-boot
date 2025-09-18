using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using serviceoncologie.Data.Models;
using serviceoncologie.Repositories;
using serviceoncologie.Repository;

namespace serviceoncologie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmissionRepository _admissionRepository;

        public AdmissionController(IAdmissionRepository admissionRepository)
        {
            _admissionRepository = admissionRepository;
        }

        // ✅ Récupérer toutes les admissions
        [HttpGet]
        public ActionResult<IEnumerable<Admission>> GetAll()
        {
            var admissions = _admissionRepository.GetAll();
            return Ok(admissions);
        }

        // ✅ Récupérer une admission par ID
        [HttpGet("{id}")]
        public ActionResult<Admission> GetById(int id)
        {
            var admission = _admissionRepository.GetById(id);
            if (admission == null)
            {
                return NotFound(new { message = "Admission non trouvée" });
            }
            return Ok(admission);
        }

        // ✅ Ajouter une nouvelle admission
        [HttpPost]
        public ActionResult Add([FromBody] Admission admission)
        {
            if (admission == null)
            {
                return BadRequest(new { message = "Les données de l'admission sont invalides" });
            }

            _admissionRepository.Add(admission);
            return CreatedAtAction(nameof(GetById), new { id = admission.Id }, admission);
        }

        // ✅ Mettre à jour une admission
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Admission admission)
        {
            if (admission == null || id != admission.Id)
            {
                return BadRequest(new { message = "Données invalides pour la mise à jour" });
            }

            var existingAdmission = _admissionRepository.GetById(id);
            if (existingAdmission == null)
            {
                return NotFound(new { message = "Admission non trouvée" });
            }

            // Mise à jour des champs nécessaires
            existingAdmission.DateSortie = admission.DateSortie;
            existingAdmission.MotifSortie = admission.MotifSortie;


            _admissionRepository.Update(existingAdmission);

            return Ok(new { message = "Admission mise à jour avec succès", admission = existingAdmission });
        }
        [HttpGet("dossierAdmission/{dossierId}")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAdmissionsByDossier(int dossierId)
        {
            var Admissionss = await _admissionRepository.GetAdmissionsByDossierId(dossierId);
            return Ok(Admissionss);
        }

        // ✅ Supprimer une admission
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var admission = _admissionRepository.GetById(id);
            if (admission == null)
            {
                return NotFound(new { message = "Admission non trouvée" });
            }

            _admissionRepository.Delete(id);
            return NoContent();
        }
        [HttpGet("consultations/admission")]
        public ActionResult<IEnumerable<Consultation>> GetConsultationsWithAdmissionObservation()
        {
            var consultations = _admissionRepository.GetConsultationsWithAdmissionObservation();
            return Ok(consultations);
        }
        [HttpGet("patient/{consultationId}")]
        public ActionResult GetPatientNomPrenomByConsultationId(int consultationId)
        {
            var patientInfo = _admissionRepository.GetPatientNomPrenomByConsultationId(consultationId);

            if (patientInfo == null || !patientInfo.Any())
            {
                return NotFound(new { message = "Patient non trouvé pour cette consultation." });
            }

            return Ok(patientInfo);
        }


    }
}
