using Api.Shared.Models.DTOs;

namespace Api.Services;

public interface IDoctorService
{
    Task<IEnumerable<DoctorDto>> GetDoctorsAsync();
    Task<DoctorDto> GetDoctorAsync(int id);
    Task<DoctorDto> CreateDoctorAsync(RegisterDoctorDto doctor);
    Task<DoctorDto> UpdateDoctorAsync(UpdateDoctorDto doctor);
    Task<bool> DeleteDoctorAsync(int id);
    Task<IEnumerable<PatientDto>> GetPatientsOfDoctor(int id);
    Task<bool> AddPatientAsync(int doctorId, int patientId);
    Task<bool> RemovePatientAsync(int doctorId, int patientId);
    Task<DoctorDto> LoginDoctorAsync(LoginDoctorDto doctor);
}