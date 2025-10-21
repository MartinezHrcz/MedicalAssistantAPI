using Api.Shared.Models;

namespace Api.Services;

public interface IJwtService
{
    string GenerateToken(Patient patient);
    string GenerateToken(Doctor doctor);
}