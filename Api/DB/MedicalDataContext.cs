using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.DB;

public class MedicalDataContext: DbContext
{
    public MedicalDataContext(DbContextOptions options) : base(options)
    { }

    public virtual DbSet<Patient> Patients { get; set; }
}