using DatabaseTutor.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using UnitOfWork.DIHelper;

namespace DatabaseTutorApi.API.Utilities
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            using var serviceScope = ServiceActivator.GetScope();
            //serviceScope.ServiceProvider.GetRequiredService<IErrorLogsRepo>().Post(new ErrorLog()
            //{
            //    ExceptionMessage = exception.Message,
            //    StackTrace = exception.StackTrace,
            //    Source = exception.Source,
            //    CreatedDate = DateTime.UtcNow
            //}, true);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseDTO<string>()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Message = "Something unexpected happened! please review data for more details.",
                Data = exception.StackTrace
            }));
        }
    }
}
