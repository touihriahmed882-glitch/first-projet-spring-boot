using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Repositories;
using serviceoncologie.Repository;
using System.Collections.Generic;

namespace serviceoncologie.Controllers
{
    [Route("api/consultation-maladies")]
    [ApiController]
    public class ConsultationMaladieController : ControllerBase
    {
        private readonly IConsultationMaladieRepository _repository;

        public ConsultationMaladieController(IConsultationMaladieRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAllConsultationMaladies()
        {
            var associations = _repository.GetAllConsultationMaladies();
            return Ok(associations);
        }

        // POST : Ajouter une maladie à une consultation
        [HttpPost("add")]
        public IActionResult AddMaladieToConsultation(int consultationId, int maladieId)
        {
            try
            {
                _repository.AddMaladieToConsultation(consultationId, maladieId);
                return Ok("Maladie ajoutée à la consultation avec dossier.");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Retourne une erreur si la consultation n'existe pas
            }
        }
        // GET : Récupérer les maladies d'un dossier
        [HttpGet("by-dossier/{dossierId}")]
        public ActionResult<IEnumerable<ConsultationMaladie>> GetConsultationMaladiesByDossier(int dossierId)
        {
            var consultationsMaladies = _repository.GetConsultationMaladiesByDossier(dossierId);

            if (consultationsMaladies == null || !consultationsMaladies.Any())
                return NotFound("Aucune consultation-maladie trouvée pour ce dossier.");

            return Ok(consultationsMaladies);
        }

        // DELETE : Supprimer une maladie d'une consultation
        [HttpDelete("remove")]
        public IActionResult RemoveMaladieFromConsultation(int consultationId, int maladieId)
        {
            _repository.RemoveMaladieFromConsultation(consultationId, maladieId);
            return Ok("Maladie supprimée de la consultation.");
        }

        // GET : Récupérer les maladies d'une consultation
        [HttpGet("by-consultation/{consultationId}")]
        public ActionResult<IEnumerable<Maladie>> GetMaladiesByConsultation(int consultationId)
        {
            var maladies = _repository.GetMaladiesByConsultation(consultationId);
            return Ok(maladies);
        }
        [HttpGet("by-medecin/{medecinId}")]
        public ActionResult<IEnumerable<ConsultationMaladie>> GetConsultationMaladiesByMedecin(int medecinId)
        {
            var consultationsMaladies = _repository.GetConsultationMaladiesByMedecin(medecinId);

            if (!consultationsMaladies.Any())
                return NotFound("Aucune consultation-maladie trouvée pour ce médecin.");

            return Ok(consultationsMaladies);
        }
        [HttpGet("maladies/by-dossier/{dossierId}")]
        public ActionResult<IEnumerable<Maladie>> GetMaladiesByDossier(int dossierId)
        {
            var maladies = _repository.GetMaladiesByDossier(dossierId);

            if (!maladies.Any())
                return NotFound("Aucune maladie trouvée pour ce dossier.");

            return Ok(maladies);
        }

    }
}
