using System.Collections.Generic;
using System.Threading.Tasks;
using serviceoncologie.Data.Models;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllPatients();
    Task<Patient> GetPatientById(int id);
    Task AddPatient(Patient patient);
    Task UpdatePatient(Patient patient);
    Task DeletePatient(int id);
    Task<IEnumerable<Patient>> GetPatientsByDossierId(int dossierId);

}
