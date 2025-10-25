

namespace Api.Shared.Models;

public record Medication
{
    public Guid id { get; init; } = Guid.NewGuid();
    public string title { get; set;}
    public string name { get; set;}
}