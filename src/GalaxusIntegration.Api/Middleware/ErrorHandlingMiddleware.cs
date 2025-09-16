using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GalaxusIntegration.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next) => _next = next;
        public async Task Invoke(HttpContext context)
        {
            try { await _next(context); }
            catch { context.Response.StatusCode = 500; }
        }
    }
}