using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetEncounterRangeService : IGetEncounterRangeService
    {
        private readonly IEncounterQuery query;
        private readonly IMapper mapper;

        public GetEncounterRangeService(IEncounterQuery query, IMapper mapper)
        {
            this.query = query;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<EncounterResponse>> GetEncounterRangeAsync(long patientId, DateTime from, DateTime to)
        {
            var encounters = await query.GetByPatientAsync(patientId);

            if (encounters == null || !encounters.Any())
                return Enumerable.Empty<EncounterResponse>();

             
            var filtered = encounters
                .Where(e => e.Date >= from && e.Date <= to)
                .Where(e => e.Status == "OPEN" || e.Status == "SIGNED")
                .ToList();

            // Convertir Entidad => Responce
            return mapper.Map<IEnumerable<EncounterResponse>>(encounters);
        }
    }
}
