using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace serviceoncologie.Data.Models
{
    public class DciMedicament
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Clé étrangère pour DCI
        [Required]
        public int DciId { get; set; }
        [JsonIgnore]

        public Dci? Dci { get; set; }

        // Clé étrangère pour Medicament
        [Required]
        public int MedicamentId { get; set; }
        [JsonIgnore]

        public Medicament? Medicament { get; set; }
    }
}
