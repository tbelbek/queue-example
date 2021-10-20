using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ReportingWith_Queue;

namespace RegistrationWeb.Middleware
{
    public class ResponseWrapper
    {
        private readonly RequestDelegate _next;
        private readonly IResponseHelper _responseHelper;

        public ResponseWrapper(RequestDelegate next, IResponseHelper responseHelper)
        {
            _next = next;
            _responseHelper = responseHelper;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentBody = context.Response.Body;

            using var memoryStream = new MemoryStream(); //new in C# 9.0 ;)
            //set the current response to the memorystream.
            context.Response.Body = memoryStream;

            await _next(context);

            //reset the body 
            context.Response.Body = currentBody;
            memoryStream.Seek(0, SeekOrigin.Begin);

            var readToEnd = new StreamReader(memoryStream).ReadToEnd();
            var objResult = JsonConvert.DeserializeObject(readToEnd);
            var result = _responseHelper.ResponseGenerator(objResult,
                context.Response.StatusCode == (int)HttpStatusCode.OK, null);

            await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }

    public static class ResponseWrapperExtensions
    {
        public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseWrapper>();
        }
    }
}