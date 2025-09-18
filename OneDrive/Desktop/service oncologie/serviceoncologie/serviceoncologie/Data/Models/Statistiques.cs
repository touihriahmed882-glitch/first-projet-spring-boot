namespace serviceoncologie.Data.Models
{
    public class Statistiques
    {
        public int NombreTotalPatients { get; set; }
        public int NombreTotalConsultations { get; set; }
        public int NombreTotalMedecins { get; set; }
        public int NombreTotalRdvs { get; set; }
        public int NombreRdvsConfirmes { get; set; }
        public int NombreRdvsAnnules { get; set; }
        public int NombrePatientsAujourdHui { get; set; }
        public int NombreConsultationsAujourdHui { get; set; }
    }
}