using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            // Mapeo simple (propiedades con mismo nombre) para records
            CreateMap<Encounter, EncounterResponse>()
                .ForCtorParam("EncounterId", opt => opt.MapFrom(src => src.EncounterId))
                .ForCtorParam("PatientID", opt => opt.MapFrom(src => src.PatientId))
                .ForCtorParam("DoctorID", opt => opt.MapFrom(src => src.DoctorId))
                .ForCtorParam("AppointmentId", opt => opt.MapFrom(src => src.AppointmentId))
                .ForCtorParam("Reasons", opt => opt.MapFrom(src => src.Reasons))
                .ForCtorParam("Subjective", opt => opt.MapFrom(src => src.Subjective))
                .ForCtorParam("Objetive", opt => opt.MapFrom(src => src.Objetive))
                .ForCtorParam("Assessment", opt => opt.MapFrom(src => src.Assessment))
                .ForCtorParam("Plan", opt => opt.MapFrom(src => src.Plan))
                .ForCtorParam("Notes", opt => opt.MapFrom(src => src.Notes))
                .ForCtorParam("Status", opt => opt.MapFrom(src => src.Status))
                .ForCtorParam("Date", opt => opt.MapFrom(src => src.Date))
                .ForCtorParam("createdAt", opt => opt.MapFrom(src => src.CreatedAt))
                .ForCtorParam("UpdatedAt", opt => opt.MapFrom(src => src.UpdatedAt));

            // Mapeo inverso (para Request)
            CreateMap<CreateEncounterRequest, Encounter>()
                .ForMember(dest => dest.EncounterId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<Antedecent, AntecedentResponse>()
                .ForCtorParam("AntecedentId", opt => opt.MapFrom(src => src.AntedecentId))
                .ForCtorParam("PatientId", opt => opt.MapFrom(src => src.PatientId))
                .ForCtorParam("Category", opt => opt.MapFrom(src => src.Category))
                .ForCtorParam("Description", opt => opt.MapFrom(src => src.Description))
                .ForCtorParam("StartDate", opt => opt.MapFrom(src => src.StartDate))
                .ForCtorParam("EndDate", opt => opt.MapFrom(src => src.EndTime))
                .ForCtorParam("Status", opt => opt.MapFrom(src => src.Status))
                .ForCtorParam("CreatedAt", opt => opt.MapFrom(src => src.CreatedAt))
                .ForCtorParam("UpdatedAt", opt => opt.MapFrom(src => src.UpdatedAt));

            CreateMap<AntecedentRequest, Antedecent>()
                .ForMember(dest => dest.AntedecentId, opt => opt.Ignore())
                .ForMember(dest => dest.PatientId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<AntecedentUpdate, Antedecent>()
                .ForMember(dest => dest.AntedecentId, opt => opt.Ignore())
                .ForMember(dest => dest.PatientId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<Attachment, AttachmentResponse>()
                .ForCtorParam("AttachmentId", opt => opt.MapFrom(src => src.AttachmentId))
                .ForCtorParam("PatientId", opt => opt.MapFrom(src => src.PatientId))
                .ForCtorParam("EncounterId", opt => opt.MapFrom(src => src.EncounterId))
                .ForCtorParam("FileName", opt => opt.MapFrom(src => src.FileName))
                .ForCtorParam("FileType", opt => opt.MapFrom(src => src.FileType))
                .ForCtorParam("FileUrl", opt => opt.MapFrom(src => src.FileUrl))
                .ForCtorParam("Notes", opt => opt.MapFrom(src => src.Notes))
                .ForCtorParam("CreatedAt", opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<AttachmentRequest, Attachment>()
                .ForMember(dest => dest.AttachmentId, opt => opt.Ignore())
                .ForMember(dest => dest.PatientId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
