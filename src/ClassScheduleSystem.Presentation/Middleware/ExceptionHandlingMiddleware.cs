
using ClassScheduleSystem.Application.Exceptions;
using ClassScheduleSystem.Domain.Exceptions;
using ClassScheduleSystem.Infrastructure.Exceptions;

namespace ClassScheduleSystem.Presentation.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Domain validation error.");

            await WriteErrorAsync(context, 400, ex.Message);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Requested resource was not found.");

            await WriteErrorAsync(context, 404, ex.Message);
        }
        catch (DataAccessException ex)
        {
            _logger.LogError(ex, "Database access error.");

            await WriteErrorAsync(
                context,
                500,
                "A database error occurred. Please try again later.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");

            await WriteErrorAsync(
                context,
                500,
                "An unexpected server error occurred.");
        }
    }

    private static async Task WriteErrorAsync(
        HttpContext context,
        int statusCode,
        string message)
    {
        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            error = message
        });
    }
}
