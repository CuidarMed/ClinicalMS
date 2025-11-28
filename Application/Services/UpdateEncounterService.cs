using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{

    public class UpdateEncounterService : IUpdateEncounterService
    {
        private readonly IEncounterCommand _command;
        private readonly IEncounterQuery _query;
        public UpdateEncounterService(IEncounterCommand command, IEncounterQuery query)
        {
            _command = command;
            _query = query;
        }
        public async Task<UpdateEncounterResponse> UpdateAsync(int encounterId,UpdateEncounterRequest request)
        {
            var encounter =  _query.GetEncounterById(encounterId).Result;
            if (encounter == null)
                throw new Exception("Cita no encontrada");
            
            // Actualizar las propiedades del encuentro
            encounter.Reasons = request.Reasons;
            encounter.Subjective = request.Subjective;
            encounter.Objetive = request.Objetive;
            encounter.Assessment = request.Assessment;
            encounter.Plan = request.Plan;
            encounter.Notes = request.Notes;
            encounter.Status = request.Status;

            // Llamar al comando para actualizar el encuentro en la base de datos
            await _command.UpdateEncounter(encounter);

            // Crear la respuesta con los datos actualizados
            var response = new UpdateEncounterResponse
            {
                EncounterId = encounter.EncounterId,
                AppointmentId = encounter.AppointmentId,
                Status = encounter.Status
            };

            return response;
        }
    }
}
