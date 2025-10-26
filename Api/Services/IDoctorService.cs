using Api.Shared.Models.DTOs;

namespace Api.Services;

public interface IDoctorService
{
    Task<IEnumerable<DoctorDto>> GetDoctorsAsync();
    Task<DoctorDto> GetDoctorAsync(int id);
    Task<IEnumerable<DoctorDto>> GetDoctorsByNameAsync(string name);
    Task<DoctorDto> CreateDoctorAsync(RegisterDoctorDto doctor);
    Task<DoctorDto> UpdateDoctorAsync(int id,UpdateDoctorDto doctor);
    Task<bool> UpdateDoctorPasswordAsync(UpdateDoctorDto doctor);
    Task<bool> DeleteDoctorAsync(int id);
    Task<IEnumerable<PatientDto>> GetPatientsOfDoctor(int id);
    Task AddPatientAsync(int doctorId, int patientId);
    Task RemovePatientAsync(int doctorId, int patientId);
    Task<DoctorAuthResponseDto> LoginDoctorAsync(LoginDoctorDto doctor);
    Task AddPatientMedication(string taj, string title ,string medication);
    Task RemovePatientMedication(string taj,Guid medication);
}