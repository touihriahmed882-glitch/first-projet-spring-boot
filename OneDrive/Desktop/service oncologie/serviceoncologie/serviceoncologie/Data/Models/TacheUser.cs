using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace serviceoncologie.Data.Models
{
    public class TacheUser
    {
        [Key]
        public int Id { get; set; }

        

        // Date d'ajout de la tâche à l'utilisateur
        public DateTime? DateAdded { get; set; }

       

        // Relation avec User
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        // Relation avec Tache
        [Required]
        public int TacheId { get; set; }

        [ForeignKey("TacheId")]
        public Tache Tache { get; set; }
    }
}
