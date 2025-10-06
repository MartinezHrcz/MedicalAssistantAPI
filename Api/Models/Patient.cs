using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class Patient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    public string Address { get; set; }

    [Required]
    [RegularExpression(@"^\d{3}-\d{3}-\d{3}$")]
    public string Taj { get; set; }

    public string Complaints { get; set; }
    
    public DateTime timeOfAdmission { get; set; } =  DateTime.Now;
    
}