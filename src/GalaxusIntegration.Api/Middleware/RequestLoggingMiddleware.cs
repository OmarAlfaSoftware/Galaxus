using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GalaxusIntegration.Api.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestLoggingMiddleware(RequestDelegate next) => _next = next;
        public async Task Invoke(HttpContext context)
        {
            // Log request here (Serilog)
            await _next(context);
        }
    }
}