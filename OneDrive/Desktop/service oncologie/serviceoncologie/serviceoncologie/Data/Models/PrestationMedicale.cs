using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace serviceoncologie.Data.Models
{
    public class PrestationMedicale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Libelle { get; set; }

        [Required]
        public int Quantite { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Unite { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Type { get; set; }

        [JsonIgnore]
        public ICollection<ConsultationPrestation>? ConsultationPrestations { get; set; }
    }
}