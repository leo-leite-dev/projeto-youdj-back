using Npgsql;
using Microsoft.EntityFrameworkCore;
using YouDj.Application.Common.Errors;
using YouDj.Application.Common.Results;

namespace YouDj.Api.Middlewares;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        IDbErrorTranslator translator)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is PostgresException pg
                  && pg.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            var info = translator.TranslateUniqueViolation(pg.ConstraintName);

            var result = Result.Conflict(
                info.Message,
                code: info.Code
            );

            if (!string.IsNullOrWhiteSpace(info.Field))
                result = result.WithMeta("field", info.Field);
            
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}