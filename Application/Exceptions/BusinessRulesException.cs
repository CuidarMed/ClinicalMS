using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    /// <summary>
    /// Se lanza cuando se viola una regla de negocio.
    /// Por ejemplo intertar firmar un encuentro que ya esta firmado
    /// </summary>
    public class BusinessRulesException : ApplicationException
    {
        public BusinessRulesException(string message) : base(message) { }
    }
}
