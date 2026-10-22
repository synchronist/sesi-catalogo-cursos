namespace Sesi.Catalogo.Api.Entities;

public sealed class Modulo
{
    public int Id { get; set; }
    public int CursoId { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public int Ordem { get; set; }
    public List<Aula> Aulas { get; set; } = [];
}
