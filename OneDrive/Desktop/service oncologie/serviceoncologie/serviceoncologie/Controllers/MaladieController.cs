using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using serviceoncologie.Data.Models;
using serviceoncologie.Repository;
using Microsoft.EntityFrameworkCore;

namespace serviceoncologie.Controllers
{
    [Route("api/maladies")]
    [ApiController]
    public class MaladieController : ControllerBase
    {
        private readonly IMaladieRepository _maladieRepository;

        public MaladieController(IMaladieRepository maladieRepository)
        {
            _maladieRepository = maladieRepository;
        }

        // Liste des maladies
        [HttpGet]
        public ActionResult<IEnumerable<Maladie>> GetAllMaladies()
        {
            return Ok(_maladieRepository.GetAll());
        }

        // Récupérer une maladie par ID
        [HttpGet("{id}")]
        public ActionResult<Maladie> GetMaladieById(int id)
        {
            var maladie = _maladieRepository.GetById(id);
            if (maladie == null)
            {
                return NotFound();
            }
            return Ok(maladie);
        }

        // Ajouter une nouvelle maladie (accessible à tous)
        [HttpPost]
        public ActionResult<Maladie> AddMaladie([FromBody] Maladie maladie)
        {
            // Tu n'as plus de validation basée sur les rôles ici
            _maladieRepository.Add(maladie);
            return CreatedAtAction(nameof(GetMaladieById), new { id = maladie.Id }, maladie);
        }

        // Modifier une maladie (accessible à tous)
        [HttpPut("{id}")]
        public IActionResult UpdateMaladie(int id, [FromBody] Maladie maladie)
        {
            var existingMaladie = _maladieRepository.GetById(id);
            if (existingMaladie == null)
            {
                return NotFound();
            }

            existingMaladie.Nom = maladie.Nom;
            existingMaladie.codecim = maladie.codecim;
            existingMaladie.Description = maladie.Description;

            _maladieRepository.Update(existingMaladie);
            return NoContent();
        }

        // Supprimer une maladie (accessible à tous)
        [HttpDelete("{id}")]
        public IActionResult DeleteMaladie(int id)
        {
            var maladie = _maladieRepository.GetById(id);
            if (maladie == null)
            {
                return NotFound();
            }

            _maladieRepository.Delete(id);
            return NoContent();
        }
        [HttpGet("medecin/{medecinId}")]
        public async Task<ActionResult<IEnumerable<Maladie>>> GetMaladiesByMedecin(int medecinId)
        {
            // Utilisation du référentiel pour récupérer les maladies par médecin
            var maladies = await _maladieRepository.GetMaladiesByMedecinIdAsync(medecinId);

            if (maladies == null || !maladies.Any())
            {
                return NotFound();
            }

            return Ok(maladies);
        }

    }
}
