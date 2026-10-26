using Npgsql;

namespace Sesi.Catalogo.Tests;

public sealed class NeonReadOnlyFactAttribute : FactAttribute
{
    public NeonReadOnlyFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CATALOGO_NEON_READONLY_CONNECTION")))
            Skip = "Configure explicitamente CATALOGO_NEON_READONLY_CONNECTION; este teste só consulta dados.";
    }
}

public sealed class NeonReadOnlyTests
{
    [NeonReadOnlyFact]
    [Trait("Category", "NeonReadOnly")]
    public async Task ConexaoNeonVerificaTlsMigrationESeedSemModificarDados()
    {
        var value = Environment.GetEnvironmentVariable("CATALOGO_NEON_READONLY_CONNECTION")!;
        var settings = new NpgsqlConnectionStringBuilder(value);
        Assert.Equal(SslMode.VerifyFull, settings.SslMode);
        Assert.EndsWith(".neon.tech", settings.Host);
        Assert.DoesNotContain("-pooler.", settings.Host);
        Assert.Equal("neondb", settings.Database);
        await using var connection = new NpgsqlConnection(value);
        await connection.OpenAsync();
        // VerifyFull exige TLS com validação de certificado/hostname no OpenAsync.
        // Sem callbacks que substituam a validação padrão do Npgsql.
        await using var transaction = await connection.BeginTransactionAsync();
        await using (var readOnly = new NpgsqlCommand("SET TRANSACTION READ ONLY", connection, transaction))
            await readOnly.ExecuteNonQueryAsync();
        foreach (var (table, expected) in new[] { ("categorias", 3L), ("cursos", 6L), ("modulos", 20L), ("aulas", 50L) })
        {
            // Identificadores fixos do teste, sem entrada externa.
            await using var count = new NpgsqlCommand($"SELECT count(*) FROM public.{table}", connection, transaction);
            Assert.Equal(expected, (long)(await count.ExecuteScalarAsync())!);
        }
        await using var history = new NpgsqlCommand("SELECT count(*) FROM public.\"__EFMigrationsHistory\" WHERE \"MigrationId\" IN ('20261021233255_InitialCatalogo','20261024192422_AlignFeaturedCoursesWithFigma','20261024192600_AlignOfficialCourseImages','20261024225428_AlignMetalurgiaSyllabusWithFigma')", connection, transaction);
        Assert.Equal(4L, (long)(await history.ExecuteScalarAsync())!);
        await transaction.RollbackAsync();
    }
}
