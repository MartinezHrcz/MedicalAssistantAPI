using Api.DB;
using Api.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class DoctorRepository: IDoctorRepository
{
    
    private readonly MedicalDataContext _context;

    public DoctorRepository(MedicalDataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctors()
    {
        return await _context.Doctors
            .AsNoTracking()
            .OrderBy(doctor => doctor.Name)
            .ToListAsync();
    }

    public async Task<Doctor?> GetDoctorById(int id)
    {
        return await _context.Doctors.FindAsync(id);
    }

    public async Task<IEnumerable<Doctor>> GetDoctorsByName(string name)
    {
        return await _context.Doctors
            .Where(doctor => doctor.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }

    public async Task<Doctor> CreateDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }

    public async Task<Doctor> UpdateDoctor(Doctor doctor)
    {
        Doctor toUpdate = await _context.Doctors.FindAsync(doctor.Id)??  throw new KeyNotFoundException($"Doctor with id {doctor.Id} not found");
        toUpdate.Name = doctor.Name;
        toUpdate.Address = doctor.Address;
        toUpdate.PhoneNumber = doctor.PhoneNumber;
        toUpdate.Email = doctor.Email;
        _context.Doctors.Update(toUpdate);
        await _context.SaveChangesAsync();
        return toUpdate;
    }

    public async Task<bool> DeleteDoctor(int id)
    {
        Doctor doctorToRemove = await _context.Doctors.FindAsync(id);
        if (doctorToRemove == null)
        {
            return false;
        }
        _context.Doctors.Remove(doctorToRemove);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DoctorEmailExist(string email)
    {
        return await _context.Doctors.AnyAsync(doctor => doctor.Email == email);
    }
}