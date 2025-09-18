using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace serviceoncologie.Data.Models
{
    public class Dossier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NumeroDossier { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        // Relation One-to-Many avec Consultation
        [JsonIgnore]
        public ICollection<Consultation>? Consultations { get; set; } = new List<Consultation>();

        // Relation One-to-Many avec DecisionStaf
        [JsonIgnore]
        public ICollection<DecisionStaf>? DecisionStafs { get; set; } = new List<DecisionStaf>();
        [JsonIgnore]
        public ICollection<ConsultationMaladie>? ConsultationMaladies { get; set; } = new List<ConsultationMaladie>();
        [JsonIgnore]
        public ICollection<Patient>? Patients { get; set; } = new List<Patient>();
        [JsonIgnore]
        public ICollection<Admission>? Admissions { get; set; } = new List<Admission>();
        // Liste des protocoles liés au dossier
        [JsonIgnore]
        public ICollection<Cure>? Cures { get; set; } = new List<Cure>();

    }
}
