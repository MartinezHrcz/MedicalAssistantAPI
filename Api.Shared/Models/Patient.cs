using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Shared.Models;

public class Patient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] [MaxLength(50)]
    public string Name { get; set; } = null!;
    
    public string? Address { get; set; }

    [Required]
    [MaxLength(12)]
    [RegularExpression(@"^\d{3}-\d{3}-\d{3}$")]
    public string Taj { get; set; } = null!;

    public string? Complaints { get; set; } 
    
    public DateTime TimeOfAdmission { get; set; } =  DateTime.Now;
    
}