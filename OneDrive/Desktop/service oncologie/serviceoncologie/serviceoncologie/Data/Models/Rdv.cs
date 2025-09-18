using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace serviceoncologie.Data.Models
{
    public class Rdv
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateRdv { get; set; }

        [Required]
        public string Etat { get; set; } // "En attente", "Confirmé", "Annulé"

        public string Observation { get; set; }

        // Relation avec User (Médecin)
        [ForeignKey("Medecin")]
        public int MedecinId { get; set; }
        [JsonIgnore]

        public User? Medecin { get; set; }

        // Relation avec DemandeRdv
        
        [JsonIgnore]
        public Paiement? Paiement { get; set; }
        // Relation avec Patient
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [JsonIgnore]
        public Patient? Patient { get; set; }


    }
}
