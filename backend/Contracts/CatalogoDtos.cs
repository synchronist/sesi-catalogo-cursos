namespace Sesi.Catalogo.Api.Contracts;

public sealed record OpcaoDto(string Codigo, string Nome);
public sealed record FiltrosDto(IReadOnlyList<OpcaoDto> Categorias, IReadOnlyList<OpcaoDto> Modalidades, IReadOnlyList<OpcaoDto> Status);
public sealed record CursoResumoDto(int Id, string NomeCompleto, string NomeBreve, string Resumo,
    OpcaoDto Categoria, OpcaoDto Modalidade, OpcaoDto Status, int CargaHoraria, string Imagem, DateOnly? DataDisponibilizacao);
public sealed record AulaDto(int Id, string Titulo, string Descricao, int Ordem);
public sealed record ModuloDto(int Id, string Titulo, string Descricao, int Ordem, IReadOnlyList<AulaDto> Aulas);
public sealed record CursoDetalheDto(CursoResumoDto Curso, string Descricao, string PublicoAlvo, IReadOnlyList<ModuloDto> Modulos);
public sealed record ListaCursosDto(IReadOnlyList<CursoResumoDto> Items, int Total);
