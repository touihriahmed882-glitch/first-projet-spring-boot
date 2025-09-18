using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;

namespace serviceoncologie.Controllers
{
    [Route("api/dossiers")]
    [ApiController]
    public class DossierController : ControllerBase
    {
        private readonly IDossierRepository _dossierRepository;

        public DossierController(IDossierRepository dossierRepository)
        {
            _dossierRepository = dossierRepository;
        }

        // ✅ Ajouter un dossier
        [HttpPost("ajouter")]
        public IActionResult AjouterDossier([FromBody] Dossier dossier)
        {
            _dossierRepository.AjouterDossier(dossier);
            return Ok(new { message = "Dossier ajouté avec succès !" });
        }

        // ✅ Supprimer un dossier
        [HttpDelete("supprimer/{id}")]
        public IActionResult SupprimerDossier(int id)
        {
            _dossierRepository.SupprimerDossier(id);
            return Ok(new { message = "Dossier supprimé avec succès !" });
        }


        // ✅ Récupérer un dossier avec les détails des consultations et décisions staf
        [HttpGet("details/{id}")]
        public IActionResult ObtenirDossierAvecDetails(int id)
        {
            var dossier = _dossierRepository.ObtenirDossierAvecDetails(id);
            if (dossier == null)
            {
                return NotFound(new { message = "Dossier non trouvé !" });
            }
            return Ok(dossier);
        }
        [HttpGet("par-patient")]
        public IActionResult ObtenirDossierParNomPrenomPatient([FromQuery] string nom, [FromQuery] string prenom)
        {
            var dossier = _dossierRepository.ObtenirDossierParNomPrenomPatient(nom, prenom);
            if (dossier == null)
            {
                return NotFound(new { message = "Aucun dossier trouvé pour ce patient !" });
            }
            return Ok(dossier);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dossier>>> GetAllDossiers()
        {
            var dossiers = await _dossierRepository.GetAllDossiers();
            return Ok(dossiers);
        }
    }
}
