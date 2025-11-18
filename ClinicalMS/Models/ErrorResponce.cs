using System;
using System.Collections.Generic;

namespace ClinicalMS.Models
{
    public class ErrorResponce
    {
        // Codigo de estado HTTP (400, 404, 500, etc)
        public int StatusCode { get; set; }

        // Mensaje principal del error
        public string Message { get; set; }

        // Diccionario de errores de validación (Solo para validationException)
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
