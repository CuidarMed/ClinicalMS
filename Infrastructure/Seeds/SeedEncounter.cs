using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds
{
    public class SeedEncounter: IEntityTypeConfiguration<Encounter>
    {
        public void Configure(EntityTypeBuilder<Encounter> builder)
        {
            builder.HasData(
                    new Encounter { 
                        EncounterId = 1, 
                        PateientId = 1, 
                        DoctorId = 3, 
                        AppointmentId = 1, 
                        Reasons = "Dolor de espalda", 
                        Subjective = "Dolor intenso arriba a un costado de la espalda", 
                        Objetive = "El dolor es generado por aire", 
                        Assessment = "El dolor fue generado por aire que se concentro en la espalda", 
                        Plan = "Pasar azufre sobre la zona afectada", 
                        Notes = "Ninguna", 
                        Status = "Open", 
                        Date = new DateTime(2025, 8, 20), 
                        CreatedAt = new DateTimeOffset(2025, 8, 20, 8, 0, 0, TimeSpan.Zero), 
                        UpdatedAt = new DateTimeOffset(2025, 8, 20, 8, 15, 0, TimeSpan.Zero)
                    },
                    new Encounter { 
                        EncounterId = 2, 
                        PateientId = 4, 
                        DoctorId = 1, 
                        AppointmentId = 2, 
                        Reasons = "Dolor de cabeza", 
                        Subjective = "Dolor de cabeza", 
                        Objetive = "Resfrio", 
                        Assessment = "El dolor de cabeza debido a fiebre. El paciente tiene una temperatura de 38°", 
                        Plan = "reposo y tomar antigripal cada 12hs", 
                        Notes = "Ninguna", 
                        Status = "Signed", 
                        Date = new DateTime(2025, 9, 11), 
                        CreatedAt = new DateTimeOffset(2025, 9, 11, 10, 0, 0, TimeSpan.Zero), 
                        UpdatedAt = new DateTimeOffset(2025, 9, 11, 10, 20, 0, TimeSpan.Zero)
                    },
                    new Encounter { 
                        EncounterId = 3, 
                        PateientId = 2, 
                        DoctorId = 2, 
                        AppointmentId = 3, 
                        Reasons = "Tos y mocos", 
                        Subjective = "Tos fuerte y acumulacion de mocos", 
                        Objetive = "Resfrio", 
                        Assessment = "El paciente se contagio de un resfriado", 
                        Plan = "Resposo, tomar un antigripal cada 8hs y realizar vapores para sacar los mocos", 
                        Notes = "Ninguna", 
                        Status = "Signed", 
                        Date = new DateTime(2025, 10, 15), 
                        CreatedAt = new DateTimeOffset(2025, 10, 15, 15, 10, 0, TimeSpan.Zero), 
                        UpdatedAt = new DateTimeOffset(2025, 10, 15, 15, 30, 0, TimeSpan.Zero)
                    }
                );
        }
    }
}
