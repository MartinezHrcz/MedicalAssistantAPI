using Api.Shared.Models;
using Api.Shared.Models.DTOs;

namespace Api.Mappers;

public class PatientMapper
{
    public static PatientDto ToDTO(Patient patient)
    {
        return new PatientDto(
            patient.Name,
            patient.Address,
            patient.Taj,
            patient.Complaints,
            patient.TimeOfAdmission
            );
    }
    
    public static IEnumerable<PatientDto> ToDtos(IEnumerable<Patient> patients)
    {
        return patients.Select(p => ToDTO(p));
    }

    public static Patient ToEntity(CreatePatientDto dto)
    {
        return new Patient
        {
           Name = dto.Name,
           Address = dto.Address,
           Taj = dto.Taj,
           Complaints = dto.Complaints
        };
    }

    public static Patient CreateEntity(CreatePatientDto dto)
    {
        return new Patient
        {
            Name = dto.Name,
            Address = dto.Address,
            Taj = dto.Taj,
            Complaints = dto.Complaints
        };
    }


}