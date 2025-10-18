using Api.Shared.Models;
using Api.Shared.Models.DTOs;

namespace Api.Mappers;

public class DoctorMapper
{
    public static DoctorDto ToDTO(Doctor doctor)
    {
        return new DoctorDto(
            doctor.Id,
            doctor.Name,
            doctor.Address,
            doctor.PhoneNumber,
            doctor.Email

        );
    }

    public static Doctor ToModel(DoctorDto doctor)
    {
        return new Doctor
        {
            Id = doctor.Id,
            Name = doctor.Name,
            Address = doctor.Address,
            PhoneNumber = doctor.Phone,
            Email = doctor.Email
        };
    }

    public static Doctor ToModel(RegisterDoctorDto dto)
    {
        return new Doctor
        {
            Name = dto.Name,
            Address = dto.Address,
            Email = dto.Email,
            PhoneNumber = dto.Phone
        };
        
    }

    public static IEnumerable<DoctorDto> ToDto(IEnumerable<Doctor> doctors)
    {
        return doctors.Select(d => ToDTO(d));
    }
}