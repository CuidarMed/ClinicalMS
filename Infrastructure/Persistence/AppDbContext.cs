using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Antedecent> Antedecents {  get; set; }
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Antecedents
            modelBuilder.Entity<Antedecent>(entity =>
            {
                entity.ToTable("Antedecents");
                entity.HasKey(a => a.AntedecentId);
                entity.Property(a => a.AntedecentId).ValueGeneratedOnAdd();
                entity.Property(a => a.PatientId).IsRequired();
                entity.Property(a => a.Category).IsRequired().HasMaxLength(200);
                entity.Property(a => a.Description).IsRequired();
                entity.Property(a => a.StartDate).IsRequired();
                entity.Property(a => a.EndTime).IsRequired();
                entity.Property(a => a.Status).IsRequired();
                entity.Property(a => a.CreatedAt).IsRequired();
            });

            // Encounter
            modelBuilder.Entity<Encounter>(entity =>
            {
                entity.ToTable("Encounters");
                entity.HasKey(e => e.EncounterId);
                entity.Property(e => e.EncounterId).ValueGeneratedOnAdd();
                entity.Property(e => e.PatientId).IsRequired();
                entity.Property(e => e.DoctorId).IsRequired();
                entity.Property(e => e.AppointmentId).IsRequired();
                entity.Property(e => e.Reasons).IsRequired().HasMaxLength(225);
                entity.Property(e => e.Subjective).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.Objetive).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.Assessment).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.Plan).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.Notes).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();
            });

            // Attachment
            modelBuilder.Entity<Attachment>(entity => {
                entity.ToTable("Attachment");
                entity.HasKey(a => a.AttachmentId);
                entity.Property(a => a.AttachmentId).ValueGeneratedOnAdd();
                entity.Property(a => a.PatientId).IsRequired();
                entity.Property(a => a.EncounterId).IsRequired();
                entity.Property(a => a.FileName).IsRequired().HasMaxLength(80);
                entity.Property(a => a.FileType).IsRequired().HasMaxLength(50);
                entity.Property(a => a.FileUrl).IsRequired();
                entity.Property(a => a.Notes).IsRequired();
                entity.Property(a => a.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.encounter)
                .WithMany(e => e.attachments)
                .HasForeignKey(a => a.EncounterId);

            // Prescription
            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.ToTable("Prescriptions");
                entity.HasKey(p => p.PrescriptionId);
                entity.Property(p => p.PrescriptionId).ValueGeneratedOnAdd();
                entity.Property(p => p.PatientId).IsRequired();
                entity.Property(p => p.DoctorId).IsRequired();
                entity.Property(p => p.Diagnosis).IsRequired().HasMaxLength(500);
                entity.Property(p => p.Medication).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Dosage).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Frequency).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Duration).IsRequired().HasMaxLength(100);
                entity.Property(p => p.AdditionalInstructions).HasMaxLength(1000);
                entity.Property(p => p.PrescriptionDate).IsRequired();
                entity.Property(p => p.CreatedAt).IsRequired();
                entity.Property(p => p.UpdatedAt).IsRequired();
            });
        }
    }
}
