using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace serviceoncologie.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Le format de l'adresse e-mail est invalide.")]

        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public DateTime DateDeNaissance { get; set; }

        [Required(ErrorMessage = "Le champ NCIN est obligatoire.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Le NCIN doit contenir exactement 8 chiffres.")]
        public string NCIN { get; set; }

        public string SituationSocial { get; set; }

        [Required]
        public string Adresse { get; set; }

        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Le téléphone doit contenir exactement 8 chiffres.")]
        public string Telephone { get; set; }

        [Required]
        public string Sexe { get; set; }
        public string? PhotoProfil { get; set; } // Ajout de l'attribut PhotoProfil

        [JsonIgnore]
        public ICollection<TacheUser> TacheUsers { get; set; } = new List<TacheUser>();
        [JsonIgnore]
        public ICollection<Maladie>? MaladiesCodées { get; set; }
        [JsonIgnore]
        public ICollection<StafMedecin>? StafMedecins { get; set; }





    }
}
