using Api.Shared.Models;

namespace Api.Repositories;

public interface IDoctorRepository
{
    Task<IEnumerable<Doctor>> GetAllDoctors();
    Task<Doctor> GetDoctorById(int id);
    Task<IEnumerable<Doctor>> GetDoctorsByName(string name);
    Task<Doctor> CreateDoctor(Doctor doctor);
    Task<Doctor> UpdateDoctor(Doctor doctor);
    Task DeleteDoctor(int id);
    Task<bool> DoctorEmailExist(string email);
}