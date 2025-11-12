using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DeleteAntecedentService : IDeleteAntecedentService
    {
        private readonly IAntecedentCommand command;
        private readonly IAntecedentQuery query;

        public DeleteAntecedentService(IAntecedentCommand command,IAntecedentQuery query)
        {
            this.command = command;
            this.query = query;
        }
        public async Task<AntecedentResponse?> DeleteAsync(int id)
        {
            var antecedent = await query.GetByIdAsync(id);
            if (antecedent == null)
            {
                return null;
            }

            antecedent.Status = "eliminada";
            await command.DeleteAsync(antecedent);

            return new AntecedentResponse
            (
                id,
                antecedent.PatientId,
                antecedent.Category,
                antecedent.Description,
                antecedent.StartDate,
                antecedent.EndTime,
                antecedent.Status,
                antecedent.CreatedAt,
                antecedent.UpdatedAt
            );
        }
    }
}
