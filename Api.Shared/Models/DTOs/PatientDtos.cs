using System.ComponentModel.DataAnnotations;

namespace Api.Shared.Models.DTOs;

public record PatientDto(
    int id,
    string Name,
    string? Address,
    string Taj,
    string? Complaints,
    int? doctorId,
    DateTime TimeOfAdmission
    );

public record CreatePatientDto(
    [Required, MaxLength(50)
     , RegularExpression(@"^[A-Za-zÀ-ž\ \s'-]{2,50}$",  ErrorMessage = "Name should only contain letters, spaces or hypens!")]
    string Name,
    [Required,MaxLength(12), RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "Taj must be in 000-000-000 format!")]
    string Taj,
    string? Address,
    string? Complaints,
    [Required, MinLength(8)]
    [RegularExpression(pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage="Password should be atleast 8 characters long, have atleast 1 uppercase 1 lowercase and a special character.")]
    string Password
    );

public record UpdatePatientDto(
    [Required, MaxLength(50),
     RegularExpression(@"^[A-Za-zÀ-ž\ \s'-]{2,50}$",  ErrorMessage = "Name should only contain letters, spaces or hypens!")]
    string Name,
    [Required, MaxLength(12)]
    [RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "TAJ must be in 000-000-000 format!")]
    string Taj,
    string? Address,
    string? Complaints
    );
    
public record RegisterPatientDto(
    [Required, MaxLength(50)]
    string Name,
    string Address,
    [Required, MaxLength(12)]
    [RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "TAJ must be in 000-000-000 format!")]
    string Taj,
    [RegularExpression(pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage="Password should be atleast 8 characters long, have atleast 1 uppercase 1 lowercase and a special character.")]
    string Password
);

public record PatientLoginDto(
    [Required, MaxLength(12)]
    [RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "TAJ must be in 000-000-000 format!")]
    string Taj,
    [RegularExpression(pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage="Password should be atleast 8 characters long, have atleast 1 uppercase 1 lowercase and a special character.")]
    string Password
);