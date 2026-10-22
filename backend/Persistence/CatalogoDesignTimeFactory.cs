using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sesi.Catalogo.Api.Services;

namespace Sesi.Catalogo.Api.Persistence;

public sealed class CatalogoDesignTimeFactory : IDesignTimeDbContextFactory<CatalogoDbContext>
{
    public CatalogoDbContext CreateDbContext(string[] args)
    {
        // Gerar migrations não requer conexão real; aplicar exige configuração explícita.
        var connection = Environment.GetEnvironmentVariable("ConnectionStrings__Catalogo")
            ?? "Host=localhost;Database=catalogo;Username=design_only";
        DeploymentConfiguration.ValidateConnection(connection);
        return new(new DbContextOptionsBuilder<CatalogoDbContext>().UseNpgsql(connection).Options);
    }
}
