namespace Sesi.Catalogo.Api.Entities;

public sealed class Categoria
{
    public int Id { get; set; }
    public required string Codigo { get; set; }
    public required string Nome { get; set; }
}
