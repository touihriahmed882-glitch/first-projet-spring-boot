using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Tache> Taches { get; set; }
        public DbSet<TacheUser> TacheUsers { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Rdv> Rdvs { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<Maladie> Maladies { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<ConsultationMaladie> ConsultationMaladies { get; set; }
        public DbSet<CommissionStaf> CommissionStafs { get; set; }
        public DbSet<StafMedecin> StafMedecins { get; set; }
        public DbSet<DecisionStaf> DecisionStafs { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Protocole> Protocoles { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Cure> Cures { get; set; }
        public DbSet<Dci> Dcis { get; set; }
        public DbSet<DciMedicament> DciMedicaments { get; set; }
        public DbSet<Dossier> Dossiers { get; set; }
        public DbSet<PrestationMedicale> PrestationsMedicales { get; set; }
        public DbSet<ConsultationPrestation> ConsultationPrestations { get; set; }












        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TacheUser>()
                .HasOne(tu => tu.User)
                .WithMany(u => u.TacheUsers)
                .HasForeignKey(tu => tu.UserId);

            modelBuilder.Entity<TacheUser>()
                .HasOne(tu => tu.Tache)
                .WithMany(t => t.TacheUsers)
                .HasForeignKey(tu => tu.TacheId);
            modelBuilder.Entity<Patient>();
            modelBuilder.Entity<Rdv>()
                .HasOne(r => r.Medecin)
                .WithMany()
                .HasForeignKey(r => r.MedecinId)
                .OnDelete(DeleteBehavior.Restrict); // Empêcher la suppression d'un médecin s'il a des RDV

     
           
            modelBuilder.Entity<Paiement>()
                .HasOne(p => p.Rdv)
                .WithOne(r => r.Paiement)
                .HasForeignKey<Paiement>(p => p.RdvId)
                .OnDelete(DeleteBehavior.Cascade);
            // Définition de la relation entre Patient et Rdv (One-to-Many)
            modelBuilder.Entity<Rdv>()
                .HasOne(r => r.Patient)      // Un Rdv a un Patient
                .WithMany(p => p.Rdvs)       // Un Patient a plusieurs Rdvs
                .HasForeignKey(r => r.PatientId)  // La clé étrangère pour cette relation
                .OnDelete(DeleteBehavior.Cascade); // Lors de la suppression d'un Patient, supprimer ses Rdvs associés (Optionnel)
            modelBuilder.Entity<Paiement>()
                .HasOne(p => p.Patient) // Paiement a un Patient
                .WithMany(patient => patient.Paiements) // Patient a plusieurs Paiements
                .HasForeignKey(p => p.PatientId) // Clé étrangère dans Paiement
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade si un Patient est supprimé
            modelBuilder.Entity<Maladie>()
                .HasOne(m => m.Medecin)
                .WithMany(u => u.MaladiesCodées)
                .HasForeignKey(m => m.MedecinId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ConsultationMaladie>()
                .HasKey(cm => new { cm.ConsultationId, cm.MaladieId });

            modelBuilder.Entity<ConsultationMaladie>()
                .HasOne(cm => cm.Consultation)
                .WithMany(c => c.ConsultationMaladies)
                .HasForeignKey(cm => cm.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConsultationMaladie>()
                .HasOne(cm => cm.Maladie)
                .WithMany(m => m.ConsultationMaladies)
                .HasForeignKey(cm => cm.MaladieId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Consultation>()
                .HasOne(c => c.Patient)
                .WithMany()
                .HasForeignKey(c => c.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Consultation>()
                .HasOne(c => c.Medecin)
                .WithMany()
                .HasForeignKey(c => c.MedecinId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ConsultationMaladie>()
        .HasKey(cm => new { cm.ConsultationId, cm.MaladieId });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StafMedecin>()
                .HasOne(sm => sm.CommissionStaf)
                .WithMany(c => c.StafMedecins)
                .HasForeignKey(sm => sm.commissionstafid)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration de la relation One-to-Many entre User et StafMedecin (uniquement pour les médecins)
            modelBuilder.Entity<StafMedecin>()
                .HasOne(sm => sm.User)
                .WithMany(u => u.StafMedecins)
                .HasForeignKey(sm => sm.userid)
                .OnDelete(DeleteBehavior.Cascade);

            // Ajout d'une contrainte pour s'assurer que seuls les médecins sont dans StafMedecin
            modelBuilder.Entity<StafMedecin>()
                .HasOne(sm => sm.User)
                .WithMany(u => u.StafMedecins)
                .HasForeignKey(sm => sm.userid)
                .HasConstraintName("FK_StafMedecin_User_Medecin")
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
            // Relation One-to-Many entre DecisionStaf et Consultation
            modelBuilder.Entity<DecisionStaf>()
                .HasOne(d => d.Consultation)
                .WithMany(c => c.DecisionStafs)
                .HasForeignKey(d => d.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation One-to-Many entre DecisionStaf et CommissionStaf
            modelBuilder.Entity<DecisionStaf>()
                .HasOne(d => d.CommissionStaf)
                .WithMany(cs => cs.DecisionStafs)
                .HasForeignKey(d => d.CommissionStafId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation One-to-Many entre Admission et DecisionStaf
            modelBuilder.Entity<Admission>()
                .HasOne(a => a.DecisionStaf)
                .WithMany(d => d.Admissions)
                .HasForeignKey(a => a.DecisionStafId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation One-to-Many entre Admission et Consultation
            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Consultation)
                .WithMany(c => c.Admissions)
                .HasForeignKey(a => a.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);

          
            modelBuilder.Entity<Cure>()
               .HasOne(c => c.Protocole)
               .WithMany(p => p.Cures)
               .HasForeignKey(c => c.ProtocoleId);

            modelBuilder.Entity<Cure>()
                .HasOne(c => c.Medicament)
                .WithMany(m => m.Cures)
                .HasForeignKey(c => c.MedicamentId);
            modelBuilder.Entity<DciMedicament>()
               .HasOne(dm => dm.Dci)
               .WithMany(d => d.DciMedicaments)
               .HasForeignKey(dm => dm.DciId);

            modelBuilder.Entity<DciMedicament>()
                .HasOne(dm => dm.Medicament)
                .WithMany(m => m.DciMedicaments)
                .HasForeignKey(dm => dm.MedicamentId);
            modelBuilder.Entity<Dossier>()
                .HasMany(d => d.Consultations)
                .WithOne(c => c.Dossier)
                .HasForeignKey(c => c.DossierId);

            modelBuilder.Entity<Dossier>()
                .HasMany(d => d.DecisionStafs)
                .WithOne(ds => ds.Dossier)
                .HasForeignKey(ds => ds.DossierId);
            modelBuilder.Entity<ConsultationMaladie>()
                .HasOne(cm => cm.Dossier)
                .WithMany(d => d.ConsultationMaladies)
                .HasForeignKey(cm => cm.DossierId);
            modelBuilder.Entity<ConsultationPrestation>()
                .HasKey(cp => new { cp.ConsultationId, cp.PrestationMedicaleId });

            modelBuilder.Entity<ConsultationPrestation>()
                .HasOne(cp => cp.Consultation)
                .WithMany(c => c.ConsultationPrestations)
                .HasForeignKey(cp => cp.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConsultationPrestation>()
                .HasOne(cp => cp.PrestationMedicale)
                .WithMany(p => p.ConsultationPrestations)
                .HasForeignKey(cp => cp.PrestationMedicaleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Patient>()
               .HasOne(p => p.Dossier)
               .WithMany(d => d.Patients)
               .HasForeignKey(p => p.DossierId)
               .OnDelete(DeleteBehavior.SetNull); // Permet d'avoir DossierId facultatif
            modelBuilder.Entity<Admission>()
              .HasOne(p => p.Dossier)
              .WithMany(d => d.Admissions)
              .HasForeignKey(p => p.DossierId)
              .OnDelete(DeleteBehavior.SetNull); // Permet d'avoir DossierId facultatif
           
            modelBuilder.Entity<Cure>()
              .HasOne(c => c.Dossier)
              .WithMany(d => d.Cures)
              .HasForeignKey(c => c.DossierId)
              .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DecisionStaf>()
               .HasOne(d => d.Protocole)
               .WithMany() // ou .WithMany(p => p.DecisionStafs) si tu veux la collection inverse
               .HasForeignKey(d => d.ProtocoleId)
               .OnDelete(DeleteBehavior.Restrict); // ou Cascade selon ton besoin
            modelBuilder.Entity<Cure>()
                .HasOne(c => c.DecisionStaf)  // Cure a une DecisionStaf
                .WithMany(d => d.Cures)      // DecisionStaf a plusieurs Cure
                .HasForeignKey(c => c.DecisionStafId)  // Clé étrangère
                .OnDelete(DeleteBehavior.Cascade);  // Optionnel, pour définir la gestion de suppression
                                                    // Relation entre Admission et Cure (One-to-Many)
                                                    // Configurer la relation entre Cure et Admission
            modelBuilder.Entity<Cure>()
                .HasOne(c => c.Admission) // Une Cure a une seule Admission
                .WithMany(a => a.Cures) // Une Admission peut avoir plusieurs Cures
                .HasForeignKey(c => c.AdmissionId) // La clé étrangère est dans la table Cure
                .OnDelete(DeleteBehavior.Cascade); // Cascade de suppression pour maintenir l'intégrité


        }
    }
}
