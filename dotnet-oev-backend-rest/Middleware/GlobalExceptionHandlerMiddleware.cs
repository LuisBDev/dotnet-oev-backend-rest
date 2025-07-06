using System.Net;
using System.Text.Json;
using dotnet_oev_backend_rest.Exceptions;

namespace dotnet_oev_backend_rest.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Intenta ejecutar el siguiente middleware en el pipeline
            await _next(context);
        }
        catch (Exception ex)
        {
            // Si ocurre una excepción, la manejamos aquí
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode;
        string message;

        // Definimos un tipo para la respuesta de error
        var errorResponse = new
        {
            title = "An error occurred",
            message = exception.Message
        };

        // Asignamos el código de estado HTTP basado en el tipo de excepción
        switch (exception)
        {
            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorResponse = errorResponse with { title = "Resource Not Found" };
                break;
            case ForbiddenException:
                statusCode = HttpStatusCode.Forbidden;
                errorResponse = errorResponse with { title = "Access Forbidden" };
                break;
            // case ValidationException:
            //     statusCode = HttpStatusCode.BadRequest;
            //     break;
            case AppException:
                statusCode = HttpStatusCode.InternalServerError;
                errorResponse = errorResponse with { title = "Application Error" };
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                errorResponse = errorResponse with { title = "Internal Server Error" };
                _logger.LogError(exception, "An unhandled exception has occurred.");
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        // Escribimos la respuesta JSON
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}