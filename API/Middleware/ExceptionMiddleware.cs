using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        /* we must use Request Delgete because its a middleware
        A task that represents the completion of request processing is Reqiest delegate  Because middleware goes from one bit of middleware to the next bit of middleware to the next bit of
        middleware always calling next.
        And that's what this request delegate is*/
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        // async Task InvokeAsynch is to tell our framework this is middleware
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // here we say await our next passing through the HTTP context
                await _next(context);
            }
            catch (Exception ex)
            {
                //exceptions of middleware going to be handled
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, ex.Message, "Internal server error");
                //returning our response n json
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }

    }
}