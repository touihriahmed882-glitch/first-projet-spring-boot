using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;

public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAllPatients()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient> GetPatientById(int id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task AddPatient(Patient patient)
    {
        // Vérifie s’il n’y a pas de dossier
        if (patient.DossierId == 0 && patient.Dossier == null)
        {
            // Créer un nouveau dossier
            var nouveauDossier = new Dossier
            {
                NumeroDossier = "Dossier-" + Guid.NewGuid().ToString(), // Génération d'un numéro de dossier unique
                DateCreation = DateTime.Now
            };

            // Ajouter le dossier à la base de données
            await _context.Dossiers.AddAsync(nouveauDossier);
            await _context.SaveChangesAsync();

            // Associer le dossier au patient
            patient.DossierId = nouveauDossier.Id;
        }

        // Ajouter le patient à la base de données
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Patient>> GetPatientsByDossierId(int dossierId)
    {
        return await _context.Patients
            .Where(p => p.DossierId == dossierId)
            .ToListAsync();
    }


    public async Task UpdatePatient(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePatient(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient != null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}
