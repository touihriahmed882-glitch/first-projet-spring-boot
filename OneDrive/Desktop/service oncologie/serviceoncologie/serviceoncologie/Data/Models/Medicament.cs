using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace serviceoncologie.Data.Models
{
    public class Medicament
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("LibelleMedicament")]
        public string LibelleMedicament { get; set; }

        // Relation Many-to-Many avec Protocole via Cure
        [JsonIgnore]
        public ICollection<Cure>? Cures { get; set; }
        [JsonIgnore]
        public ICollection<DciMedicament>? DciMedicaments { get; set; } = new List<DciMedicament>();

    }
}
