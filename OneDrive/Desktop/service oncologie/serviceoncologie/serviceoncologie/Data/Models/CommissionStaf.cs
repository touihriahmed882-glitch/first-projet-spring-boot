using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace serviceoncologie.Data.Models
{
    public class CommissionStaf
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateCreation { get; set; }

        [JsonIgnore]
        public ICollection<StafMedecin> StafMedecins { get; set; } = new List<StafMedecin>();
        [JsonIgnore]
        public ICollection<DecisionStaf>? DecisionStafs { get; set; }
    }
}