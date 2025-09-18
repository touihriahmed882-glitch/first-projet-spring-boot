using serviceoncologie.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


public class StafMedecin
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int userid { get; set; }
    [JsonIgnore]

    public User? User { get; set; }

    [Required]
    public int commissionstafid { get; set; }
    [JsonIgnore]

    public CommissionStaf? CommissionStaf { get; set; }
}