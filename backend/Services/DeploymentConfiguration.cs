using Npgsql;

namespace Sesi.Catalogo.Api.Services;

public static class DeploymentConfiguration
{
    public static string? PortUrl(string? port)
    {
        if (port is null) return null;
        if (!int.TryParse(port, out var value) || value is < 1024 or > 65535)
            throw new InvalidOperationException("PORT deve ser uma porta entre 1024 e 65535 para execução sem privilégios.");
        return $"http://0.0.0.0:{value}";
    }

    public static void ValidateConnection(string connection)
    {
        NpgsqlConnectionStringBuilder settings;
        try { settings = new(connection); }
        catch (ArgumentException)
        {
            // Não inclui o valor recebido, nem a exceção que pode contê-lo.
            throw new InvalidOperationException("ConnectionStrings__Catalogo não é uma conexão Npgsql válida.");
        }
        var neon = settings.Host?.Split(',').Any(host =>
            host.Trim().TrimEnd('.').EndsWith(".neon.tech", StringComparison.OrdinalIgnoreCase)) == true;
        if (neon && settings.SslMode != SslMode.VerifyFull)
            throw new InvalidOperationException("Conexões Neon exigem SSL Mode=VerifyFull.");
    }
}
