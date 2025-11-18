using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ClinicalMS.Models;

namespace ClinicalMS.Middleware
{
    public class ExceptionMiddleware
    {
        /// <summary>
        /// Middleware que intercepta TODAS las excepciones de la aplicación
        /// y las convierte en respuestas HTTP estandarizadas.
        /// 
        /// Flujo:
        /// 1. Una petición entra al pipeline
        /// 2. Si ocurre una excepción en cualquier parte, este middleware la captura
        /// 3. Determina qué tipo de excepción es
        /// 4. Crea una respuesta apropiada (ErrorResponse)
        /// 5. Retorna la respuesta al cliente
        /// </summary>

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <param name="next">El siguiente middleware en el pipeline</param>
        /// <param name="logger">Logger para registrar errores</param>

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Metodo principal que se ejecuta en cada peticion HTTP
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        // Maneja la excepcion y crea una respuesta HTTP apropiada
        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponce();

            // Determinamos el tipo de excepciones y cinfigurar la respuesta apropiada
            switch (ex)
            {
                // Entidad no encontrada
                case NotFoundException notFoundEx:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = notFoundEx.Message;

                    // Log como warning
                    _logger.LogWarning(notFoundEx,
                        "Elemento no encotrado: {Message}",
                        notFoundEx.Message);
                    break;

                // Violacion de regla de negocio
                case BusinessRulesException businessEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = businessEx.Message;

                    _logger.LogWarning(businessEx,
                        "Violacion de regla de negocio: {Message}",
                        businessEx.Message);
                    break;

                case ValidationException validationEx:
                    context.Response.StatusCode= (int)HttpStatusCode.BadRequest;
                    response.StatusCode= (int)HttpStatusCode.BadRequest;
                    response.Message = "Error de validación";

                    response.Errors = validationEx.Errors;

                    _logger.LogWarning(validationEx,
                        "Error de validacion: {ErrorCount} errores encontrados",
                        validationEx.Errors?.Count ?? 0);
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "Ocurrio un error interno en el servidor";

                    _logger.LogWarning(ex,
                        "Error no controlado: {Message}",
                        ex.Message);
                    break;
            }

            // Configurar opciones de serializacion JSON
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // camalCase
                WriteIndented = true, // JSON formateado
            };

            // Serializar la respuesta a JSON
            var result = JsonSerializer.Serialize(response, jsonOptions);

            // Escribir la respuesta al cliente
            await context.Response.WriteAsync(result);
        }
    }
}
