namespace Sesi.Catalogo.Api.Entities;

public sealed class Aula
{
    public int Id { get; set; }
    public int ModuloId { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public int Ordem { get; set; }
}
