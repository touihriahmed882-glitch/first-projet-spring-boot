using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace serviceoncologie.Data.Models
{
    public class Tache
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Libelle { get; set; }
        [JsonIgnore]
        public ICollection<TacheUser>? TacheUsers { get; set; }= new List<TacheUser>();
    }
}
