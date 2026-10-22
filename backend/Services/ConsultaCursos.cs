using Microsoft.EntityFrameworkCore;
using Sesi.Catalogo.Api.Contracts;
using Sesi.Catalogo.Api.Entities;
using Sesi.Catalogo.Api.Persistence;

namespace Sesi.Catalogo.Api.Services;

public sealed record ConsultaCursos(string? Busca = null, string? Categoria = null, string? Modalidade = null, string? Status = null)
{
    public static Dictionary<string, string[]> Validar(IQueryCollection query)
    {
        var erros = new Dictionary<string, string[]>();
        string[] nomes = ["busca", "categoria", "modalidade", "status"];
        foreach (var (chave, valores) in query)
        {
            if (!nomes.Contains(chave, StringComparer.Ordinal)) erros[chave] = ["Parâmetro desconhecido."];
            else if (valores.Count != 1) erros[chave] = ["Informe apenas um valor."];
            else if (valores[0]?.Trim().Length > (chave == "busca" ? 150 : 60)) erros[chave] = ["Valor excede o tamanho permitido."];
        }
        foreach (var (chave, opcoes) in new[] { ("modalidade", ValoresCatalogo.Modalidades), ("status", ValoresCatalogo.Status) })
        {
            var valor = query[chave].ToString().Trim();
            if (valor.Length > 0 && !opcoes.Any(x => x.Codigo == valor)) erros[chave] = ["Código desconhecido."];
        }
        return erros;
    }

    public IQueryable<Curso> Aplicar(IQueryable<Curso> cursos)
    {
        if (!string.IsNullOrWhiteSpace(Busca))
        {
            // Busca literal: %, _ e a barra não são curingas fornecidos pelo usuário.
            var literal = Busca.Trim().Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");
            var padrao = $"%{literal}%";
            cursos = cursos.Where(x => EF.Functions.ILike(x.NomeCompleto, padrao, "\\") || EF.Functions.ILike(x.NomeBreve, padrao, "\\"));
        }
        if (!string.IsNullOrWhiteSpace(Categoria)) cursos = cursos.Where(x => x.Categoria.Codigo == Categoria.Trim());
        if (!string.IsNullOrWhiteSpace(Modalidade)) cursos = cursos.Where(x => x.Modalidade == Modalidade.Trim());
        if (!string.IsNullOrWhiteSpace(Status)) cursos = cursos.Where(x => x.Status == Status.Trim());
        return cursos.OrderBy(x => x.NomeCompleto).ThenBy(x => x.Id);
    }
}

public sealed class CatalogoService(CatalogoDbContext db)
{
    public async Task<ListaCursosDto> ListarAsync(ConsultaCursos consulta, CancellationToken ct)
    {
        var cursos = await consulta.Aplicar(db.Cursos.AsNoTracking()).Include(x => x.Categoria).ToListAsync(ct);
        var items = cursos.Select(Resumo).ToArray();
        return new(items, items.Length);
    }

    public async Task<CursoDetalheDto?> DetalharAsync(int id, CancellationToken ct)
    {
        var curso = await db.Cursos.AsNoTracking().Include(x => x.Categoria)
            .Include(x => x.Modulos).ThenInclude(x => x.Aulas).AsSplitQuery().SingleOrDefaultAsync(x => x.Id == id, ct);
        return curso is null ? null : new(Resumo(curso), curso.Descricao, curso.PublicoAlvo,
            curso.Modulos.OrderBy(x => x.Ordem).ThenBy(x => x.Id).Select(m => new ModuloDto(m.Id, m.Titulo, m.Descricao, m.Ordem,
                m.Aulas.OrderBy(x => x.Ordem).ThenBy(x => x.Id).Select(a => new AulaDto(a.Id, a.Titulo, a.Descricao, a.Ordem)).ToArray())).ToArray());
    }

    private static CursoResumoDto Resumo(Curso c) => new(c.Id, c.NomeCompleto, c.NomeBreve, c.Resumo,
        new(c.Categoria.Codigo, c.Categoria.Nome), ValoresCatalogo.Modalidades.Single(x => x.Codigo == c.Modalidade),
        ValoresCatalogo.Status.Single(x => x.Codigo == c.Status), c.CargaHoraria, c.Imagem, c.DataDisponibilizacao);
}
