using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SearchAntecedentService : ISearchAntecedentService
    {
        private readonly IAntecedentQuery _antecedentQuery;

        public SearchAntecedentService(IAntecedentQuery antecedentQuery)
        {
            _antecedentQuery = antecedentQuery;
        }

        /// <summary>
        /// Obtiene un antecedente específico por su ID.
        /// </summary>
        public async Task<AntecedentResponse?> GetByIdAsync(long id)
        {
            try
            {
                var antecedent = await _antecedentQuery.GetByIdAsync(id);

                if (antecedent == null)
                    return null;

                return MapToResponse(antecedent);
            }
            catch (Exception ex)
            {
                // En un entorno real podrías loguear el error con ILogger
                throw new ApplicationException($"Error al obtener el antecedente con ID {id}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene todos los antecedentes existentes.
        /// </summary>
        /// <returns>Lista de AntecedentResponse</returns>
        public async Task<List<AntecedentResponse>> GetAllAsync()
        {
            try
            {
                var antecedents = await _antecedentQuery.GetAllAsync();

                return antecedents.Select(MapToResponse).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener los antecedentes: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Mapea manualmente la entidad Antecedent al DTO AntecedentResponse.
        /// </summary>
        private static AntecedentResponse MapToResponse(Antecedent antecedent)
        {
            return new AntecedentResponse
            {
                AntecedentId = antecedent.AntecedentId,
                PatientId = antecedent.PatientId,
                Category = antecedent.Category,
                Description = antecedent.Description,
                StartDate = antecedent.StartDate,
                EndDate = antecedent.EndTime,
                Status = antecedent.Status,
                CreatedAt = antecedent.CreatedAt,
                UpdatedAt = antecedent.UpdatedAt
            };
        }
    }
}
