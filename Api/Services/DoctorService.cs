using Api.Repositories;
using Api.Shared.Models;
using Api.Shared.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Api.Services;

public class DoctorService : IDoctorService
{
    
    private readonly IDoctorRepository  _doctorRepository;
    private PasswordHasher<DoctorDto> _passwordHasher;

    public DoctorService(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
        _passwordHasher = new PasswordHasher<DoctorDto>();
    }

    public async Task<IEnumerable<DoctorDto>> GetDoctorsAsync()
    {
        IEnumerable<Doctor> doctors = await _doctorRepository.GetAllDoctors();
    }

    public async Task<DoctorDto?> GetDoctorAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<DoctorDto> CreateDoctorAsync(RegisterDoctorDto doctor)
    {
        throw new NotImplementedException();
    }

    public async Task<DoctorDto> UpdateDoctorAsync(UpdateDoctorDto doctor)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteDoctorAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PatientDto>> GetPatientsOfDoctor(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddPatientAsync(int doctorId, int patientId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemovePatientAsync(int doctorId, int patientId)
    {
        throw new NotImplementedException();
    }

    public async Task<DoctorDto> LoginDoctorAsync(LoginDoctorDto doctor)
    {
        throw new NotImplementedException();
    }
}