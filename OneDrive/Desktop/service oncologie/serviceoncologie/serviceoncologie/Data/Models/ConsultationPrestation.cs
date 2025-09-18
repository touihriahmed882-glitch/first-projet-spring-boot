using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace serviceoncologie.Data.Models
{
    public class ConsultationPrestation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Consultation")]
        public int ConsultationId { get; set; }
        [JsonIgnore]
        public Consultation? Consultation { get; set; }

        [ForeignKey("PrestationMedicale")]
        public int PrestationMedicaleId { get; set; }
        [JsonIgnore]
        public PrestationMedicale? PrestationMedicale { get; set; }
    }
}
