using Api.Mappers;
using Api.Shared.Models;
using Api.Repositories;
using Api.Shared.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository  _patientRepository;

    private PasswordHasher<Patient> _passwordHasher;
    
    private readonly IJwtService _jwtService;
    
    public PatientService(IPatientRepository patientRepository, IJwtService jwtService)
    {
        _patientRepository = patientRepository;
        _jwtService = jwtService;
        _passwordHasher = new PasswordHasher<Patient>();
    }
    
    public async Task<IEnumerable<PatientDto>> GetPatientsAsync()
    {
        IEnumerable<Patient> patients = await _patientRepository.GetAllPatients();

        IEnumerable<PatientDto> dtos = PatientMapper.ToDtos(patients).OrderBy(p => p.TimeOfAdmission);
        
        return dtos;
    }

    public async Task<PatientDto> GetPatientByIdAsync(int id)
    {
        Patient patient = await _patientRepository.GetPatientById(id);
        return PatientMapper.ToDTO(patient);
    }

    public async Task<IEnumerable<PatientDto>> GetPatientByNameAsync(string name)
    {
        IEnumerable<Patient> patients = await _patientRepository.GetPatientsByName(name);
        IEnumerable<PatientDto> dtos = patients.Select(PatientMapper.ToDTO).OrderBy(dto => dto.TimeOfAdmission );
        return dtos;
    }

    public async Task<PatientDto> GetPatientByTajAsync(string taj)
    {
        Patient patient = await _patientRepository.GetPatientByTaj(taj);
        return PatientMapper.ToDTO(patient);
    }

    public async Task<PatientDto> RegisterPatientAsync(RegisterPatientDto dto)
    {
        if (await _patientRepository.PatientTajExists(dto.Taj))
        {
            throw new InvalidOperationException("Patient Taj Already Exists");
        }

        Patient patient = new Patient
        {
            Name = dto.Name,
            Taj = dto.Taj,
            Address = dto.Address
        };
        patient.PasswordHash = _passwordHasher.HashPassword(patient, dto.Password);
        patient = await _patientRepository.CreatePatient(patient);
        return PatientMapper.ToDTO(patient);
    }

    public async Task<PatientAuthResponsDto> LoginPatientAsync(PatientLoginDto dto)
    {
        Patient patient =  await _patientRepository.GetPatientByTaj(dto.Taj) 
                           ?? throw new KeyNotFoundException("Patient Taj Not Found");
        PasswordVerificationResult passwordValid = _passwordHasher.VerifyHashedPassword(patient, patient.PasswordHash, dto.Password);
        if (passwordValid == PasswordVerificationResult.Failed)
        {
            throw new UnauthorizedAccessException("Passwords do not match");
        }
        var token = _jwtService.GenerateToken(patient);
        return new PatientAuthResponsDto(PatientMapper.ToDTO(patient),  token);
    }

    public async Task<PatientMedicationDto> GetPatientMedicationAsync(string taj)
    {
        if (!(await _patientRepository.PatientTajExists(taj)))
        {
            throw new KeyNotFoundException("Taj Not Found");
        }
        Patient patient =  await _patientRepository.GetPatientByTaj(taj);
        PatientMedicationDto medication = PatientMapper.toMedication(patient);
        return medication;
    }

    public async Task<PatientDto> CreatePatientAsync(CreatePatientDto dto)
    {
        
        if (await _patientRepository.PatientTajExists(dto.Taj))
        {
            throw new InvalidOperationException($"Taj {dto.Taj} is already taken");
        }
        
        Patient patient = PatientMapper.ToEntity(dto);
        
        patient.PasswordHash = _passwordHasher.HashPassword(patient, dto.Password);
        try
        {
            patient = await _patientRepository.CreatePatient(patient);
        }
        catch (DbUpdateException)
        {
            throw new  InvalidOperationException($"Taj already taken");
        }

        return PatientMapper.ToDTO(patient);
    }

    public async Task<PatientDto> UpdatePatientAsync(int id,UpdatePatientDto dto)
    {
        Patient patient = await _patientRepository.GetPatientById(id);

        if (dto.Taj != patient.Taj && await _patientRepository.PatientTajExists(dto.Taj))
        {
            throw new InvalidOperationException($"Taj {dto.Taj} is already taken");
        }
        
        patient.Name = dto.Name;
        patient.Address = dto.Address;
        patient.Complaints = dto.Complaints;
        patient.Taj = dto.Taj;

        try
        {
            patient = await _patientRepository.UpdatePatient(patient);
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("Taj already taken!");
        }
        return PatientMapper.ToDTO(patient);
    }

    public async Task DeletePatient(int id)
    {
        await _patientRepository.DeletePatient(id);
    }
    
    public async Task DeletePatientByTaj(string taj)
    {
        await _patientRepository.DeletePatientByTaj(taj);
    }
}