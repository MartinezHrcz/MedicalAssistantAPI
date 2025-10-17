using Api.Repositories;
using Api.Shared.Models;
using Api.Shared.Models.DTOs;
using Api.Mappers;
using Microsoft.AspNetCore.Identity;

namespace Api.Services;

public class DoctorService : IDoctorService
{
    
    private readonly IDoctorRepository  _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private PasswordHasher<Doctor> _passwordHasher;

    public DoctorService(IDoctorRepository doctorRepository, IPatientRepository patientRepository)
    {
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _passwordHasher = new PasswordHasher<Doctor>();
    }

    public async Task<IEnumerable<DoctorDto>> GetDoctorsAsync()
    {
        IEnumerable<Doctor> doctors = await _doctorRepository.GetAllDoctors();
        return DoctorMapper.ToDto(doctors);
    }

    public async Task<DoctorDto?> GetDoctorAsync(int id)
    {
        Doctor doctor = await _doctorRepository.GetDoctorById(id) ??  throw new KeyNotFoundException();
        return DoctorMapper.ToDTO(doctor);
    }

    public async Task<DoctorDto> CreateDoctorAsync(RegisterDoctorDto dto)
    {
        if (await _doctorRepository.DoctorEmailExist(dto.Email))
        {
            throw new InvalidOperationException($"Email {dto.Email} already exists!");
        }
        Doctor doctor = DoctorMapper.ToModel(dto);
        doctor.PasswordHash = _passwordHasher.HashPassword(doctor, dto.Password);
        doctor = await _doctorRepository.CreateDoctor(doctor);  
        return DoctorMapper.ToDTO(doctor);
    }

    public async Task<DoctorDto> UpdateDoctorAsync(UpdateDoctorDto dto)
    {
        Doctor doctor = new Doctor
        {
            Name = dto.Name,
            Email = dto.Email,
            Address = dto.Address,
            PhoneNumber = dto.Phone
        };
        doctor = await _doctorRepository.UpdateDoctor(doctor);
        return DoctorMapper.ToDTO(doctor);
    }

    public async Task<bool> DeleteDoctorAsync(int id)
    {
        return await _doctorRepository.DeleteDoctor(id);
    }

    public async Task<IEnumerable<PatientDto>> GetPatientsOfDoctor(int id)
    {
        Doctor doctor = await _doctorRepository.GetDoctorById(id)  ?? throw new KeyNotFoundException("Doctor not found with id: "+ id);
        IEnumerable<Patient> patients = await _patientRepository.GetPatientsByDoctor(id);
        return PatientMapper.ToDtos(patients);
    }

    public async Task<bool> AddPatientAsync(int doctorId, int patientId)
    {
        Doctor doctor = await _doctorRepository.GetDoctorById(doctorId)?? throw new KeyNotFoundException("Doctor not found with id: " + doctorId);
        Patient patient = await _patientRepository.GetPatientById(patientId) ?? throw new KeyNotFoundException("Patient not found with id: " + patientId);
        patient.doctor =  doctor;
        await _patientRepository.UpdateAsync(patient);
        return true;
    }

    public async Task<bool> RemovePatientAsync(int doctorId, int patientId)
    {
        Doctor doctor = await _doctorRepository.GetDoctorById(doctorId)?? throw new KeyNotFoundException("Doctor not found with id: " + doctorId);
        Patient patient = await _patientRepository.GetPatientById(patientId) ?? throw new KeyNotFoundException("Patient not found with id: " + patientId);
        if (patient.doctor != null && !doctor.Id.Equals(patient.doctor.Id))
        {
            throw new InvalidOperationException("Doctor isn't assigned to this patient");
        }
        patient.doctor = null;
        await _patientRepository.UpdateAsync(patient);
        return true;
    }

    public async Task<DoctorDto> LoginDoctorAsync(LoginDoctorDto dto)
    {
        Doctor doctor = await _doctorRepository.GetDoctorByEmail(dto.Email) ?? throw new KeyNotFoundException("Doctor with email not found!");
        PasswordVerificationResult passwordValid =
            _passwordHasher.VerifyHashedPassword(doctor, doctor.PasswordHash, dto.Password);
        if (passwordValid == PasswordVerificationResult.Failed)
        {
            throw new UnauthorizedAccessException("Passwords do not match!");
        }
        return DoctorMapper.ToDTO(doctor);
    }
}