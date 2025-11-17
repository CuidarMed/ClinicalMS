using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SignEncounterService : ISignEncouterService
    {
        private readonly IEncounterQuery _query;
        private readonly IEncounterCommand _command;
        private readonly IMapper _mapper;
        private readonly IValidator<EncounterSign> _validator;

        public SignEncounterService(IEncounterQuery query, IEncounterCommand command, IMapper mapper, IValidator<EncounterSign> validator)
        {
            _query = query;
            _command = command;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<EncounterResponse> SignEncounter(int id, long doctorId, EncounterSign sign)
        {
            await _validator.ValidateAndThrowAsync(sign);

            var encounter = await _query.GetEncounterById(id);

            if (encounter == null)
                throw new Exception("Cita no encontrada");

            if (encounter.Status != "Open")
                throw new Exception("La cita ya fue firmada y no puede modificarse");

            await _command.SignEncounter(id, doctorId, sign);

            encounter = await _query.GetEncounterById(id);

            // Convierte Entidad => Responce
            return _mapper.Map<EncounterResponse>(encounter);
        }
    }
}
