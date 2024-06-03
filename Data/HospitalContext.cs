namespace APBD_09.Data;

using Microsoft.EntityFrameworkCore;
using APBD_09.Models;

public class HospitalContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }

    public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prescription>()
            .HasMany(p => p.Medicaments)
            .WithMany(m => m.Prescriptions);
    }
}
