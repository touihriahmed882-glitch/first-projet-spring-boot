using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace serviceoncologie.Data.Models
{
    public class Paiement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DatePaiement { get; set; }

        [Required]
        public double Montant { get; set; }

        [Required]
        public string MethodePaiement { get; set; } // Ex: Carte bancaire, Espèces, Virement

        [Required]
        public string Statut { get; set; } // Ex: "Payé", "En attente", "Échoué"

        // Relation avec le rendez-vous
        [ForeignKey("Rdv")]
        public int RdvId { get; set; }
        [JsonIgnore]
        public Rdv? Rdv { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [JsonIgnore]
        public Patient? Patient { get; set; }
    }
}
