using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(string message) : base(message) { }


        // Constructor para multiples errores de validacion
        public ValidationException(IDictionary<string, string[]> errors) : base("Se encontraron uno o mas errores.") { 
            Errors = errors;
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
