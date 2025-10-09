using Api.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.DB;

public class MedicalDataContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<Patient> Patients { get; set; }
}