using Api.DB;
using Api.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class PatientRepositroy : IPatientRepository
{

    private readonly MedicalDataContext _context;
    
    public PatientRepositroy(MedicalDataContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Patient>> GetAllPatients()
    {
        return await _context.Patients
            .Include(p => p.doctor)
            .OrderBy(patient => patient.TimeOfAdmission)
            .ToListAsync();
    }

    public async Task<Patient> GetPatientById(int id)
    {
        Patient patient = await _context.Patients.FindAsync(id) 
                          ?? throw new KeyNotFoundException($"Patient not found with id {id}");
        return patient;
    }

    public async Task<IEnumerable<Patient>> GetPatientsByName(string name)
    {
        List<Patient> patients = await _context.Patients
            .Where(patient => patient.Name == name)
            .OrderBy(patient => patient.TimeOfAdmission)
            .ToListAsync();
        return patients;
    }

    public async Task<Patient> GetPatientByTaj(string taj)
    {
        Patient patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Taj == taj) ?? throw new KeyNotFoundException($"Patient not found with TAJ: {taj}");
        
        return patient;
    }

    public async Task<Patient> CreatePatient(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<Patient> UpdatePatient(Patient patient)
    {
        Patient patientToUpdate = await _context.Patients.FindAsync(patient.Id)
            ?? throw new KeyNotFoundException($"Patient not found with id {patient.Id}");
        
        patientToUpdate.Name = patient.Name;
        patientToUpdate.Address = patient.Address;
        patientToUpdate.Complaints  = patient.Complaints;
        patientToUpdate.Taj = patient.Taj;
        patientToUpdate.doctor = patient.doctor;
        patientToUpdate.medications = patient.medications;
        
        _context.Patients.Update(patientToUpdate);
        await _context.SaveChangesAsync();
        return patientToUpdate;
    }

    public async Task DeletePatient(int id)
    {
        Patient patientToRemove = await _context.Patients.FindAsync(id)?? throw new KeyNotFoundException($"Patient not found with id {id}");
        _context.Patients.Remove(patientToRemove);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeletePatientByTaj(string taj)
    {
        Patient patientToRemove = await GetPatientByTaj(taj);
        _context.Patients.Remove(patientToRemove);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> PatientTajExists(string taj)
    {
        return await _context.Patients.AnyAsync(p => p.Taj.Equals(taj));
    }

    public async Task UpdateAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Patient>> GetPatientsByDoctor(int doctorId)
    {
        IEnumerable<Patient> patients = await _context.Patients.Where(p=>p.doctor != null && p.doctor.Id.Equals(doctorId)).ToListAsync();
        return patients;
    }
}