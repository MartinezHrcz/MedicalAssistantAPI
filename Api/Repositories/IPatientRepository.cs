using Api.Shared.Models;

namespace Api.Repositories;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllPatients();
    Task<Patient> GetPatientById(int id);
    Task<IEnumerable<Patient>> GetPatientsByName(string name);
    Task<Patient> GetPatientByTaj(string taj);
    Task<Patient> CreatePatient(Patient patient);
    Task<Patient> UpdatePatient(Patient patient);
    Task DeletePatient(int id);
    Task DeletePatientByTaj(string taj);
    Task<bool> PatientTajExists(string taj);
}