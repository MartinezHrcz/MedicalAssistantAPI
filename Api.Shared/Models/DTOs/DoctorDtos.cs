using System.ComponentModel.DataAnnotations;

namespace Api.Shared.Models.DTOs;

public record DoctorDto (
    int Id,
    string Name,
    string Address,
    string Phone,
    string Email
);

public record CreateDoctorDto(
    [Required, MaxLength(50)
     , RegularExpression(@"^[A-Za-zÀ-ž\ \s'-]{2,50}$",  ErrorMessage = "Name should only contain letters, spaces or hypens!")]
    string Name,
    string Address,
    [Phone]
    string Phone,
    [EmailAddress]
    string Email,
    [RegularExpression(pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage="Password should be atleast 8 characters long, have atleast 1 uppercase 1 lowercase and a special character.")]
    string Password
);

public record UpdateDoctorDto(
    [Required, MaxLength(50)
     , RegularExpression(@"^[A-Za-zÀ-ž\ \s'-]{2,50}$",  ErrorMessage = "Name should only contain letters, spaces or hypens!")]
    string Name,
    string? Address,
    [Phone]
    string Phone,
    [EmailAddress]
    string Email
);

public record RegisterDoctorDto(
    [Required, MaxLength(50)
     , RegularExpression(@"^[A-Za-zÀ-ž\ \s'-]{2,50}$",  ErrorMessage = "Name should only contain letters, spaces or hypens!")]
    string Name,
    string? Address,
    [Phone]
    string Phone,
    [EmailAddress]
    string Email,
    [RegularExpression(pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage="Password should be atleast 8 characters long, have atleast 1 uppercase 1 lowercase and a special character.")]
    string Password
);

public record LoginDoctorDto(
    [EmailAddress]
    string Email,
    [Required, MinLength(8)]
    [RegularExpression(pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage="Password should be atleast 8 characters long, have atleast 1 uppercase 1 lowercase and a special character.")]
    string Password
);

public record DoctorAuthResponseDto(
    DoctorDto doctor,
    string Token
);