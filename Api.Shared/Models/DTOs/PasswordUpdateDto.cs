using System.ComponentModel.DataAnnotations;

namespace Api.Shared.Models.DTOs;

public record PasswordUpdateDto(
    [RegularExpression(pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage="Password should be atleast 8 characters long, have atleast 1 uppercase 1 lowercase and a special character.")]
    string OldPassword,
    [RegularExpression(pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage="Password should be atleast 8 characters long, have atleast 1 uppercase 1 lowercase and a special character.")]
    string NewPassword
);