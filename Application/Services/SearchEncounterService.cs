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
    public class SearchEncounterService : ISearchEncounterService
    {
        private readonly IEncounterQuery _encountersQuery;
        private readonly IMapper _mapper;

        public SearchEncounterService(IEncounterQuery encountersQuery, IMapper mapper)
        {
            _encountersQuery = encountersQuery;
            _mapper = mapper;
        }

        public async Task<EncounterResponse> SeachEncounterService(int id)
        {
            var encounter = await _encountersQuery.GetEncounterById(id);

            if (encounter == null)
            {
                throw new Exception("No se encontro la cita.");
            }

            if (encounter.Status == "Open")
                throw new Exception("La cita esta en curso o todavia no se realizo");

            else
                // Convierte Entidad => Responce
                return _mapper.Map<EncounterResponse>(encounter);
        }
    }
}
