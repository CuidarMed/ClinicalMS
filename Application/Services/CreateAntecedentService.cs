using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Servicio de aplicación responsable de la creación de antecedentes clínicos.
    /// Forma parte de la capa Application, y delega la persistencia al Command.
    /// </summary>
    public class CreateAntecedentService : ICreateAntecedentService
    {
        private readonly IAntecedentCommand _antecedentCommand;

        public CreateAntecedentService(IAntecedentCommand antecedentCommand)
        {
            _antecedentCommand = antecedentCommand;
        }

        /// <summary>
        /// Crea un nuevo antecedente clínico asociado a un paciente.
        /// </summary>
        /// <param name="patientId">Id del paciente (recibido desde la ruta)</param>
        /// <param name="request">Datos del antecedente a crear</param>
        /// <returns>DTO del antecedente creado</returns>
        public async Task<AntecedentResponse> CreateAsync(long patientId, AntecedentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "El request no puede ser nulo.");

            // Creamos la entidad Antecedent con el ID del paciente que viene por parámetro.
            var antecedent = new Antecedent
            {
                PatientId = patientId, // asignación correcta desde la URL
                Category = request.Category,
                Description = request.Description,
                StartDate = request.StartDate ?? DateTime.UtcNow,
                EndTime = request.EndTime ?? DateTime.MinValue,
                Status = request.Status,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            // Delegamos la persistencia al Command (Infraestructura)
            var created = await _antecedentCommand.CreateAsync(antecedent);

            // Devolvemos un DTO de respuesta, mapeando la entidad creada
            return new AntecedentResponse
            {
                AntecedentId = created.AntecedentId,
                PatientId = created.PatientId,
                Category = created.Category,
                Description = created.Description,
                StartDate = created.StartDate,
                EndDate = created.EndTime,
                Status = created.Status,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }
    }
}
