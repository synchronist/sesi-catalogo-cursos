using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Sesi.Catalogo.Api.Services;

namespace Sesi.Catalogo.Tests;

public sealed class DeploymentTests
{
    [Theory]
    [InlineData("10000", "http://0.0.0.0:10000")]
    [InlineData(null, null)]
    public void PortaRender(string? port, string? expected) =>
        Assert.Equal(expected, DeploymentConfiguration.PortUrl(port));

    [Theory]
    [InlineData("")]
    [InlineData("80")]
    [InlineData("65536")]
    [InlineData("invalida")]
    public void RejeitaPortaInvalida(string port) =>
        Assert.Throws<InvalidOperationException>(() => DeploymentConfiguration.PortUrl(port));

    [Theory]
    [InlineData("Disable")]
    [InlineData("Prefer")]
    [InlineData("Require")]
    [InlineData("VerifyCA")]
    public void NeonExigeCertificadoEHostname(string mode) =>
        Assert.Throws<InvalidOperationException>(() => DeploymentConfiguration.ValidateConnection(
            $"Host=ep-exemplo-pooler.us-east-2.aws.neon.tech;SSL Mode={mode}"));

    [Fact]
    public void AceitaNeonVerifyFullELocalSemTls()
    {
        DeploymentConfiguration.ValidateConnection("Host=ep-exemplo.us-east-2.aws.neon.tech;SSL Mode=VerifyFull");
        DeploymentConfiguration.ValidateConnection("Host=127.0.0.1;SSL Mode=Disable");
    }

    [Fact]
    public async Task SpaNaoInterceptaApiHealthOuArquivoInexistente()
    {
        var webroot = Path.Combine(Path.GetTempPath(), "sstp-web-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(webroot);
        await File.WriteAllTextAsync(Path.Combine(webroot, "index.html"), "<!doctype html><title>SPA de teste</title>");
        await File.WriteAllTextAsync(Path.Combine(webroot, "probe.css"), "body{color:black}");
        try
        {
            await using var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(b =>
            {
                b.UseEnvironment("Production");
                b.UseWebRoot(webroot);
                b.UseSetting("ConnectionStrings:Catalogo", "Host=127.0.0.1;Port=1;Database=unused;Username=unused;Timeout=1");
            });
            using var client = factory.CreateClient(new() { AllowAutoRedirect = false });
            foreach (var route in new[] { "/", "/cursos", "/cursos/1" })
            {
                using var response = await client.GetAsync(route);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Contains("SPA de teste", await response.Content.ReadAsStringAsync());
            }
            using var css = await client.GetAsync("/probe.css");
            Assert.Equal("text/css", css.Content.Headers.ContentType?.MediaType);
            foreach (var route in new[] { "/api/inexistente", "/health/inexistente", "/assets/inexistente.js" })
            {
                using var response = await client.GetAsync(route);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                Assert.DoesNotContain("SPA de teste", await response.Content.ReadAsStringAsync());
            }
            using var invalid = await client.GetAsync("/api/cursos?status=invalido");
            Assert.Equal(HttpStatusCode.BadRequest, invalid.StatusCode);
            using var health = await client.GetAsync("/health");
            Assert.Equal(HttpStatusCode.ServiceUnavailable, health.StatusCode);
        }
        finally { Directory.Delete(webroot, recursive: true); }
    }
}
