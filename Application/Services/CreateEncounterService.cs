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
    public class CreateEncounterService : ICreateEncounterService
    {
        private readonly IEncounterCommand command;
        private readonly IMapper _mapper;

        public CreateEncounterService(IEncounterCommand command, IMapper mapper)
        {
            this.command = command;
            this._mapper = mapper;
        }
        public async Task<EncounterResponse> CreateAsync(CreateEncounterRequest request)
        {
            // Convertir Request => Entidad
            var encounter = _mapper.Map<Encounter>(request);
         
            var encounterId = await command.InsertAsync(encounter);
           
            // Convertir Entidad => Responce
            return _mapper.Map<EncounterResponse>(encounter);
        }
    }
}
