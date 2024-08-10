using Newtonsoft.Json;
using System.Net;

namespace BookStoreApp.Api.Middleware
{
    public class CustomUnauthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomUnauthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    statusCode = context.Response.StatusCode,
                    message = "You are not authorized to access this resource."
                }));
            }
        }
    }

}
