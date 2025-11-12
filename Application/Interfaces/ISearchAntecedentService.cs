using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISearchAntecedentService
    {

        Task<AntecedentResponse?> GetByIdAsync(long id);
        Task<List<AntecedentResponse>> GetAllAsync();
    }
}
