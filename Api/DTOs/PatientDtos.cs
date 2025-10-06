using System.ComponentModel.DataAnnotations;

namespace Api.DTOs;

public record PatientDto(
    int Id,
    string Name,
    string? Address,
    string Taj,
    string? Complaints,
    DateTime TimeOfAdmission
    );

public record CreatePatientDto(
    [property: Required, MaxLength(50)]
    string Name,
    [property:Required, RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "Taj must be in 000-000-000 format!")]
    string Taj,
    string? Address,
    string? Complaints
    );

public record UpdatePatientDto(
    [property:Required, MaxLength(50)]
    string Name,
    [property:Required,  RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "Taj must be in 000-000-000 format!")]
    string Taj,
    string? Address,
    string? Complaints
    );