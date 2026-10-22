using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sesi.Catalogo.Api.Persistence;
using Sesi.Catalogo.Api.Services;

namespace Sesi.Catalogo.Tests;

public sealed class ConsultaTests
{
    [Theory]
    [InlineData("?status=invalido", "status")]
    [InlineData("?categoria=a&categoria=b", "categoria")]
    [InlineData("?modalidade=presencial", "modalidade")]
    [InlineData("?page=1", "page")]
    public void RejeitaParametrosInvalidos(string query, string chave)
    {
        var request = new DefaultHttpContext().Request;
        request.QueryString = new QueryString(query);
        Assert.Contains(chave, ConsultaCursos.Validar(request.Query).Keys);
    }

    [Fact]
    public void RejeitaBuscaLongaEAceitaBuscaVazia()
    {
        var request = new DefaultHttpContext().Request;
        request.QueryString = new QueryString("?busca=" + new string('a', 151));
        Assert.Contains("busca", ConsultaCursos.Validar(request.Query).Keys);
        request.QueryString = new QueryString("?busca=%20%20&status=disponivel");
        Assert.Empty(ConsultaCursos.Validar(request.Query));
    }

    [Fact]
    public void PesquisaEFiltrosSaoTraduzidosParaPostgresqlComOrdenacao()
    {
        using var db = new CatalogoDbContext(new DbContextOptionsBuilder<CatalogoDbContext>()
            .UseNpgsql("Host=localhost;Database=translation_only;Username=unused").Options);
        var sql = new ConsultaCursos("50%_", "matematica", "online", "disponivel").Aplicar(db.Cursos).ToQueryString();
        Assert.Contains("ILIKE", sql);
        Assert.Contains("NomeCompleto", sql);
        Assert.Contains("NomeBreve", sql);
        Assert.Contains(" AND ", sql);
        Assert.Contains("CategoriaId", sql);
        Assert.Contains("Modalidade", sql);
        Assert.Contains("Status", sql);
        Assert.Contains("ORDER BY", sql);
        Assert.Contains(@"50\%\_", sql);
    }
}
