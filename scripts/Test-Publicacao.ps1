param([string]$BaseUrl = 'http://127.0.0.1:5180')
$ErrorActionPreference = 'Stop'
$base = $BaseUrl.TrimEnd('/')
$checks = [System.Collections.Generic.List[object]]::new()
function Check([string]$path, [int]$status, [string]$type) {
    $response = Invoke-WebRequest -Uri "$base$path" -SkipHttpErrorCheck -MaximumRedirection 0
    if ([int]$response.StatusCode -ne $status -or $response.Headers['Content-Type'] -notlike "$type*") {
        throw "Falha em $path : status/content-type inesperado."
    }
    $checks.Add([pscustomobject]@{path=$path; status=[int]$response.StatusCode; contentType=[string]$response.Headers['Content-Type']})
    return $response
}
$index = Check '/' 200 'text/html'
foreach ($path in @('/cursos', '/cursos/1')) {
    $page = Check $path 200 'text/html'
    if ($page.Content -ne $index.Content) { throw "Fallback diferente do index em $path" }
}
$health = (Check '/health' 200 'application/json').Content | ConvertFrom-Json
if ($health.status -ne 'Healthy') { throw 'Banco não está saudável.' }
$courses = (Check '/api/cursos' 200 'application/json').Content | ConvertFrom-Json
if ($courses.total -ne 6) { throw 'Quantidade diferente do seed esperado (6).' }
$filtered = (Check '/api/cursos?busca=Medidas&categoria=matematica&modalidade=online&status=disponivel' 200 'application/json').Content | ConvertFrom-Json
if ($filtered.total -ne 1) { throw 'Busca combinada inconsistente.' }
$detail = (Check '/api/cursos/1' 200 'application/json').Content | ConvertFrom-Json
if ($detail.modulos.Count -ne 2 -or @($detail.modulos.aulas).Count -ne 4) { throw 'Módulos/aulas inconsistentes.' }
$null = Check '/api/cursos?status=invalido' 400 'application/problem+json'
$null = Check '/api/cursos/999999' 404 'application/problem+json'
$null = Check '/api/inexistente' 404 'application/problem+json'
$null = Check '/health/inexistente' 404 'application/problem+json'
$null = Check '/assets/inexistente.js' 404 'application/problem+json'
$null = Check '/assets/oficiais/hero-image.png' 200 'image/png'
$scriptPath = [regex]::Match($index.Content, 'src="([^"]+\.js)"').Groups[1].Value
$cssPath = [regex]::Match($index.Content, 'href="([^"]+\.css)"').Groups[1].Value
if (!$scriptPath -or !$cssPath) { throw 'Bundles não encontrados no HTML.' }
$js = Check $scriptPath 200 'text/javascript'
$null = Check $cssPath 200 'text/css'
if ($js.Content -match 'https?://(localhost|127\.0\.0\.1)([:/]|\b)') { throw 'Origem localhost no bundle.' }
$checks | ConvertTo-Json
