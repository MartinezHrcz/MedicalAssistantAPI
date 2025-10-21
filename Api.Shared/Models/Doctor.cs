using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Shared.Models;

public class Doctor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required] 
    [MaxLength(50)]
    [RegularExpression(pattern:"^[A-Za-zÀ-ž\\ \\s'-]{2,50}$", ErrorMessage = "Name should only contain letters, spaces or hypens!")]
    public string Name { get; set; }
    
    public string? Address { get; set; }
    
    [Phone]
    public string PhoneNumber { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required,RegularExpression(pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage="Password should be atleast 8 characters long, have atleast 1 uppercase 1 lowercase and a special character.")]
    public string PasswordHash {get; set;}

    public string Role => "Doctor";

}