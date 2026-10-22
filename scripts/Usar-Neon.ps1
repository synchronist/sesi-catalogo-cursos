# Execute por dot-source: . ./scripts/Usar-Neon.ps1
# Lê somente a conexão DIRETA gerada pela CLI, sem imprimi-la ou gravá-la.
Remove-Item Env:ConnectionStrings__Catalogo -ErrorAction SilentlyContinue
$workspace = Split-Path $PSScriptRoot -Parent
$contextPath = Join-Path $workspace '.neon'
$envPath = Join-Path $workspace '.env.local'
if (!(Test-Path -LiteralPath $contextPath) -or !(Test-Path -LiteralPath $envPath)) {
    throw 'Contexto/ambiente Neon ausente. Confirme login e vínculo do projeto antes de continuar.'
}
$context = Get-Content -LiteralPath $contextPath -Raw | ConvertFrom-Json
if ($context.orgId -ne 'org-noisy-tree-27200340' -or
    $context.projectId -ne 'empty-tooth-58563543' -or
    $context.branch -notin @('production', 'br-lively-sun-b6s1tndm')) {
    throw 'Contexto Neon diferente do projeto/branch autorizado.'
}
$line = Get-Content -LiteralPath $envPath | Where-Object { $_ -match '^DATABASE_URL_UNPOOLED=' }
if (@($line).Count -ne 1) { throw 'Conexão direta Neon ausente ou ambígua.' }
$value = $line.Substring($line.IndexOf('=') + 1).Trim()
if (($value.StartsWith("'") -and $value.EndsWith("'")) -or
    ($value.StartsWith('"') -and $value.EndsWith('"'))) {
    $value = $value.Substring(1, $value.Length - 2)
}
try {
    $uri = [Uri]::new($value)
    if ($uri.Scheme -notin @('postgres', 'postgresql') -or
        !$uri.DnsSafeHost.EndsWith('.neon.tech') -or $uri.DnsSafeHost.Contains('-pooler.')) {
        throw 'Destino inválido.'
    }
    $database = [Uri]::UnescapeDataString($uri.AbsolutePath.TrimStart('/'))
    $credentials = $uri.UserInfo.Split(':', 2)
    if ($database -ne 'neondb' -or $credentials.Count -ne 2) { throw 'Banco/credencial inválido.' }
    $connection = [System.Data.Common.DbConnectionStringBuilder]::new()
    $connection['Host'] = $uri.DnsSafeHost
    $connection['Port'] = if ($uri.Port -gt 0) { $uri.Port } else { 5432 }
    $connection['Database'] = $database
    $connection['Username'] = [Uri]::UnescapeDataString($credentials[0])
    $connection['Password'] = [Uri]::UnescapeDataString($credentials[1])
    $connection['SSL Mode'] = 'VerifyFull'
    $connection['Timeout'] = 15
    $connection['Command Timeout'] = 30
    $connection['Maximum Pool Size'] = 10
    $connection['Include Error Detail'] = $false
    $env:ConnectionStrings__Catalogo = $connection.ConnectionString
}
catch { throw 'Não foi possível preparar a conexão Neon segura; detalhes e valores foram omitidos.' }
finally {
    Remove-Variable value, uri, credentials, connection, line -ErrorAction SilentlyContinue
}
