using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace serviceoncologie.Data.Models
{
    public class Consultation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateConsultation { get; set; }

        [Required]
        public double Tension { get; set; }

        [Required]
        public double Temperature { get; set; }

        [Required]
        public double Poids { get; set; } // Récupéré depuis le Patient

        [Required]
        public double Taille { get; set; } // Récupéré depuis le Patient

        public double SurfaceCorporelle => Math.Sqrt((Taille * Poids) / 3600); // Calcul automatique
        [Required]
        [Column(TypeName = "text")]
        public string? Observation { get; set; }

        // Relation Many-to-One avec Patient
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [JsonIgnore]
        public Patient? Patient { get; set; }

        // Relation Many-to-One avec Médecin (User)
        [ForeignKey("Medecin")]
        public int MedecinId { get; set; }
        [JsonIgnore]
        public User? Medecin { get; set; } // Doit avoir le rôle Médecin
        [JsonIgnore]
        // Relation One-to-Many avec Maladie
        public ICollection<Maladie>? Maladies { get; set; }
        [JsonIgnore]
        public ICollection<ConsultationMaladie>? ConsultationMaladies { get; set; }
        [JsonIgnore]
        public ICollection<DecisionStaf>? DecisionStafs { get; set; }

        // Relation avec Admission
        [JsonIgnore]
        public ICollection<Admission>? Admissions { get; set; }
        [ForeignKey("Dossier")]
        public int DossierId { get; set; }
        [JsonIgnore]
        public Dossier? Dossier { get; set; }
        [JsonIgnore]
        public ICollection<ConsultationPrestation>? ConsultationPrestations { get; set; }
        [NotMapped]
        public string? PatientNom => Patient?.Nom;

        [NotMapped]
        public string? PatientPrenom => Patient?.Prenom;

        [NotMapped]
        public string? MedecinNom => Medecin?.Nom;

        [NotMapped]
        public string? MedecinPrenom => Medecin?.Prenom;

    }
}
