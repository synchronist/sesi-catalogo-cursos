# Preparação de deploy — Fase 3.1

Validação local em 21–22/10/2026, sem recursos externos ou publicação.

## Resultado confirmado

- Docker Desktop Engine 29.8.2 operacional. Build multi-stage Linux executou npm ci/build, restore e publish .NET 10.
- Imagem final `sstp-catalogo:phase3`: `sha256:3024dc40bdb28a40846ef57aa28e797c9d7bb3b71e88a3ad4204855431cf1010`; tamanho informado pelo Docker 379997644 bytes. Tags base móveis; digest identifica esta validação.
- Runtime como `app`, UID/GID 1654. Ausência de SDK, node_modules, .git, Microsoft.EntityFrameworkCore.Design.dll e appsettings.Development.json conferida na imagem final. O Dockerfile não recebe conexão no build.
- 25 testes .NET Release aprovados, nenhum ignorado: regressão HTTP/PostgreSQL e novos testes de PORT, TLS Neon e fronteiras SPA/API/assets. O teste de fallback usa HTML mínimo de teste; a validação abaixo usa o dist React real.
- Oito testes frontend aprovados. Frontend visual não modificado.
- PostgreSQL 17.6 descartável `sstp-deploy-postgres`, porta 55433, distinto do banco de desenvolvimento. Credencial aleatória gerada apenas em memória/ambiente Docker.
- SQL idempotente gerado em `artifacts/catalogo-idempotente.sql` (ignorado). Migration aplicada duas vezes: segunda aplicação sem alterações. SQL reaplicado com ON_ERROR_STOP; contagens continuaram 3 categorias, 6 cursos, 12 módulos, 24 aulas. SQL ascendente gerado sem DROP TABLE/DELETE FROM/TRUNCATE.
- Imagem final executada em Production, PORT 10000, HTTP local 5180, conectada ao banco de teste. Quinze GETs aprovados: index, duas rotas SPA, health, listagem, filtros combinados, detalhe, 400, 404 e assets reais. [Resultados](validacao-container.json).
- Instância separada em PORT 10001/HTTP 5181 com destino PostgreSQL inacessível: health e listagem retornaram 503 ProblemDetails sem host/usuário/conexão; index continuou 200. Não foi uma falha do Neon.
- GET /cursos/1 com X-Forwarded-Proto=https respondeu 200 sem redirecionamento. Não equivale a teste real do proxy Render.
- Bundle servido sem URL http(s) localhost/127.0.0.1. Logs consultados não continham a senha de teste.
- Encontrados apenas `.env.example` da raiz e frontend, com marcadores/configuração pública. Configs JSON sem conexão/segredo; .gitignore e .dockerignore revisados. Nenhum scanner dedicado de segredos ou vulnerabilidades da imagem foi executado.

## Ajustes durante a validação

Primeiro comando Docker foi bloqueado pelo sandbox; execução autorizada fora dele permitiu o build. Teste Debug encontrou o executável da API local bloqueado, portanto foi executado Release sem parar esse servidor. `dotnet ef -c Release` foi rejeitado (a opção significa contexto), corrigido para `--configuration Release` antes da aplicação da migration. Inspeção da imagem identificou configuração Development publicada; CopyToPublishDirectory=Never corrigiu isso. Imagem reconstruída após ajustes; frontend reutilizado pelo cache. Bibliotecas EF Design excluídas somente no build ProductionImage.

## Limites e próxima rodada

Neon e Render foram consultados por documentação pública, sem criar contas ou recursos. Não foram validados handshake TLS/CA com Neon, permissão da role, região/quotas da conta, comportamento do proxy Render, cold start integrado ou backup/restauração. Health mede conectividade, não histórico de migrations.

Na próxima rodada autorizada: disponibilizar a fonte aprovada para Render, criar Neon Free dedicado, confirmar banco vazio, aplicar migration via máquina local/conexão direta VerifyFull, configurar Web Service Docker Free e env, publicar, executar o script de smoke pela URL HTTPS e conferir navegação/recarga/mobile/console. Comandos e critérios estão no README.

Os três containers temporários foram encerrados e removidos pelo --rm ao finalizar; a imagem permanece local para reprodução. O PostgreSQL de desenvolvimento e os containers dos outros projetos permaneceram ativos. Nenhum commit, deploy, dado pessoal ou custo foi criado.
