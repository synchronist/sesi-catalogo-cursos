using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Sesi.Catalogo.Tests;

public sealed class HttpTests
{
    [Theory]
    [InlineData("/api/cursos?status=desconhecido")]
    [InlineData("/api/cursos?categoria=a&categoria=b")]
    [InlineData("/api/cursos/abc")]
    [InlineData("/api/cursos/0")]
    public async Task ParametrosInvalidosRetornamProblemDetailsSemConsultarBanco(string path)
    {
        await using var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(b =>
            b.UseSetting("ConnectionStrings:Catalogo", "Host=127.0.0.1;Port=1;Database=unused;Username=unused;Timeout=1"));
        using var client = factory.CreateClient();
        using var response = await client.GetAsync(path);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(400, body.RootElement.GetProperty("status").GetInt32());
        Assert.True(body.RootElement.TryGetProperty("traceId", out _));
    }

    [Fact]
    public async Task ConsultaRetorna503SemExporDetalhesQuandoBancoNaoResponde()
    {
        await using var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(b =>
            b.UseSetting("ConnectionStrings:Catalogo", "Host=127.0.0.1;Port=1;Database=unused;Username=unused;Timeout=1"));
        using var client = factory.CreateClient();
        using var response = await client.GetAsync("/api/cursos");
        Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(503, body.RootElement.GetProperty("status").GetInt32());
        Assert.False(body.RootElement.TryGetProperty("exception", out _));
        Assert.DoesNotContain("127.0.0.1", body.RootElement.GetProperty("detail").GetString());
    }

    [Fact]
    public async Task HealthRetorna503QuandoBancoNaoResponde()
    {
        await using var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(b =>
            b.UseSetting("ConnectionStrings:Catalogo", "Host=127.0.0.1;Port=1;Database=unused;Username=unused;Timeout=1"));
        using var client = factory.CreateClient();
        using var response = await client.GetAsync("/health");
        Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
    }
}
