using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace serviceoncologie.Data.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public DateTime DateNaissance { get; set; }
       

        [Required(ErrorMessage = "Le champ NCIN est obligatoire.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Le NCIN doit contenir exactement 8 chiffres.")]
        public string NCIN { get; set; }

        [Required]
        public string Categorie { get; set; } // Militaire ou Civile

        public string SousCategorie { get; set; }

        [Required]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "Le champ NumeroTelephone est obligatoire.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Le NumeroTelephone doit contenir exactement 8 chiffres.")]
        public string NumeroTelephone { get; set; }
        [Required]
        public string SituationSocial { get; set; }

        // Collection des rendez-vous associés à ce patient
        [JsonIgnore]
        public ICollection<Rdv>? Rdvs { get; set; }
        [JsonIgnore]
        public ICollection<Paiement>? Paiements { get; set; }
        public int DossierId { get; set; }
        public Dossier? Dossier { get; set; }



    }
}
