using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Sesi.Catalogo.Api.Contracts;
using Sesi.Catalogo.Api.Persistence;

namespace Sesi.Catalogo.Tests;

public sealed class PostgresFactAttribute : FactAttribute
{
    public PostgresFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CATALOGO_TEST_CONNECTION")))
            Skip = "Configure CATALOGO_TEST_CONNECTION para um banco PostgreSQL exclusivo de testes.";
    }
}

public sealed class PostgresTests
{
    [PostgresFact]
    [Trait("Category", "PostgreSQL")]
    public async Task ApiPesquisaFiltrosDetalhesESeedEmPostgresqlReal()
    {
        // Isolamento em schema exclusivo; nunca apaga o banco informado.
        var schema = "teste_" + Guid.NewGuid().ToString("N");
        var connection = Environment.GetEnvironmentVariable("CATALOGO_TEST_CONNECTION")!;
        await using var admin = new NpgsqlConnection(connection);
        await admin.OpenAsync();
        await using (var create = new NpgsqlCommand($"CREATE SCHEMA {schema}", admin)) await create.ExecuteNonQueryAsync();
        try
        {
            var scoped = new NpgsqlConnectionStringBuilder(connection) { SearchPath = schema }.ConnectionString;
            await using var db = new CatalogoDbContext(new DbContextOptionsBuilder<CatalogoDbContext>()
                .UseNpgsql(scoped, options => options.MigrationsHistoryTable("__EFMigrationsHistory", schema)).Options);
            await db.Database.MigrateAsync();
            await db.Database.MigrateAsync(); // Repetição não duplica o seed.
            Assert.Equal(6, await db.Cursos.CountAsync());
            Assert.Equal(3, await db.Categorias.CountAsync());
            Assert.Equal(12, await db.Modulos.CountAsync());
            Assert.Equal(24, await db.Aulas.CountAsync());

            await using var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(b => b.UseSetting("ConnectionStrings:Catalogo", scoped));
            using var client = factory.CreateClient();
            var lista = await client.GetFromJsonAsync<ListaCursosDto>("/api/cursos");
            Assert.Equal(6, lista!.Total);
            foreach (var nome in new[] { "METALURGIA", "Medidas na Metalurgia" })
            {
                var busca = await client.GetFromJsonAsync<ListaCursosDto>("/api/cursos?busca=" + Uri.EscapeDataString(nome));
                Assert.Equal(1, busca!.Total);
                Assert.Equal(1, busca.Items.Single().Id);
            }
            var filtrado = await client.GetFromJsonAsync<ListaCursosDto>("/api/cursos?busca=Medidas&categoria=matematica&modalidade=online&status=disponivel");
            Assert.Equal(1, filtrado!.Total);
            var vazio = await client.GetFromJsonAsync<ListaCursosDto>("/api/cursos?categoria=matematica&status=encerrado");
            Assert.Equal(0, vazio!.Total);
            Assert.Empty(vazio.Items);
            var literal = await client.GetFromJsonAsync<ListaCursosDto>("/api/cursos?busca=%25");
            Assert.Equal(0, literal!.Total);
            var detalhe = await client.GetFromJsonAsync<CursoDetalheDto>("/api/cursos/1");
            Assert.Equal(1, detalhe!.Curso.Id);
            Assert.Equal(2, detalhe.Modulos.Count);
            Assert.All(detalhe.Modulos, m => Assert.Equal(2, m.Aulas.Count));
            Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync("/api/cursos/99999")).StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, (await client.GetAsync("/api/cursos?categoria=inexistente")).StatusCode);
            var filtros = await client.GetFromJsonAsync<FiltrosDto>("/api/catalogo/filtros");
            Assert.Equal(3, filtros!.Categorias.Count);
            Assert.Equal(2, filtros.Modalidades.Count);
            Assert.Equal(3, filtros.Status.Count);
            Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/health")).StatusCode);
        }
        finally
        {
            await using var drop = new NpgsqlCommand($"DROP SCHEMA {schema} CASCADE", admin);
            await drop.ExecuteNonQueryAsync();
        }
    }
}
