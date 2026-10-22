using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Sesi.Catalogo.Api.Endpoints;
using Sesi.Catalogo.Api.Persistence;
using Sesi.Catalogo.Api.Services;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("Catalogo");
// A aplicação compila sem banco; conexão só é necessária para executar.
if (string.IsNullOrWhiteSpace(connection))
    throw new InvalidOperationException("Configure ConnectionStrings__Catalogo com a conexão PostgreSQL.");
DeploymentConfiguration.ValidateConnection(connection);
var portUrl = DeploymentConfiguration.PortUrl(builder.Configuration["PORT"]);
if (portUrl is not null) builder.WebHost.UseUrls(portUrl);

builder.Services.AddDbContext<CatalogoDbContext>(options => options.UseNpgsql(connection));
builder.Services.AddScoped<CatalogoService>();
builder.Services.AddProblemDetails(options => options.CustomizeProblemDetails = context =>
{
    context.ProblemDetails.Instance ??= context.HttpContext.Request.Path;
    context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
});
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddHealthChecks().AddCheck<DatabaseHealthCheck>("postgresql", timeout: TimeSpan.FromSeconds(5));

var app = builder.Build();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapCatalogo();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        if (context.Response.StatusCode == 503)
            await Results.Problem(statusCode: 503, title: "Banco indisponível").ExecuteAsync(context);
        else await context.Response.WriteAsJsonAsync(new { status = report.Status.ToString() });
    }
});
// Impede que rotas desconhecidas da API/health recebam o HTML da SPA.
app.Map("/api/{**path}", () => Results.Problem(statusCode: 404, title: "Recurso não encontrado"));
app.Map("/health/{**path}", () => Results.Problem(statusCode: 404, title: "Recurso não encontrado"));
app.MapFallbackToFile("index.html");
// Migrations nunca são aplicadas automaticamente na inicialização.
app.Run();

public partial class Program;
