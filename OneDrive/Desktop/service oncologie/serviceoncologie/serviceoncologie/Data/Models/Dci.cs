using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace serviceoncologie.Data.Models
{
    public class Dci
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Libelle { get; set; }

        // Relation avec Medicament (1,N)
        [JsonIgnore]
        public ICollection<DciMedicament>? DciMedicaments { get; set; } = new List<DciMedicament>();
    }
}
