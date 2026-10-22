using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Sesi.Catalogo.Api.Services;

public sealed class ApiExceptionHandler(IProblemDetailsService problems, ILogger<ApiExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct)
    {
        if (exception is OperationCanceledException && context.RequestAborted.IsCancellationRequested) return false;
        var unavailable = IsDatabaseUnavailable(exception);
        var status = unavailable ? 503 : 500;
        logger.LogError(exception, "Falha na API. TraceId: {TraceId}", context.TraceIdentifier);
        context.Response.StatusCode = status;
        return await problems.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = context,
            ProblemDetails = new ProblemDetails
            {
                Status = status, Title = unavailable ? "Serviço temporariamente indisponível" : "Erro interno",
                Detail = unavailable ? "Não foi possível consultar o banco. Tente novamente mais tarde." : "Não foi possível concluir a solicitação."
            }
        });
    }

    private static bool IsDatabaseUnavailable(Exception exception)
    {
        // EF Core pode encapsular a falha transitória do provedor.
        for (Exception? cause = exception; cause is not null; cause = cause.InnerException)
            if (cause is NpgsqlException { IsTransient: true } or TimeoutException) return true;
        return false;
    }
}
