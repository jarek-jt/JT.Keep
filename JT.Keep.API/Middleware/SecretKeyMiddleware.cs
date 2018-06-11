using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace JT.Keep.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SecretKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _key = "secret-user-key";
        private readonly string _valueOfSecretKey = "123";


        public SecretKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!CheckHeader(context))
            {
                context.Response.StatusCode = 400; //Bad Request                
                await context.Response.WriteAsync("User Key is missing");
                return;
            }

            if (!CheckKey(context))
            {
                context.Response.StatusCode = 401; //UnAuthorized
                await context.Response.WriteAsync("Invalid User Key");
                return;
            }

           
            await _next(context);
        }

        private bool CheckKey(HttpContext context)
        {
            var sentKey=context.Request.Headers[_key];

            return sentKey == _valueOfSecretKey;

        }

        private bool CheckHeader(HttpContext context)
        {
            return context.Request.Headers.Keys.Contains(_key);

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SecretKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecretKeyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecretKeyMiddleware>();
        }
    }
}
