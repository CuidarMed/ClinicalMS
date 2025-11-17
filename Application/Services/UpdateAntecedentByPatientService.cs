using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UpdateAntecedentByPatientService : IUpdateAntecedentByPatient
    {
        private readonly IAntecedentCommand _command;
        private readonly IAntecedentQuery _query;
        private readonly IMapper _mapper;
        private readonly IValidator<AntecedentUpdate> _validator;


        public UpdateAntecedentByPatientService(IAntecedentCommand command, IAntecedentQuery query, IMapper mapper, IValidator<AntecedentUpdate> validator)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<AntecedentResponse> UpdateAntecedentByPatientAsync(long patientId, int antecedentId, AntecedentUpdate update)
        {
            await _validator.ValidateAndThrowAsync(update);

            var antecedent = await _query.GetByIdAsync(antecedentId);

            if (antecedent == null)
                throw new Exception("Cita no encontrada.");

            if (patientId != antecedent.PatientId)
                throw new Exception("El paciente dado es incorrecto");

            var updateAntecedent = await _command.updateAntecedent(antecedentId, update);

            // Convertir Entidad => Responce
            return _mapper.Map<AntecedentResponse>(antecedent);
        }
    }
}
