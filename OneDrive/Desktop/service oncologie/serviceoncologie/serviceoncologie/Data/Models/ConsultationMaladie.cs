using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace serviceoncologie.Data.Models
{
    public class ConsultationMaladie
    {
        [Key]
        public int Id { get; set; }

        // ForeignKey vers Consultation
        [Key, Column(Order = 1)]
        [ForeignKey("Consultation")]
        public int ConsultationId { get; set; }
        public Consultation? Consultation { get; set; }

        // ForeignKey vers Maladie
        [Key, Column(Order = 2)]
        [ForeignKey("Maladie")]
        public int MaladieId { get; set; }
        public Maladie? Maladie { get; set; }
        [ForeignKey("Dossier")]
        public int DossierId { get; set; }
        public Dossier? Dossier { get; set; }
    }
}
