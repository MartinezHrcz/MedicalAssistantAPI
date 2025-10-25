using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api.Shared.Models;

[Index(nameof(Taj) , IsUnique = true)]
public class Patient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] 
    [MaxLength(50)]
    [RegularExpression(pattern:"^[A-Za-zÀ-ž\\ \\s'-]{2,50}$", ErrorMessage = "Name should only contain letters, spaces or hypens!")]
    public string Name { get; set; } = null!;
    
    public string? Address { get; set; }

    [Required]
    [MaxLength(12)]
    [RegularExpression(@"^\d{3}-\d{3}-\d{3}$")]
    public string Taj { get; set; } = null!;

    public string? Complaints { get; set; } 
    
    public DateTime TimeOfAdmission { get; set; } =  DateTime.Now;

    [Required]
    public string PasswordHash {get; set; }
    
    public Doctor? doctor { get; set;}

    public List<Medication> Medications { get; set;} = new();
    
    public string Role => "Patient";
}