using Sesi.Catalogo.Api.Contracts;

namespace Sesi.Catalogo.Api.Services;

public static class ValoresCatalogo
{
    public static readonly OpcaoDto[] Modalidades = [new("online", "Online"), new("semipresencial", "Semipresencial")];
    public static readonly OpcaoDto[] Status = [new("disponivel", "Disponível"), new("em-breve", "Em breve"), new("encerrado", "Encerrado")];
}
