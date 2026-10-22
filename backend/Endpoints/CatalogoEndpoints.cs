using Microsoft.EntityFrameworkCore;
using Sesi.Catalogo.Api.Contracts;
using Sesi.Catalogo.Api.Persistence;
using Sesi.Catalogo.Api.Services;

namespace Sesi.Catalogo.Api.Endpoints;

public static class CatalogoEndpoints
{
    public static void MapCatalogo(this WebApplication app)
    {
        app.MapGet("/api/cursos", async (HttpRequest request, CatalogoDbContext db, CatalogoService service, CancellationToken ct) =>
        {
            var erros = ConsultaCursos.Validar(request.Query);
            if (erros.Count > 0) return Results.ValidationProblem(erros);
            string? Valor(string nome) => string.IsNullOrWhiteSpace(request.Query[nome]) ? null : request.Query[nome].ToString().Trim();
            var categoria = Valor("categoria");
            if (categoria is not null && !await db.Categorias.AsNoTracking().AnyAsync(x => x.Codigo == categoria, ct))
                return Results.ValidationProblem(new Dictionary<string, string[]> { ["categoria"] = ["Código desconhecido."] });
            return Results.Ok(await service.ListarAsync(new(Valor("busca"), categoria, Valor("modalidade"), Valor("status")), ct));
        });

        app.MapGet("/api/cursos/{id}", async (string id, CatalogoService service, CancellationToken ct) =>
        {
            if (!int.TryParse(id, out var numero) || numero < 1)
                return Results.ValidationProblem(new Dictionary<string, string[]> { ["id"] = ["Informe um inteiro positivo."] });
            var curso = await service.DetalharAsync(numero, ct);
            return curso is null ? Results.Problem(statusCode: 404, title: "Curso não encontrado") : Results.Ok(curso);
        });

        app.MapGet("/api/catalogo/filtros", async (CatalogoDbContext db, CancellationToken ct) =>
            Results.Ok(new FiltrosDto(await db.Categorias.AsNoTracking().OrderBy(x => x.Nome).ThenBy(x => x.Id)
                .Select(x => new OpcaoDto(x.Codigo, x.Nome)).ToListAsync(ct), ValoresCatalogo.Modalidades, ValoresCatalogo.Status)));
    }
}
