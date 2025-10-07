using Api.DTOs;
using Api.Models;

namespace Api.Services;

public interface IPatientService
{
    Task<IEnumerable<Patient>> GetPatientsAsync();
    Task<PatientDto> GetPatientByIdAsync(int id);
    Task<IEnumerable<PatientDto>> GetPatientByNameAsync(string name);
    Task<PatientDto> GetPatientByTajAsync(string taj);
    Task<PatientDto> CreatePatientAsync(CreatePatientDto dto);
    Task<PatientDto> UpdatePatientAsync(UpdatePatientDto dto);
    Task DeletePatient(int id);
}