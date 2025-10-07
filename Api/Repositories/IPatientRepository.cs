using Api.Models;

namespace Api.Repositories;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllPatients();
    Task<Patient> GetPatientById(string id);
    Task<Patient> GetPatientByName(string name);
    Task<Patient> GetPatientByTaj(string taj);
    Task<Patient> CreatePatient(Patient patient);
    Task<Patient> UpdatePatient(Patient patient);
    Task DeletePatient(string id);
}