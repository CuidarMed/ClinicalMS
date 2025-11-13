using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAntecedentCommand
    {
        Task<int> DeleteAsync(Antedecent antedecent);
        Task<Antedecent> updateAntecedent(int antecedentId, AntecedentUpdate antecedent);
    }
}
