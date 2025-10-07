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
    [Required, MaxLength(50)]
    string Name,
    [Required,MaxLength(12), RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "Taj must be in 000-000-000 format!")]
    string Taj,
    string? Address,
    string? Complaints
    );

public record UpdatePatientDto(
    [Required, MaxLength(50)]
    string Name,
    [Required, MaxLength(12)]
    [RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "TAJ must be in 000-000-000 format!")]
    string Taj,
    string? Address,
    string? Complaints
    );