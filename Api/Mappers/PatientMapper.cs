using Api.Shared.Models;
using Api.Shared.Models.DTOs;

namespace Api.Mappers;

public class PatientMapper
{
    public static PatientDto ToDTO(Patient patient)
    {
        if (patient.doctor == null)
        {
            return new PatientDto(
                patient.Id,
                patient.Name,
                patient.Address,
                patient.Taj,
                patient.Complaints,
                null,
                patient.TimeOfAdmission
            );
        }

        return new PatientDto(
            id:patient.Id,
            Name:patient.Name,
            Address:patient.Address,
            Taj:patient.Taj,
            Complaints:patient.Complaints,
            doctorId:patient.doctor.Id,
            TimeOfAdmission:patient.TimeOfAdmission
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