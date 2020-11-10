using System;
using System.Threading.Tasks;
using Gateway.Http.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Gateway.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpException httpException)
            {
                context.Response.StatusCode = (int) httpException.HttpStatusCode;
                context.Response.ContentType = "application/json";
                WriteClassIntoResponse(httpException.ConvertDto(), context);
            }
            catch (Exception exception)
            {
                Console.WriteLine("EEEEEEEEE"); // TODO Async controller
                Console.WriteLine(exception); 
            }
        }

        private void WriteClassIntoResponse(object obj, HttpContext context)
        {
            context.Response.WriteAsync(JsonConvert.SerializeObject(obj)).Wait();
        }
    }
}