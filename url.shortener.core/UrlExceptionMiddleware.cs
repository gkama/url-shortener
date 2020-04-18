using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

using url.shortener.data;

namespace url.shortener.core
{
    public class UrlExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        private const string _defaultMessage = "An unexpected error has occurred.";

        public UrlExceptionMiddleware(RequestDelegate next, ILogger<UrlExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                if (httpContext.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                var id = string.IsNullOrEmpty(httpContext?.TraceIdentifier)
                    ? Guid.NewGuid().ToString()
                    : httpContext.TraceIdentifier;

                _logger.LogError(e, $"an exception was thrown during the request. {id}");

                await WriteExceptionResponseAsync(httpContext, e, id);
            }
        }

        private async Task WriteExceptionResponseAsync(HttpContext httpContext, Exception e, string id)
        {
            var canViewSensitiveInfo = _env
                .IsDevelopment();

            var problem = new ProblemDetails()
            {
                Title = canViewSensitiveInfo
                    ? e.Message
                    : _defaultMessage,
                Detail = canViewSensitiveInfo
                    ? e.Demystify().ToString()
                    : null,
                Instance = $"url:error:{id}"
            };

            if (e is UrlException ue)
                problem.Status = ue.StatusCode;
            else
                problem.Status = StatusCodes.Status500InternalServerError;

            var problemjson = JsonSerializer
                .Serialize(problem);

            httpContext.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response
                .WriteAsync(problemjson);
        }
    }

    public static class HttpStatusCodeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseUrlException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UrlExceptionMiddleware>();
        }
    }
}
