using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;

namespace serviceoncologie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecisionStafController : ControllerBase
    {
        private readonly IDecisionStafRepository _decisionStafRepository;

        public DecisionStafController(IDecisionStafRepository decisionStafRepository)
        {
            _decisionStafRepository = decisionStafRepository;
        }

        // ✅ Récupérer toutes les décisions staff
        [HttpGet]
        public ActionResult<IEnumerable<DecisionStaf>> GetAll()
        {
            var decisions = _decisionStafRepository.GetAll();
            return Ok(decisions);
        }

        // ✅ Récupérer une décision staff par ID
        [HttpGet("{id}")]
        public ActionResult<DecisionStaf> GetById(int id)
        {
            var decision = _decisionStafRepository.GetById(id);
            if (decision == null)
            {
                return NotFound(new { message = "Décision staff non trouvée" });
            }

            // Ici, le DossierId devrait être automatiquement récupéré depuis la consultation dans le repository
            return Ok(decision);
        }


        // ✅ Ajouter une nouvelle décision staff
        [HttpPost]
        public ActionResult Add([FromBody] DecisionStaf decisionStaf)
        {
            if (decisionStaf == null)
            {
                return BadRequest(new { message = "Données invalides" });
            }

            _decisionStafRepository.Add(decisionStaf);  // Ajoute la décision dans le repository
            return CreatedAtAction(nameof(GetById), new { id = decisionStaf.Id }, decisionStaf);
        }


        // ✅ Mettre à jour une décision staff
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] DecisionStaf decisionStaf)
        {
            if (decisionStaf == null || id != decisionStaf.Id)
            {
                return BadRequest(new { message = "Données invalides" });
            }

            var existingDecision = _decisionStafRepository.GetById(id);
            if (existingDecision == null)
            {
                return NotFound(new { message = "Décision staff non trouvée" });
            }

            _decisionStafRepository.Update(decisionStaf);
            return NoContent();
        }

        // ✅ Supprimer une décision staff
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var decision = _decisionStafRepository.GetById(id);
            if (decision == null)
            {
                return NotFound(new { message = "Décision staff non trouvée" });
            }

            _decisionStafRepository.Delete(id);
            return NoContent();
        }
        // ✅ Récupérer toutes les décisions staff avec Observation="Admission"
        [HttpGet("with-admission")]
        public ActionResult<IEnumerable<int>> GetDecisionIdsWithAdmission()
        {
            var decisionIds = _decisionStafRepository.GetDecisionsWithAdmission();
            return Ok(decisionIds);
        }

        // ✅ Récupérer la consultation d'une décision staff avec Observation="Admission"
        [HttpGet("{id}/consultation")]
        public ActionResult<Consultation> GetConsultationByDecisionId(int id)
        {
            var consultation = _decisionStafRepository.GetConsultationByDecisionId(id);
            if (consultation == null)
            {
                return NotFound(new { message = "Aucune consultation trouvée pour cette décision staff avec Observation='Admission'" });
            }
            return Ok(consultation);
        }
        [HttpGet("by-dossier/{dossierId}")]
        public ActionResult<IEnumerable<DecisionStaf>> GetDecisionsByDossierId(int dossierId)
        {
            var decisions = _decisionStafRepository.GetDecisionsByDossierId(dossierId);
            if (decisions == null || !decisions.Any())
            {
                return NotFound(new { message = "Aucune décision staff trouvée pour ce dossier" });
            }
            return Ok(decisions);
        }

    }
}
