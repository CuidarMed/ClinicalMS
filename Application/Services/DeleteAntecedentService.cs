using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
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
        private readonly IMapper mapper;

        public DeleteAntecedentService(IAntecedentCommand command,IAntecedentQuery query, IMapper mapper)
        {
            this.command = command;
            this.query = query;
            this.mapper = mapper;
        }
        public async Task<AntecedentResponse?> DeleteAsync(int id)
        {
            var antecedent = await query.GetByIdAsync(id);
            if (antecedent == null)
                throw new NotFoundException("El encuentro no existe."); 

            antecedent.Status = "eliminada";
            await command.DeleteAsync(antecedent);

            // Convertir Entidad => Responce
            return mapper.Map<AntecedentResponse>(antecedent);
        }
    }
}
