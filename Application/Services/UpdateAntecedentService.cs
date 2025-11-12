using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UpdateAntecedentService : IUpdateAntecedentService
    {
        private readonly IAntecedentCommand _antecedentCommand;
        private readonly IAntecedentQuery _antecedentQuery;

        public UpdateAntecedentService(IAntecedentCommand antecedentCommand, IAntecedentQuery antecedentQuery)
        {
            _antecedentCommand = antecedentCommand;
            _antecedentQuery = antecedentQuery;
        }

        public async Task<AntecedentResponse> UpdateAntecedent(long id, AntecedentRequest request)
        {
            // Buscar el antecedente existente
            var antecedent = await _antecedentQuery.GetByIdAsync(id);
            if (antecedent == null)
            {
                return null; // Si no existe, devolvemos null (sin lanzar excepción)
            }

            // Actualizar solo los campos que no sean nulos o vacíos
            if (!string.IsNullOrWhiteSpace(request.Category))
                antecedent.Category = request.Category;

            if (!string.IsNullOrWhiteSpace(request.Description))
                antecedent.Description = request.Description;

            if (request.StartDate.HasValue)
                antecedent.StartDate = request.StartDate.Value;

            if (request.EndTime.HasValue)
                antecedent.EndTime = request.EndTime.Value;

            if (!string.IsNullOrWhiteSpace(request.Status))
                antecedent.Status = request.Status;

            antecedent.UpdatedAt = DateTimeOffset.UtcNow;

            // Guardar los cambios mediante el Command
            var updatedAntecedent = await _antecedentCommand.UpdateAsync(id, request);

            // Retornar la respuesta mapeada
            return new AntecedentResponse
            {
                AntecedentId = updatedAntecedent.AntecedentId,
                PatientId = updatedAntecedent.PatientId,
                Category = updatedAntecedent.Category,
                Description = updatedAntecedent.Description,
                StartDate = updatedAntecedent.StartDate,
                EndDate = updatedAntecedent.EndTime,
                Status = updatedAntecedent.Status,
                CreatedAt = updatedAntecedent.CreatedAt,
                UpdatedAt = updatedAntecedent.UpdatedAt
            };
        }
    }
}
