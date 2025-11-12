using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class AntecedentQuery : IAntecedentQuery
    {
        private readonly AppDbContext _context;

        public AntecedentQuery(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene un antecedente por su identificador único.
        /// </summary>
        /// <param name="id">Identificador del antecedente</param>
        /// <returns>Entidad Antecedent o null si no existe</returns>
        public async Task<Antecedent?> GetByIdAsync(long id)
        {
            return await _context.Antecedents
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AntecedentId == id);
        }

        /// <summary>
        /// Obtiene todos los antecedentes disponibles.
        /// </summary>
        /// <returns>Lista de entidades Antecedent</returns>
        public async Task<List<Antecedent>> GetAllAsync()
        {
            return await _context.Antecedents
                .AsNoTracking()
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }
    }
}
