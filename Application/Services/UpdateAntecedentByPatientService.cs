using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
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

        public UpdateAntecedentByPatientService(IAntecedentCommand command, IAntecedentQuery query, IMapper mapper)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
        }

        public async Task<AntecedentResponse> UpdateAntecedentByPatientAsync(long patientId, int antecedentId, AntecedentUpdate update)
        {
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
