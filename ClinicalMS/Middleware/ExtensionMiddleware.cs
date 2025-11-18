using Microsoft.AspNetCore.Builder;
using ClinicalMS.Middleware;


namespace ClinicalMS.Middleware
{
    public static class ExtensionMiddleware
    {
        // Registra el middleware de manejo de excepciones en el pipeline
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
