using Application.DTOs;
using Domain.Entities;
using Infrastructure.Persistence;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public class AntecedentCommand : IAntecedentCommand
    {
        private readonly AppDbContext _context;

        public AntecedentCommand(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Crea un nuevo antecedente clínico.
        /// </summary>
        /// <param name="antecedent">Entidad antecedente a crear</param>
        /// <returns>Entidad creada</returns>
        public async Task<Antecedent> CreateAsync(Antecedent antecedent)
        {
            _context.Antecedents.Add(antecedent);
            await _context.SaveChangesAsync();
            return antecedent;
        }

        /// <summary>
        /// Actualiza parcialmente un antecedente existente.
        /// </summary>
        public async Task<Antecedent?> UpdateAsync(long id, AntecedentRequest request)
        {
            var antecedent = await _context.Antecedents.FirstOrDefaultAsync(a => a.AntecedentId == id);

            if (antecedent == null)
                return null;

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

            await _context.SaveChangesAsync();
            return antecedent;
        }
    }
}

