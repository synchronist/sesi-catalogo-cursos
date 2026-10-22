namespace Sesi.Catalogo.Api.Entities;

public sealed class Curso
{
    public int Id { get; set; }
    public required string NomeCompleto { get; set; }
    public required string NomeBreve { get; set; }
    public required string Resumo { get; set; }
    public required string Descricao { get; set; }
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;
    public required string Modalidade { get; set; }
    public required string Status { get; set; }
    public int CargaHoraria { get; set; }
    public required string PublicoAlvo { get; set; }
    public required string Imagem { get; set; }
    public DateOnly? DataDisponibilizacao { get; set; }
    public List<Modulo> Modulos { get; set; } = [];
}
