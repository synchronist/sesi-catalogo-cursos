param([string]$OutputDirectory = 'entregaveis')
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$root = Split-Path $PSScriptRoot -Parent
Set-Location $root
$output = [IO.Path]::GetFullPath((Join-Path $root $OutputDirectory))
if (!$output.StartsWith($root + [IO.Path]::DirectorySeparatorChar, [StringComparison]::OrdinalIgnoreCase)) {
    throw 'A saída deve permanecer dentro do workspace.'
}
if (Test-Path -LiteralPath $output) { throw 'Saída já existe. Escolha outra pasta para preservar artefatos anteriores.' }
if (@(Get-ChildItem frontend -Force -File | Where-Object { $_.Name -like '.env*' -and $_.Name -ne '.env.example' }).Count) {
    throw 'Revise os arquivos .env do frontend antes de empacotar.'
}
$package = Join-Path $output 'sesi-catalogo-publicacao-linux-x64'
$app = Join-Path $package 'app'
New-Item -ItemType Directory -Path $app -Force | Out-Null
$previousApiBase = $env:VITE_API_BASE_URL
try {
    $env:VITE_API_BASE_URL = ''
    npm --prefix frontend run build
    if ($LASTEXITCODE -ne 0) { throw 'Build frontend falhou.' }
} finally { $env:VITE_API_BASE_URL = $previousApiBase }
dotnet publish backend/Sesi.Catalogo.Api.csproj -c Release -r linux-x64 --self-contained false -p:UseAppHost=false -p:ProductionImage=true -p:DebugType=None -p:DebugSymbols=false -o $app
if ($LASTEXITCODE -ne 0) { throw 'Publish backend falhou.' }
Copy-Item frontend/dist -Destination (Join-Path $app 'wwwroot') -Recurse
New-Item -ItemType Directory -Path (Join-Path $package 'docs'), (Join-Path $package 'migrations') | Out-Null
Copy-Item docs/EXECUCAO_PACOTE.md (Join-Path $package 'EXECUCAO.md')
Copy-Item docs/LIMITACOES_ENTREGA.md (Join-Path $package 'docs/LIMITACOES.md')
Copy-Item README.md, USO_DE_IA.md (Join-Path $package 'docs')
Copy-Item backend/Persistence/Migrations/*.cs (Join-Path $package 'migrations')
$sha = (git rev-parse HEAD).Trim()
if ($LASTEXITCODE -ne 0) { throw 'Não foi possível identificar a revisão fonte.' }
@{
    sourceCommit = $sha
    note = 'Artefatos gerados localmente; documentação pode incluir alterações ainda não commitadas. Não atesta SHA do backend Render.'
    target = 'linux-x64; glibc; framework-dependent; ASP.NET Core Runtime 10'
    generatedUtc = [DateTime]::UtcNow.ToString('o')
    dotnetSdk = (dotnet --version)
    node = (node --version)
    npm = (npm --version)
    productionImageProperty = $true
} | ConvertTo-Json | Set-Content (Join-Path $package 'PUBLICACAO.json') -Encoding utf8NoBOM
if (!(Test-Path (Join-Path $app 'wwwroot/index.html'))) { throw 'React não foi copiado.' }
if (Test-Path (Join-Path $app 'Microsoft.EntityFrameworkCore.Design.dll')) { throw 'EF Design indevido.' }
$forbidden = @(Get-ChildItem $package -Recurse -Force -File | Where-Object {
    $_.Name -match '^\.env|^\.neon|appsettings\.Development|\.(pdb|log|key|pem|pfx|db|sqlite|tmp)$' -or
    $_.FullName -match '[\\/](node_modules|obj|bin|\.git)[\\/]'
})
if ($forbidden.Count) { throw 'Arquivo de desenvolvimento ou privado no pacote.' }
$files = @(Get-ChildItem $package -Recurse -File | Sort-Object FullName)
$manifest = foreach ($file in $files) {
    [pscustomobject]@{
        path = [IO.Path]::GetRelativePath($package, $file.FullName).Replace('\','/')
        bytes = $file.Length
        sha256 = (Get-FileHash -LiteralPath $file.FullName -Algorithm SHA256).Hash.ToLowerInvariant()
    }
}
$manifest | Export-Csv (Join-Path $package 'MANIFESTO.csv') -NoTypeInformation -Encoding utf8NoBOM
$checksumFiles = @(Get-ChildItem $package -Recurse -File | Sort-Object FullName)
$lines = foreach ($file in $checksumFiles) {
    $relative = [IO.Path]::GetRelativePath($package, $file.FullName).Replace('\','/')
    '{0}  {1}' -f (Get-FileHash -LiteralPath $file.FullName -Algorithm SHA256).Hash.ToLowerInvariant(), $relative
}
$lines | Set-Content (Join-Path $package 'SHA256SUMS') -Encoding utf8NoBOM
# O ZIP só é finalizado depois da auditoria de segredos separada.
Write-Output "Publish preparado: $package"
Write-Output 'Audite o conteúdo; depois compacte e valide o ZIP, sem incluir o próprio ZIP no pacote.'
