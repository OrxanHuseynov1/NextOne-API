using Domain.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using FluentValidation;

namespace BusinessLayer.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddleware(RequestDelegate next)
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
        context.Response.ContentType = "application/json";
        var statusCode = (int)HttpStatusCode.InternalServerError;

        var errorDetails = new ErrorDetails
        {
            StatusCode = statusCode,
            Message = "Daxili server xətası baş verdi. Lütfən, daha sonra yenidən cəhd edin."
        };

        if (exception is ValidationException validationException)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            var validationErrors = validationException.Errors
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
            errorDetails.StatusCode = statusCode;
            errorDetails.Message = "Daxil edilən məlumatlarda səhvlər var.";
            errorDetails.Details = string.Join(" | ", validationErrors);
        }

        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(errorDetails.ToString());
    }
}

public static class CustomExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}
