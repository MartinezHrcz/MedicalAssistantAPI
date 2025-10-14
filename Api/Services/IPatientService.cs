using Api.Shared.Models;
using Api.Shared.Models.DTOs;

namespace Api.Services;

public interface IPatientService
{
    Task<IEnumerable<PatientDto>> GetPatientsAsync();
    Task<PatientDto> GetPatientByIdAsync(int id);
    Task<IEnumerable<PatientDto>> GetPatientByNameAsync(string name);
    Task<PatientDto> GetPatientByTajAsync(string taj);
    Task<PatientDto> CreatePatientAsync(CreatePatientDto dto);
    Task<PatientDto> UpdatePatientAsync(int id,UpdatePatientDto dto);
    Task DeletePatient(int id);
}