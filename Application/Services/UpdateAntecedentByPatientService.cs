using Application.DTOs;
using Application.Interfaces;
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

        public UpdateAntecedentByPatientService(IAntecedentCommand command, IAntecedentQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<AntecedentResponse> UpdateAntecedentByPatientAsync(long patientId, int antecedentId, AntecedentUpdate update)
        {
            var antecedent = await _query.GetByIdAsync(antecedentId);

            if (antecedent == null)
                throw new Exception("Cita no encontrada.");

            if (patientId != antecedent.PatientId)
                throw new Exception("El paciente dado es incorrecto");

            var updateAntecedent = await _command.updateAntecedent(antecedentId, update);

            return await Task.FromResult(new AntecedentResponse(
                    AntecedentId: updateAntecedent.AntedecentId,
                    PatientId: updateAntecedent.PatientId,
                    Category: updateAntecedent.Category,
                    Description: updateAntecedent.Description,
                    StartDate: updateAntecedent.StartDate,
                    EndDate: updateAntecedent.EndTime,
                    Status: updateAntecedent.Status,
                    CreatedAt: updateAntecedent.CreatedAt,
                    UpdatedAt: updateAntecedent.UpdatedAt
                ));
        }
    }
}
