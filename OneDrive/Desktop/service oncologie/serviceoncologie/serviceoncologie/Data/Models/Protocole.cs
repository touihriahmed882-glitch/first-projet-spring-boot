using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace serviceoncologie.Data.Models
{
    public class Protocole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string NomProtocole { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string TypeProtocole { get; set; } = string.Empty;

        [Required]
        public int DureeProtocole { get; set; } // Durée en jours, semaines, etc.

        [Required]
        public int NombreCure { get; set; } // Nombre de cures

        
       


        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Rythme { get; set; } = string.Empty; // Ex: "Hebdomadaire", "Mensuel", etc.

        // Relation Many-to-One avec DecisionStaf
       
        [JsonIgnore]
        public ICollection<Cure>? Cures { get; set; }

       

    }
}
