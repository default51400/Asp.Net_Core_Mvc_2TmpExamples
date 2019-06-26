using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ConfiguringApps.Infrastructure
{
    public class ContentMiddleware
    {
        private RequestDelegate _nextDelegate;
        private UptimeService _uptime;

        public ContentMiddleware(RequestDelegate next, UptimeService up)
        {
            _nextDelegate = next;
            _uptime = up;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.ToString().ToLower() == "/middleware")
                await httpContext.Response.WriteAsync(
                    "This is from the content middleware"+ 
                    $"(uptime: {_uptime.Uptime}ms++)", Encoding.UTF8);
            else await _nextDelegate.Invoke(httpContext);
        }
    }
}
