using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.Text;
using System;
using System.Threading.Tasks;

namespace PictureManagerApi.Middleware
{
    public class PhysicalPathMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IFileProvider _fileProvider;

        public PhysicalPathMiddleware(RequestDelegate next, IFileProvider fileProvider)
        {
            _next = next;
            _fileProvider = fileProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            var output = new StringBuilder("");
            

            await context.Response.WriteAsync(output.ToString());
        }
    }

    public static class UsePhysicalPathExtensions
    {
        public static IApplicationBuilder UsePhysicalPath(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PhysicalPathMiddleware>();
        }
    }
}