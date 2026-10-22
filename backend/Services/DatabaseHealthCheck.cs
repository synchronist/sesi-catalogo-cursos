using Microsoft.Extensions.Diagnostics.HealthChecks;
using Sesi.Catalogo.Api.Persistence;

namespace Sesi.Catalogo.Api.Services;

public sealed class DatabaseHealthCheck(CatalogoDbContext db) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            return await db.Database.CanConnectAsync(cancellationToken)
                ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy("Banco indisponível.");
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            return HealthCheckResult.Unhealthy("Banco indisponível.", ex);
        }
    }
}
