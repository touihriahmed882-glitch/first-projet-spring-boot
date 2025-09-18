using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using serviceoncologie.Data.Models;
public class Admission
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime DateAdmission { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string MotifAdmission { get; set; }

    public DateTime? DateSortie { get; set; } // Optionnel (peut être null)

    [Column(TypeName = "text")]
    public string? MotifSortie { get; set; } // Optionnel

    // Relation Many-to-One avec DecisionStaf
    [ForeignKey("DecisionStaf")]
    public int DecisionStafId { get; set; }
    [JsonIgnore]
    public DecisionStaf? DecisionStaf { get; set; }

    // Relation Many-to-One avec Consultation
    [ForeignKey("Consultation")]
    public int ConsultationId { get; set; }
    [JsonIgnore]
    public Consultation? Consultation { get; set; }
    public int DossierId { get; set; }
    public Dossier? Dossier { get; set; }
    public virtual ICollection<Cure>? Cures { get; set; }

}
