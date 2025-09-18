using System;
using Microsoft.AspNetCore.Mvc;
using serviceoncologie.Repository;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Controllers
{
    [Route("api/statistiques")]
    [ApiController]
    public class StatistiquesController : ControllerBase
    {
        private readonly IStatistiquesRepository _statistiquesRepository;

        public StatistiquesController(IStatistiquesRepository statistiquesRepository)
        {
            _statistiquesRepository = statistiquesRepository;
        }

        [HttpGet("generales")]
        public ActionResult<Statistiques> GetStatistiquesGenerales()
        {
            var stats = _statistiquesRepository.GetStatistiquesGenerales();
            return Ok(stats);
        }

        [HttpGet("patients-par-jour")]
        public ActionResult<int> GetNombrePatientsParJour([FromQuery] DateTime date)
        {
            var nombre = _statistiquesRepository.GetNombrePatientsParJour(date);
            return Ok(nombre);
        }

        [HttpGet("consultations-par-jour")]
        public ActionResult<int> GetNombreConsultationsParJour([FromQuery] DateTime date)
        {
            var nombre = _statistiquesRepository.GetNombreConsultationsParJour(date);
            return Ok(nombre);
        }
        [HttpGet("pourcentage-role")]
        public ActionResult<double> GetPourcentageRole([FromQuery] string role)
        {
            var pourcentage = _statistiquesRepository.GetPourcentageRole(role);
            return Ok(pourcentage);
        }

        // 🔹 Récupérer le pourcentage des admissions par rapport aux consultations
        [HttpGet("pourcentage-admissions")]
        public ActionResult<double> GetPourcentageAdmissionsParConsultations()
        {
            var pourcentage = _statistiquesRepository.GetPourcentageAdmissionsParConsultations();
            return Ok(pourcentage);
        }
        [HttpGet("patients-par-categorie")]
        public IActionResult GetNombrePatientsParCategorie()
        {
            var result = _statistiquesRepository.GetNombrePatientsParCategorie();
            return Ok(result);
        }
        [HttpGet("consultations-par-maladie")]
        public ActionResult<Dictionary<string, int>> GetNombreConsultationsParMaladie()
        {
            var statistiques = _statistiquesRepository.GetNombreConsultationsParMaladie();
            return Ok(statistiques);
        }

    }
}
