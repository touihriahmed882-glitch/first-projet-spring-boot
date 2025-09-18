using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace serviceoncologie.Data.Models
{
    public class Cure
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateCure { get; set; }

        public string Observation { get; set; }

        // Clé étrangère vers Protocole
        [Required]
        public int ProtocoleId { get; set; }
        [JsonIgnore]
        public Protocole? Protocole { get; set; }
        public bool EstValidee { get; set; } // Pour indiquer si la cure a été validée ou non


        // Clé étrangère vers Medicament
        [Required]
        public int MedicamentId { get; set; }
        [JsonIgnore]
        public Medicament? Medicament { get; set; }
        [Required]
        public int DossierId { get; set; }
        [JsonIgnore]
        public Dossier? Dossier { get; set; }
        [Required]
        public int DecisionStafId { get; set; }
        [JsonIgnore]
        public DecisionStaf? DecisionStaf { get; set; }
        [Required]
        public int AdmissionId { get; set; }
        [JsonIgnore]
        public Admission? Admission { get; set; }
    }
}
