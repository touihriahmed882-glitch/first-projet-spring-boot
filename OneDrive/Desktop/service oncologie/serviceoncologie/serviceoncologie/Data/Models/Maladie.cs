using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace serviceoncologie.Data.Models
{
    public class Maladie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; } // Nom de la maladie

        [Required]
        public string codecim { get; set; } // Code CIM-10 pour standardisation

        public string Description { get; set; }

        // Relation Many-to-One : Une maladie est codée par UN seul médecin
        [ForeignKey("Medecin")]
        public int MedecinId { get; set; }
        [JsonIgnore]
        public User? Medecin { get; set; }
        [JsonIgnore]
        public ICollection<ConsultationMaladie>? ConsultationMaladies { get; set; }

    }
}
