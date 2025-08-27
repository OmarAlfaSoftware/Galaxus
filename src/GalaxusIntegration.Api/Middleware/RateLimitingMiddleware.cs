using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GalaxusIntegration.Api.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        public RateLimitingMiddleware(RequestDelegate next) => _next = next;
        public async Task Invoke(HttpContext context)
        {
            // Implement rate limiting logic here
            await _next(context);
        }
    }
}