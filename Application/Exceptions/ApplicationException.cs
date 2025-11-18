using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    /// <summary>
    /// Creamos una clase base para todas las excepciones de la aplicacion.
    /// Heredar de esta clase permite capturar todas las excepciones personalizadas. 
    /// </summary>
    public class ApplicationException : Exception
    {
        protected ApplicationException(string message) : base(message) { }

        protected ApplicationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
