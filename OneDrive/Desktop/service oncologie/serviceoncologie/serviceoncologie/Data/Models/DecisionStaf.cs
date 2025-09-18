using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace serviceoncologie.Data.Models
{
    public class DecisionStaf
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Observation { get; set; }

        // Relation Many-to-One avec Consultation
        [ForeignKey("Consultation")]
        public int ConsultationId { get; set; }
        [JsonIgnore]
        public Consultation? Consultation { get; set; }

        // Relation Many-to-One avec CommissionStaf
        [ForeignKey("CommissionStaf")]
        public int CommissionStafId { get; set; }
        [JsonIgnore]
        public CommissionStaf? CommissionStaf { get; set; }
        // Ajout de la relation Many-to-One avec Protocole
        [ForeignKey("Protocole")]
        public int? ProtocoleId { get; set; }

        [JsonIgnore]
        public Protocole? Protocole { get; set; }




        // Relation One-to-Many avec Admission
        [JsonIgnore]
        public ICollection<Admission>? Admissions { get; set; } = new List<Admission>();
        [JsonIgnore]
        [ForeignKey("Dossier")]
        public int DossierId { get; set; }
        [JsonIgnore]
        public Dossier? Dossier { get; set; }
        [JsonIgnore]
        public ICollection<Cure>? Cures { get; set; }

        [NotMapped]
        public string? PatientNom => Consultation?.Patient?.Nom;

        [NotMapped]
        public string? PatientPrenom => Consultation?.Patient?.Prenom;

    }
}