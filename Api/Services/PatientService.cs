using Api.DTOs;
using Api.Mappers;
using Api.Shared.Models;
using Api.Repositories;

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

        IEnumerable<PatientDto> dtos = patients.Select(PatientMapper.ToDTO);
        
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
        IEnumerable<PatientDto> dtos = patients.Select(PatientMapper.ToDTO);
        return dtos;
    }

    public async Task<PatientDto> GetPatientByTajAsync(string taj)
    {
        Patient patient = await _patientRepository.GetPatientByTaj(taj);
        return PatientMapper.ToDTO(patient);
    }

    public async Task<PatientDto> CreatePatientAsync(CreatePatientDto dto)
    {
        Patient patient = PatientMapper.ToEntity(dto);
        await _patientRepository.CreatePatient(patient);
        return PatientMapper.ToDTO(patient);
    }

    public async Task<PatientDto> UpdatePatientAsync(int id,UpdatePatientDto dto)
    {
        Patient patient = await _patientRepository.GetPatientById(id);
        
        patient.Name = dto.Name;
        patient.Address = dto.Address;
        patient.Complaints = dto.Complaints;
        patient.Taj = dto.Taj;
        
        patient = await _patientRepository.UpdatePatient(patient);
        return PatientMapper.ToDTO(patient);
    }

    public async Task DeletePatient(int id)
    {
        await _patientRepository.DeletePatient(id);
    }
}