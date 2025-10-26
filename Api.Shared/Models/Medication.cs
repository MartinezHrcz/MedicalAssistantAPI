

using System.ComponentModel.DataAnnotations;

namespace Api.Shared.Models;

public record Medication
{
    [Key]
    public Guid id { get; init; } = Guid.NewGuid();
    [Required]
    public string title { get; set;}
    [Required]
    public string name { get; set;}
    public Patient patient;
}