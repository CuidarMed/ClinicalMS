using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppValidationException = Application.Exceptions.ValidationException;

namespace Application.Services
{
    public class CreateEncounterService : ICreateEncounterService
    {
        private readonly IEncounterCommand command;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateEncounterRequest> _validator;

        public CreateEncounterService(IEncounterCommand command, IMapper mapper, IValidator<CreateEncounterRequest> validator)
        {
            this.command = command;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<EncounterResponse> CreateAsync(CreateEncounterRequest request)
        {
            // Validamos el request antes de procesar
            var validation = await _validator.ValidateAsync(request);
            if(!validation.IsValid) {
                var errors = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
                throw new AppValidationException(errors);
            }

            // Convertir Request => Entidad
            var encounter = _mapper.Map<Encounter>(request);
         
            await command.InsertAsync(encounter);
           
            // Convertir Entidad => Responce
            return _mapper.Map<EncounterResponse>(encounter);
        }
    }
}
