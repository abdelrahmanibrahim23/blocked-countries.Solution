using blocked_countries.Errors;
using Microsoft.AspNetCore.Routing.Matching;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text.Json;

namespace blocked_countries.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;
        private readonly RequestDelegate next;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger,IHostEnvironment env,RequestDelegate next)
        {
            this.logger = logger;
            this.env = env;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await next.Invoke(context);
            }
            catch (Exception ex)
            {
                #nullable disable
                logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/Json";
                context.Response.StatusCode=(int) HttpStatusCode.InternalServerError;
                var exceptionErrorResponce = env.IsDevelopment() ?
                    new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString())
                    :
                    new ApiExceptionResponse(500);
                var option =new JsonSerializerOptions(){ PropertyNamingPolicy=JsonNamingPolicy.CamelCase };
                var json=JsonSerializer.Serialize(exceptionErrorResponce, option);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
