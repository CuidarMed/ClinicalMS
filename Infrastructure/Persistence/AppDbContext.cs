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
                entity.ToTable("Attchment");
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
        }
    }
}
