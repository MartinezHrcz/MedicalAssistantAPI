using Api.DTOs;
using Api.Mappers;
using Api.Shared.Models;
using Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository  _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
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

    public async Task<PatientDto> CreatePatientAsync(CreatePatientDto dto)
    {
        
        if (await _patientRepository.PatientTajExists(dto.Taj))
        {
            throw new InvalidOperationException($"Taj {dto.Taj} is already taken");
        }
        
        Patient patient = PatientMapper.ToEntity(dto);
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