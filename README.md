# Catálogo de cursos SESI-SP

Aplicação demonstrativa para o teste técnico de Desenvolvedor de Sistemas I. As fases 1 e 2 implementam persistência PostgreSQL, API de consulta e interface React com Landing Page, catálogo e detalhes. Todo conteúdo dos cursos é fictício. Entrega prevista: 26/10/2026 às 08h, horário de São Paulo.

Estado atual: integração real com Neon validada na Fase 3.4 (22/10/2026), com EF Core/Npgsql e TLS VerifyFull. Publicação no Render ainda não realizada. Os registros das fases anteriores abaixo são históricos; não substituem a validação atual descrita ao final.

## Tecnologias e arquitetura

- ASP.NET Core Web API, .NET 10 e EF Core 10.0.13; provedor Npgsql 10.0.3.
- PostgreSQL 17.6 no Compose de desenvolvimento; outra instância PostgreSQL pode ser configurada por conexão.
- React 19, TypeScript 5.9 e Vite 8; versões resolvidas no package-lock.json.
- xUnit e WebApplicationFactory para testes HTTP; testes de integração usam PostgreSQL real.

Fluxo: React → API HTTP → serviço de consulta → EF Core/Npgsql → PostgreSQL. Um projeto de API, sem microsserviços, autenticação, CQRS ou repositório genérico. O frontend usa URLs relativas e proxy Vite em desenvolvimento, sem precisar liberar CORS indiscriminadamente. Na imagem de produção, o ASP.NET serve o build React em `wwwroot` e a API pelo mesmo domínio.

## Estrutura

```text
frontend/
  src/api/                 cliente HTTP e contratos TypeScript
  src/pages/               Landing Page, catálogo e detalhes
  src/components.tsx       componentes compartilhados
  src/styles.css           tokens, tipografia oficial e responsividade
  src/catalogQuery.ts      URL e proteção contra respostas obsoletas
  tests/                   testes frontend de URL e concorrência
  public/assets/oficiais/  somente os 65 arquivos do ZIP fornecido
backend/
  Contracts/               DTOs públicos
  Entities/                Categoria, Curso, Modulo e Aula
  Persistence/             DbContext, mapeamentos, seed e migrations
  Endpoints/               rotas e validação de entrada
  Services/                consultas, erros e health check
tests/                     validação, SQL, HTTP e integração PostgreSQL
compose.yaml               PostgreSQL de desenvolvimento
dotnet-tools.json          ferramenta EF local
USO_DE_IA.md                registro factual de assistência e validação
```

## Pré-requisitos e configurações

.NET SDK 10 e Node.js >= 22.12, com npm. Docker é opcional: os builds não o utilizam. Não há SQLite nem banco em memória substituindo PostgreSQL.

| Variável | Uso |
|---|---|
| `ConnectionStrings__Catalogo` | Obrigatória para executar API/aplicar migrations; conexão Npgsql |
| `ASPNETCORE_URLS` | Endereço da API, quando não definido pelo perfil/CLI |
| `POSTGRES_PASSWORD` | Senha escolhida localmente para inicializar o Compose |
| `POSTGRES_PORT` | Porta publicada pelo Compose; padrão 55432, somente loopback |
| `CATALOGO_TEST_CONNECTION` | Banco PostgreSQL exclusivo para testes; usuário precisa criar schemas |
| `API_PROXY_TARGET` | Destino do proxy Vite; padrão http://localhost:5080 |
| `VITE_API_BASE_URL` | Prefixo público da API; manter vazio para usar o proxy/local mesmo domínio |
| `ASPNETCORE_ENVIRONMENT` | Development local; Production na imagem |
| `PORT` | Porta HTTP definida pelo Render; vincula 0.0.0.0, entre 1024 e 65535; sem variável, runtime da imagem usa 8080 |

Nenhuma senha real está nos arquivos do projeto. `.env` e variações locais estão no `.gitignore`; arquivos `.env.example` têm somente marcadores. O Compose pode ler `.env`, mas `dotnet run` não o carrega. Variáveis `VITE_*` entram no JavaScript público: nunca incluir segredos. Não configurar origem externa no frontend sem antes definir uma política CORS explícita na API.

## Execução local

Comandos PowerShell, a partir da raiz. Escolha uma senha local e não a versione:

```powershell
$env:POSTGRES_PASSWORD = '<sua-senha-local>'
$env:POSTGRES_PORT = '55432'
docker compose -p sstp-dev up -d postgres
$env:ConnectionStrings__Catalogo = "Host=127.0.0.1;Port=55432;Database=catalogo;Username=catalogo;Password=$env:POSTGRES_PASSWORD;Timeout=5"
```

Sem Docker, configure `ConnectionStrings__Catalogo` com uma instância PostgreSQL existente dedicada ao projeto. Compilar não exige essa variável nem conexão ativa.

Durante esta implementação foi criado o container `sstp-dev-postgres-1` na porta 55432, com senha aleatória mantida apenas na configuração local do Docker. Para reutilizá-lo, recupere a senha para a variável, sem imprimi-la:

```powershell
$env:POSTGRES_PASSWORD = ((docker inspect sstp-dev-postgres-1 | ConvertFrom-Json).Config.Env | Where-Object { $_ -like 'POSTGRES_PASSWORD=*' }) -replace '^POSTGRES_PASSWORD=',''
$env:ConnectionStrings__Catalogo = "Host=127.0.0.1;Port=55432;Database=catalogo;Username=catalogo;Password=$env:POSTGRES_PASSWORD;Timeout=5"
```

A senha de um volume PostgreSQL já inicializado não muda apenas por trocar a variável do Compose. Os containers de outros projetos não foram modificados.

```powershell
dotnet tool restore
dotnet restore Sesi.Catalogo.slnx
dotnet build Sesi.Catalogo.slnx --no-restore
dotnet ef database update --project backend
dotnet run --project backend --launch-profile http
```

API: http://localhost:5080. Migrations são uma operação explícita: a inicial cria tabelas e insere os dados; a aplicação nunca executa migrations automaticamente. Revisar migrations futuras antes de aplicá-las a um banco com dados.

Em outro terminal:

```powershell
cd frontend
npm ci
npm run dev
```

Frontend: http://localhost:5173. Rotas: `/` (Landing Page), `/cursos` (catálogo) e `/cursos/{id}` (detalhes). Para compilar: `npm run build`; para testes frontend: `npm test`. As telas consultam a API real, sem catálogo mockado.

## Modelo de dados e seed

Categoria 1:N Curso; Curso 1:N Modulo; Modulo 1:N Aula. Chaves inteiras; categoria com código único. Campos textuais obrigatórios e com limites; carga horária e ordens positivas; ordem única por curso/módulo. Categoria referenciada não pode ser excluída; exclusão de curso propaga para módulos/aulas no modelo, sem endpoints de escrita.

Curso armazena nomes completo/breve, resumo, descrição, categoria, modalidade, status, horas, público-alvo, caminho da imagem e data opcional (`date`). Modalidade e status têm constraints CHECK e códigos controlados:

- Modalidades: `online`, `semipresencial`.
- Status: `disponivel`, `em-breve`, `encerrado`.
- Categorias: `matematica`, `ciencias-da-natureza`, `linguagens`.

Seed versionado em `CatalogoSeed`/migration: seis cursos, três categorias, duas modalidades, três status, 12 módulos e 24 aulas. Cursos de medidas, geometria têxtil, energia, química, comunicação e documentação, com cargas de 12 a 32 horas e públicos distintos. IDs e datas são fixos; nenhuma data depende do relógio da execução. Reaplicar a mesma migration não duplica dados. Status é explícito, sem transição automática por data.

O catálogo usa provisoriamente a mesma fotografia oficial `banner-ava.png` nos seis cursos. Não foram inventadas fotografias específicas. O ZIP original foi preservado e os 65 arquivos copiados tiveram SHA-256 comparado aos originais; a licença OFL acompanha as fontes.

## Contrato da API

Respostas JSON com propriedades em camelCase. Operações assíncronas, cancelamento propagado e consultas sem tracking. Filtros são traduzidos para SQL PostgreSQL, antes da materialização.

### GET /api/cursos

Parâmetros opcionais, no máximo um valor por parâmetro:

| Parâmetro | Regra |
|---|---|
| `busca` | Até 150 caracteres após trim; trecho literal no nome completo OU breve; ILIKE sem distinguir caixa, mantendo acentos significativos |
| `categoria` | Código cadastrado; até 60 caracteres |
| `modalidade` | `online` ou `semipresencial` |
| `status` | `disponivel`, `em-breve` ou `encerrado` |

Vazios/espaços são ignorados. Parâmetros desconhecidos, repetidos, longos ou códigos inválidos retornam 400. Os nomes dos parâmetros e códigos são exatos. `%`, `_` e barra invertida na busca são literais, não curingas. Filtros combinam com AND. Sem paginação nesta fase; ordenação por nome completo e ID, segundo a collation do banco.

Resposta 200: `{ "items": [...], "total": 6 }`. Cada item contém `id`, `nomeCompleto`, `nomeBreve`, `resumo`, `categoria`, `modalidade`, `status`, `cargaHoraria`, `imagem` e `dataDisponibilizacao`. Categoria/modalidade/status são objetos `{ codigo, nome }`; data é ISO `YYYY-MM-DD` ou null. Zero resultados retorna 200, lista vazia e total zero. O total é calculado sobre os mesmos resultados filtrados, sem consulta separada sujeita a divergência.

Exemplo: `/api/cursos?busca=Medidas&categoria=matematica&modalidade=online&status=disponivel`.

### GET /api/cursos/{id}

ID inteiro positivo. Resposta 200: `{ curso, descricao, publicoAlvo, modulos }`. `curso` tem o formato do item da listagem. Cada módulo contém `id`, `titulo`, `descricao`, `ordem`, `aulas`; cada aula contém `id`, `titulo`, `descricao`, `ordem`. Módulos/aulas são ordenados por ordem e ID. ID malformado/não positivo: 400; inexistente: 404.

### GET /api/catalogo/filtros

Resposta 200: `{ categorias, modalidades, status }`, com listas de `{ codigo, nome }`. Categorias ordenadas por nome e ID; demais listas têm ordem fixa.

### GET /health

Testa conectividade com o banco configurado. 200: `{ "status": "Healthy" }`; 503 se o banco não responde. Não garante que todas as migrations foram aplicadas.

### Erros

`application/problem+json`: `type`, `title`, `status`, `detail` quando pertinente, `instance`, `traceId`; validação acrescenta `errors` por campo. Indisponibilidade transitória do banco retorna 503, erro inesperado retorna 500. Respostas não incluem conexão, SQL ou stack trace. Logs de diagnóstico ficam no servidor e não devem ser publicados como artefatos sem revisão.

## Testes e validação

Sem serviços externos: `dotnet test Sesi.Catalogo.slnx --filter 'Category!=PostgreSQL'`. Para a suíte completa, configure uma conexão de teste:

```powershell
$env:CATALOGO_TEST_CONNECTION = $env:ConnectionStrings__Catalogo
dotnet test Sesi.Catalogo.slnx
```

Use banco dedicado ao projeto/testes. O teste integrado cria um schema de nome aleatório, aplica a migration duas vezes, verifica contagens e endpoints, e remove somente esse schema ao terminar. Não apaga o banco. Sem a variável, esse teste aparece explicitamente como ignorado; com conexão inválida, falha em vez de simular PostgreSQL.

**Executado e confirmado em 21/10/2026:**

- Restore do backend e ferramenta EF; build da solução sem avisos/erros.
- `npm install`, auditoria reportando zero vulnerabilidades e `npm run build` com TypeScript/Vite.
- Migration inicial gerada, aplicada ao PostgreSQL exclusivo e repetida no teste sem duplicar seed.
- 13 testes aprovados, zero ignorados, com PostgreSQL real: validação, tradução SQL, pesquisa pelos dois nomes, filtros combinados, lista vazia, busca literal, detalhes, filtros disponíveis, 404 e saúde 200/503; indisponibilidade da consulta retorna 503.
- Verificação do modelo EF contra a migration e integridade dos assets.
- Verificação HTTP real com API/Vite fora do sandbox: filtro combinado retornou um curso, detalhes retornaram dois módulos/quatro aulas, health retornou Healthy e imagem oficial retornou 200. Os servidores temporários foram encerrados após a conferência; PostgreSQL de desenvolvimento permanece disponível.

**Instruções ainda não reproduzidas integralmente:** instalação do zero em outra máquina, `npm ci` em checkout limpo, conexão com outro provedor PostgreSQL e execução sem Docker. Não confundir esses caminhos documentados com validações concluídas.

## Responsividade, acessibilidade e limites

A interface usa Open Sans variável do ZIP, cores institucionais e tokens de tipografia/espaçamento. Layouts fluidos, cartões em três/duas/uma colunas e menu recolhível até 800px; carrosséis têm rolagem horizontal própria, sem estender a largura do documento. Há HTML semântico, skip link, labels explícitos, foco visível, navegação de páginas com foco no conteúdo, estados com `role=status`/`role=alert`, imagens decorativas com alt vazio e seções nativas `details/summary`. `prefers-reduced-motion` desativa animação, transições e rolagem suave. Barras fixas têm espaço de rolagem reservado.

O Figma disponibilizou três fluxos: inicial, detalhes e AVA; Landing Page e detalhes foram revisitados antes desta implementação. Não foram localizadas variantes móveis nem telas próprias de busca/filtros/erro/vazio/carregamento. O catálogo e as composições responsivas são adaptações documentadas, não telas extraídas do protótipo.

Não há autenticação, matrícula, reprodução de vídeo, Moodle ou gerenciamento de cursos. O AVA observado não foi implementado. Os tipos TypeScript representam o contrato, sem validação adicional de payload em runtime. Não há certificação formal de conformidade WCAG ou teste com leitor de tela real.

## Fase 2 — interface e validação

- **Landing Page:** cabeçalho transparente no hero e branco após rolagem, apresentação institucional, cursos reais da API em carrossel, indicadores ilustrativos, benefícios, depoimentos fictícios, CTA e rodapé.
- **Catálogo:** pesquisa submetida por botão/Enter, três filtros simultâneos, contagem da API, limpeza, loading/vazio/erro e nova tentativa. URL guarda o estado aplicado; o formulário permite editar antes de aplicar. Recarregar e Voltar reconstroem a pesquisa. Não há filtro local substituindo SQL.
- **Concorrência:** `AbortController` cancela requisições anteriores; uma revisão lógica invalida callbacks antigos mesmo se o transporte ignorar o cancelamento. Desmontagem cancela a operação; transições não exibem o resultado da URL anterior.
- **Detalhes:** metadados, data pt-BR sem deslocamento de fuso, descrição, público-alvo, módulos/aulas reais e seções expansíveis. 400/404 exibem curso não encontrado; outras falhas oferecem nova tentativa. Matrícula está explicitamente indisponível.
- **Contrato/backend:** nenhuma alteração de comportamento, contrato, entidades, seed ou migrations nesta fase.

### Fidelidade e diferenças intencionais

Foram comparados visualmente hierarquia, uso de fotografias, vermelho/branco/preto/bege, cartões, benefícios, depoimentos e disposição dos detalhes. A família tipográfica é a oficial; medidas, escala e espaçamentos são interpretação das telas visualizadas, sem acesso ao Dev Mode. Não se declara reprodução pixel a pixel.

1. Fotografia oficial `banner-ava.png` reutilizada nos cartões, com três recortes CSS por ID; faltam as fotografias individuais dos outros cursos do Figma. Os recortes não representam novas imagens e não eliminam a repetição. Nenhum logotipo foi recriado ou imagem gerada.
2. Cursos, nomes, datas, ordenação e dois módulos/quatro aulas por curso refletem o seed, não uma cópia integral do conteúdo do protótipo, que mostra dez módulos no curso demonstrado.
3. Catálogo independente, busca, filtros, estados e variantes móveis são composições complementares.
4. CTA de matrícula/área do aluno explicitamente indisponíveis; imagem de vídeo sem botão de reprodução. CTA principal dos detalhes leva ao conteúdo. FAQ foi adaptada ao caráter demonstrativo, sem prometer aulas ou certificados.
5. Rodapé mantém a identidade e oferece destinos existentes no projeto. Contatos, redes e páginas externas não foram simulados como funcionais. Indicadores e depoimentos são identificados como ilustrativos.

### Evidências executadas nesta fase

- Build TypeScript/Vite concluído; cinco testes frontend aprovados, cobrindo serialização/reconstrução da URL, limpeza e bloqueio de respostas obsoletas/canceladas.
- Os 13 testes .NET existentes passaram novamente com PostgreSQL real, zero ignorados. Backend não foi modificado nem recompilado nesta fase.
- Navegador: Landing Page, catálogo e detalhes inspecionados nas larguras **360, 390, 768, 1024 e 1440 px**. Medição do DOM confirmou `scrollWidth` igual ao `clientWidth` do documento; rolagem dos carrosséis é intencional.
- Integração visual com seis cursos reais, busca por Medidas com categoria Matemática/modalidade Online/status Disponível retornando um curso; zero resultados, limpeza, persistência após recarga e restauração por Voltar conferidos.
- Detalhes exibiram dois módulos/quatro aulas da API; Enter/Espaço expandiram/recolheram conteúdo. Menu móvel operou por teclado; Escape fechou e devolveu foco ao botão. Foco visível conferido.
- API foi interrompida para observar erros de catálogo/filtros e reiniciada; botões de nova tentativa recuperaram seis resultados. Curso inexistente foi observado com resposta real da API.
- Open Sans carregada no navegador e imagens sem falhas nos trechos conferidos. Loading inicial foi observado na árvore acessível.

A inspeção ocorreu em um navegador desktop com viewport ajustado, não em dispositivos físicos ou múltiplos motores. Não foram executados auditor automático de acessibilidade, leitor de tela real ou simulação do ajuste de movimento do sistema operacional. As regras de redução de movimento foram revisadas no CSS. A proteção contra respostas fora de ordem foi testada de forma determinística em unidade; não houve simulação de rede reordenada no navegador.

Ao concluir a fase 2, API (5080), Vite (5173) e PostgreSQL (55432) foram mantidos localmente para revisão. Isso não constitui deploy. Os comandos de execução acima permitem reiniciá-los quando necessário.

## Publicação e entregáveis posteriores

### Fase 2.2 — correções verificadas em 21/10/2026

Cartões de destaque simplificados conforme referência (metadados preservados no catálogo), hero recalibrado, controles de depoimentos desabilitados conforme os limites e limpeza como ação principal no erro de filtro inválido. API e banco não foram alterados.

Executados: oito testes frontend aprovados, `npm run build` aprovado e TypeScript sem erros. As três telas foram revistas em 390/1440 px. Busca combinada, URL, limpeza, detalhes, acordeão e menu móvel conferidos. Loading foi observado com proxy temporário atrasando respostas reais; vazio e 404 vieram da API, e falha/recuperação foram verificadas interrompendo somente o proxy. Não se declara um teste de indisponibilidade do banco nesta rodada. Serviços de teste encerrados; sem dependências novas. Evidências e limites estão em [docs/AUDITORIA_VISUAL.md](docs/AUDITORIA_VISUAL.md), seção Fase 2.2, e `docs/evidencias/fase-2-2/`.

A interface está validada localmente para avançar à preparação da primeira publicação. Configuração de produção e conferência no provedor continuam pendentes; não há deploy validado.

Nenhum deploy, commit ou publicação no GitHub foi realizado. A Fase 3.1 prepara a configuração abaixo; contas, recursos externos e disponibilidade para esta conta ainda não foram validados. Antes da entrega, produzir ZIP de publicação, links públicos e relação final de limitações/desvios do Figma. O uso de IA está registrado separadamente em `USO_DE_IA.md`.

## Fase 3.1 — publicação preparada, sem deploy

### Imagem e ambiente

`Dockerfile` na raiz: Node 24 executa `npm ci` e build; SDK .NET 10 restaura/publica Release; runtime ASP.NET 10 recebe somente publish e dist. Usuário `app` (UID 1654), sem SDK, Node, node_modules, bibliotecas EF Design ou configuração Development na imagem final. `.dockerignore` limita o contexto e exclui .env, certificados, dumps, testes e artefatos locais. As tags de imagens são móveis: rebuild pode receber patches; o resultado validado está registrado em `docs/VALIDACAO_DEPLOY.md`.

Produção usa `appsettings.json` + `appsettings.Production.json` + variáveis, sem conexão embutida. O build força `VITE_API_BASE_URL` vazio: frontend consulta `/api` na mesma origem. `API_PROXY_TARGET` é apenas desenvolvimento. Não colocar segredos em `VITE_*`, Docker ARG, Dockerfile ou URL pública.

Render termina HTTPS e encaminha HTTP ao container. A aplicação não força redirecionamento HTTPS, não gera URLs absolutas nem confia indiscriminadamente em forwarded headers; assim não cria loop de proxy. Se futuramente passar a depender do esquema/IP encaminhado, configurar proxies confiáveis explicitamente. A aplicação não deve ser exposta diretamente em HTTP público fora desse desenho.

`PORT` tem precedência sobre ASPNETCORE_URLS quando fornecida. `EXPOSE 8080` é apenas metadado do padrão local; a porta efetiva no Render vem de `PORT`. Não sobrescrever o Docker Command: o ENTRYPOINT já inicia a API. Configurar **Health Check Path `/health`** no Render; não há curl nem agente de health adicional na imagem. [Docker no Render](https://render.com/docs/docker), [porta e HTTPS](https://render.com/docs/web-services), [health checks HTTP](https://render.com/docs/health-checks).

### Neon Free — procedimento preparado na Fase 3.1

Na Fase 3.4 foi integrado o projeto existente informado pelo usuário; não foi criado outro projeto. Os passos abaixo permanecem como referência de preparação de um novo ambiente. O projeto real confirmado usa PostgreSQL 18.6, conforme registro ao final.

1. No console Neon, escolher explicitamente Free, criar projeto dedicado e selecionar PostgreSQL 17 e região próxima ao Render, caso disponíveis. Não fazer upgrade nem habilitar recursos cobrados. Confirmar limites e disponibilidade exibidos antes de criar.
2. Usar banco exclusivo vazio e role proprietária para migrations. Em **Connect**, selecionar branch/banco/role corretos. Desativar pooling para obter o hostname direto; a aplicação também usará conexão direta nesta solução de uma instância, com pool Npgsql limitado. Pooler poderá ser adotado depois de validado, sem trocar a conexão administrativa.
3. Converter os campos fornecidos para **formato Npgsql**, não colar uma URI `postgresql://` em `ConnectionStrings__Catalogo`. Exemplo exclusivamente ilustrativo:

```text
Host=<hostname-direto-neon>;Port=5432;Database=<banco>;Username=<role>;Password=<senha>;SSL Mode=VerifyFull;Timeout=15;Command Timeout=30;Maximum Pool Size=10;Include Error Detail=false
```

Usar hostname DNS original, não endereço IP, e codificação/quoting Npgsql correta para senhas com caracteres especiais. Não usar `Trust Server Certificate=true`, callbacks de aceitação de certificado ou SSL Disable/Prefer/Require. Para hosts `.neon.tech`, API e fábrica de migrations rejeitam modos diferentes de VerifyFull. O banco local de teste sem TLS é uma exceção restrita a ambiente local, não um exemplo de conexão Neon.

`VerifyFull` valida CA e hostname usando as raízes confiáveis do sistema. Falha de certificado deve ser investigada sem desativar a validação. A conexão Neon/TLS real foi validada posteriormente na Fase 3.4. [TLS no Neon](https://neon.com/docs/connect/connect-securely), [semântica SSL do Npgsql](https://www.npgsql.org/doc/security.html), [conexões diretas/pooling](https://neon.com/docs/connect/connection-pooling).

### Migrations e seed — operação explícita pela máquina local

A migration inicial `20261021233255_InitialCatalogo` cria tabelas/constraints/índices e insere IDs/datas fixos: 3 categorias, 6 cursos, 12 módulos e 24 aulas. EF mantém `__EFMigrationsHistory`; repetir `database update` ou o SQL **idempotente** não reinsere o seed já aplicado. Não há EnsureCreated nem Migrate no startup. O método Down existe para reversão, mas não faz parte do procedimento abaixo: não executar downgrade, drop ou recriação.

Antes de executar, confirmar projeto/branch/banco vazio no console e listar tabelas. Em banco já ocupado: obter backup e revisar histórico/schema e SQL pendente; não aplicar a inicial por tentativa nem apagar tabelas para resolver conflito. Não editar seed/migrations já aplicados. Uma nova migration deve passar por revisão própria.

PowerShell, raiz do projeto, .NET SDK 10 e acesso de rede ao Neon. A entrada protegida evita gravar a conexão no histórico do terminal:

```powershell
dotnet tool restore
dotnet restore Sesi.Catalogo.slnx
dotnet build backend --configuration Release --no-restore
$conexaoProtegida = Read-Host 'Conexão Npgsql DIRETA do banco destinado às migrations' -AsSecureString
$env:ConnectionStrings__Catalogo = [Net.NetworkCredential]::new('', $conexaoProtegida).Password
New-Item -ItemType Directory -Path artifacts -Force | Out-Null
dotnet ef migrations script --idempotent --project backend --configuration Release --no-build --output artifacts/catalogo-idempotente.sql
# Revisar o SQL, destino e backup antes do comando seguinte.
dotnet ef database update --project backend --configuration Release --no-build
# Repetição deve informar que o banco já está atualizado.
dotnet ef database update --project backend --configuration Release --no-build
Remove-Item Env:ConnectionStrings__Catalogo
```

Interromper a sequência se qualquer comando retornar erro; não executar os seguintes. `-c` no dotnet ef significa **contexto**, portanto usar `--configuration Release`. O SQL gerado não contém a conexão e fica ignorado em artifacts. Alternativa sem ferramenta EF no servidor: revisar e aplicar esse SQL idempotente pelo cliente PostgreSQL local (`psql`, com senha via prompt/passfile protegido) usando conexão direta com TLS verificado e `ON_ERROR_STOP=1`. Não depender de shell ou job no serviço gratuito. Verificar as contagens e histórico após aplicar. Nunca usar o teste automatizado com conexão de produção: ele cria e remove schemas próprios.

### Render Free — instruções para a próxima rodada, não executadas

Após autorização específica para publicação e fonte disponibilizada em repositório acessível ao Render:

1. **New → Web Service**, selecionar repositório/branch aprovados; **Language: Docker**; Root Directory raiz; Dockerfile Path `./Dockerfile`, contexto raiz.
2. Selecionar explicitamente **Free**, região próxima ao Neon, sem discos ou serviços adicionais pagos. Não configurar build/start command customizado nem pre-deploy de migrations. Desabilitar auto-deploy inicialmente para controlar a publicação.
3. Em Environment, cadastrar `ConnectionStrings__Catalogo` com conexão Npgsql direta/TLS do banco já migrado; `ASPNETCORE_ENVIRONMENT=Production`. `PORT` normalmente é fornecida pelo Render (padrão documentado 10000); não definir ASPNETCORE_URLS ou variáveis VITE. Segredo apenas no painel, sem screenshots/logs contendo valor.
4. Definir **Health Check Path: `/health`**. Conferir configurações e confirmar criação/deploy somente na rodada autorizada. O build Docker não necessita acesso ao banco ou sua senha.
5. Abrir URL HTTPS e conferir health 200/Healthy, seis cursos, busca combinada, detalhe, refresh em `/cursos/1` e assets. `/api/inexistente` precisa retornar 404 ProblemDetails, nunca o index da SPA.

No PowerShell 7, `./scripts/Test-Publicacao.ps1 -BaseUrl 'https://<servico>.onrender.com'` executa GETs de fumaça sem gravar/alterar dados. Ele espera exatamente o seed demonstrativo. Fazer também a navegação manual, mobile e observação de console/rede, inclusive após cold start. Isso ainda não foi executado no Render.

### Limites gratuitos e riscos operacionais

Fontes oficiais consultadas em 21–22/10/2026; não são garantia de oferta futura ou elegibilidade da conta:

- **Render Free:** suspensão após 15 minutos sem tráfego, reativação em cerca de um minuto; filesystem efêmero; 750 horas por workspace/mês; limites de banda/build compartilhados. Sem shell, jobs avulsos ou disco persistente. Pode reiniciar/suspender serviços. Adequado a demonstração, sem promessa de disponibilidade contínua. [Limites oficiais](https://render.com/docs/free).
- **Neon Free:** página atual anuncia 100 CU-h/mês e 1 GB por projeto, compute até 2 CU; escala a zero após cinco minutos inativo. Quotas, retenção limitada e oferta devem ser reconfirmadas no console. Não tratar restauração gratuita como backup completo. [Preços/limites atuais](https://neon.com/pricing).
- Health consulta conectividade PostgreSQL: 200 `{"status":"Healthy"}` ou 503 ProblemDetails genérico. Não comprova que migrations/todas as consultas estão corretas; o smoke test cobre o catálogo. Sondas frequentes podem manter o compute Neon ativo e consumir quota: acompanhar consumo após publicar. Cold starts e indisponibilidade externa podem causar falhas de health/reinícios; não relaxar a sonda para esconder banco indisponível.
- PostgreSQL externo preserva dados entre builds; o container não escreve dados permanentes. Backup/exportação e recuperação real no Neon, TLS real, limites de memória/carga, conta e integração Render↔Neon permanecem não validados.

### Reprodução e diagnóstico locais

```powershell
docker info
docker build -t sstp-catalogo:local .
# Defina ConnectionStrings__Catalogo por entrada protegida, sem registrar a senha.
# Docker Desktop alcança o PostgreSQL do host por host.docker.internal.
docker run --rm --name sstp-catalogo-local -p 127.0.0.1:5180:10000 -e PORT=10000 -e ConnectionStrings__Catalogo sstp-catalogo:local
# Outro terminal, PowerShell 7:
./scripts/Test-Publicacao.ps1 -BaseUrl http://127.0.0.1:5180
docker logs --tail 60 sstp-catalogo-local
docker stop sstp-catalogo-local
```

`host.docker.internal` é específico do Docker Desktop; não usar no Render. Em Linux, conectar por rede Docker apropriada ou configurar host gateway. Ao terminar, remover variável de conexão da sessão; não usar `docker inspect` completo ou compartilhar ambiente/logs sem revisão. Segredos passados como env são acessíveis ao administrador Docker, portanto usar credencial descartável nos testes.

Se health falhar: conferir nome da variável, hostname/porta, banco/role, TLS/CA, quotas e disponibilidade Neon; nunca imprimir a conexão. Se health passar e catálogo falhar: conferir migrations e permissões. Se houver HTML em `/api`, rever o artefato/rotas; se refresh da SPA falhar, conferir wwwroot e fallback. Reverter imagem pode recuperar código, mas não desfaz migrations; preferir correção progressiva revisada, com backup antes de alterar schema. Não remover volumes ou recriar banco como diagnóstico.

**Executado localmente nesta fase:** Docker build multi-stage; npm ci/build Linux; 25 testes .NET aprovados sem ignorados, oito frontend aprovados; imagem final como UID 1654, sem SDK/EF Design/config Development; migrations duas vezes e reaplicação SQL idempotente em PostgreSQL 17 temporário; smoke test de 15 GETs com SPA/API/assets; 503 para API/health com banco inacessível e SPA ainda 200; proxy HTTPS sem redirecionamento. [Registro](docs/VALIDACAO_DEPLOY.md) e [respostas HTTP](docs/validacao-container.json).

**Não executado:** criação de contas/recursos, conexão Neon, deploy Render, publicação GitHub, teste TLS com o provedor, recuperação de backup, carga e instalação integral em outra máquina. Nenhum segredo real foi gravado no repositório; revisão dirigida de .env/config e logs não equivale a scanner completo de segredos.

## Repositório local — Fase 3.2

Repositório inicializado em `main`, sem remoto configurado nesta rodada. Código, migrations, testes, manifests, documentação e 65 assets oficiais são candidatos à versão inicial. Capturas referenciadas pela auditoria são evidências; ZIPs locais, materiais originais PDF/DOCX, builds, dependências, SQL gerado em artifacts, logs, bancos e configurações privadas permanecem fora do Git. Nenhum arquivo local foi excluído. `.gitattributes` mantém texto em LF e imagens/fontes como binários.

Auditoria pré-commit em 22/10/2026: varredura dos arquivos de texto candidatos por padrões de chaves privadas, tokens e URIs autenticadas, seguida de revisão das referências a senha/conexão. Não foram encontradas credenciais reais; os `.env.example` contêm apenas marcador e configuração pública. Não havia histórico anterior a auditar. Essa verificação não substitui scanner dedicado nem garante ausência de todos os tipos de segredo.

Para conectar posteriormente um repositório **vazio** criado pelo usuário, substituir os marcadores e conferir o destino antes de qualquer envio:

```powershell
git status
git remote add origin https://github.com/<usuario>/<repositorio>.git
git remote -v
# Somente quando houver autorização para publicar:
git push -u origin main
```

Não usar force push. Se o remoto já tiver histórico, interromper e revisar sua integração antes de enviar. Nenhum comando de conta externa ou publicação foi executado nesta fase; Neon/Render continuam sem validação externa.

## Fase 3.4 — integração Neon validada em 22/10/2026

Na tentativa inicial, CLI oficial 8.10.3 instalada, mas `neon me` retornou 1; nenhuma operação remota foi feita naquela tentativa. Após autenticação manual pelo usuário, a retomada confirmou autenticação e acesso. Não foram encontradas skills oficiais Neon instaladas nas pastas consultadas; foram usados help da CLI e documentação oficial, sem instalar MCP/integrações globais de agentes.

Destino confirmado pela API Neon: organização `org-noisy-tree-27200340`, plano Free; projeto `empty-tooth-58563543` (`sesi-catalogo-cursos`), região `aws-sa-east-1`; branch `production` / `br-lively-sun-b6s1tndm`; banco `neondb`, PostgreSQL 18.6. Somente PostgreSQL, sem criar projeto/branch/database duplicados ou habilitar outros serviços.

`neon init` foi executado com os IDs confirmados e `--no-agent-setup`. Foram gerados `.neon`, `.env.local`, `neon.ts` e manifests npm na raiz. O plano foi revisado antes de `neon deploy`: somente Postgres, ação noop, sem conflitos/avisos. Deploy concluiu sem alterações de recursos. A política genérica de expiração de novas branches foi retirada do arquivo gerado; o plano final continuou noop. [Plano final](docs/validacao-neon-plano.json).

As dependências npm da raiz (`@neon/config` e `@neon/env`) servem apenas à configuração/CLI; não substituem EF Core nem fazem parte do bundle React ou imagem de produção. Não existe script build na raiz: usar `npm --prefix frontend run build` e `npm --prefix frontend test`.

### Conexão local e reprodução

`.env.local` contém `DATABASE_URL`, `DATABASE_URL_UNPOOLED` e `NEON_BRANCH` fornecidos pela CLI. `.neon` mantém o contexto local. Ambos estão ignorados e não devem ser versionados, impressos ou enviados ao frontend. Não colocar credenciais em appsettings ou em `VITE_*`.

Após login/contexto confirmado e `neon init`/pull do ambiente, usar PowerShell 7:

```powershell
. ./scripts/Usar-Neon.ps1
# O script lê somente DATABASE_URL_UNPOOLED, valida contexto/banco,
# converte URI para Npgsql com quoting e exporta ConnectionStrings__Catalogo.
# Não imprime ou grava a conexão; força SSL Mode=VerifyFull e pool máximo 10.
dotnet build backend --configuration Release --no-restore
dotnet run --project backend --configuration Release --no-build --no-launch-profile --urls http://127.0.0.1:5080
# Ao encerrar a execução, remover a variável da sessão:
Remove-Item Env:ConnectionStrings__Catalogo
```

Migrations continuam explícitas. Antes de reaplicar em qualquer destino, reconfirmar projeto/branch/database e revisar estado/SQL. No banco autorizado, a inspeção encontrou zero tabelas de usuário e nenhum histórico EF. Foi aplicada somente `20261021233255_InitialCatalogo`, que incluiu o seed. Segunda execução de `dotnet ef database update --project backend --configuration Release --no-build` não aplicou mudanças. Não houve EnsureCreated, downgrade, exclusão ou recriação de dados. Contagens remotas: **3 categorias, 6 cursos, 12 módulos e 24 aulas**. [Consulta de contagens](docs/validacao-neon-banco.json).

Teste remoto opcional e explicitamente somente leitura:

```powershell
. ./scripts/Usar-Neon.ps1
$env:CATALOGO_NEON_READONLY_CONNECTION = $env:ConnectionStrings__Catalogo
dotnet test tests --configuration Release --filter Category=NeonReadOnly
Remove-Item Env:CATALOGO_NEON_READONLY_CONNECTION,Env:ConnectionStrings__Catalogo
```

Esse teste abre a conexão Npgsql com VerifyFull, confere migration/contagens em transação READ ONLY e faz rollback. **Não usar `CATALOGO_TEST_CONNECTION` com o Neon de produção**: o teste PostgreSQL anterior cria/remove schemas de teste. Para a suíte local sem banco, usar `--filter 'Category!=PostgreSQL&Category!=NeonReadOnly'` (com aspas no PowerShell).

### Evidências e limites desta validação

- Backend Release compilado sem avisos/erros; frontend TypeScript/Vite compilado; oito testes frontend e 24 testes backend sem dependência de PostgreSQL aprovados. Um teste novo NeonReadOnly passou contra o banco remoto, sem modificar registros.
- Instância temporária ASP.NET Production em 127.0.0.1:5184 serviu o dist React real pelo mesmo domínio. Quinze GETs passaram: health 200/Healthy, seis cursos, pesquisa/filtros combinados (um resultado), detalhes com dois módulos/quatro aulas, SPA e assets/erros. [Resultados HTTP](docs/validacao-neon-http.json).
- No navegador: Landing com cursos da API; catálogo com seis resultados; busca Medidas/Matemática/Online/Disponível retornou um curso e refletiu URL; detalhes reais e acordeão exibiram quatro aulas. Console consultado sem error/warn. [Catálogo](docs/evidencias/neon/catalogo-neon.jpg), [detalhes](docs/evidencias/neon/detalhes-neon.jpg).
- A consulta `pg_stat_ssl` da sessão SQL da CLI retornou false; esse valor foi preservado na evidência e não usado para atestar o transporte da aplicação. Npgsql abriu a conexão com VerifyFull, sem callbacks de aceitação de certificado. A CLI e a aplicação são clientes distintos; a topologia interna do provedor não foi auditada.
- A instância temporária de validação foi encerrada ao fim; conexão local pode ser reproduzida pelo script. Não houve teste de carga, cold start integrado no Render, restauração de backup ou validação TLS a partir da imagem Linux no Render.

Próxima publicação ainda depende de autorização, revisão/versionamento das mudanças locais e configuração Render Free/segredo ConnectionStrings__Catalogo. Neon está validado a partir desta máquina; Render não foi publicado ou testado. Quotas e consumo devem ser acompanhados; nenhum plano pago foi habilitado. Nesta rodada não houve commit ou push.

## Fase 3.5 — revisão para GitHub e Render — 22/10/2026

Alterações Neon revisadas para publicação em `main`, no remoto `https://github.com/synchronist/sesi-catalogo-cursos.git`. O contexto `.neon`, `.env.local`, dependências locais, bin/obj/dist e artifacts permanecem excluídos. `neon.ts`, manifests, script de conexão, teste somente leitura e evidências contêm apenas conteúdo publicável. O script não grava nem imprime a conexão; ela é disponibilizada ao processo por ConnectionStrings__Catalogo.

Foi construída a imagem `sstp-catalogo:render-ready` a partir de uma cópia do stage Git, sem arquivos locais ignorados. Digest observado: `sha256:1978db163b180bc50b548d9933648ca73faadcd1c15c4a18e900605b3c559108`. Build multi-stage aprovado, com cache do frontend e restore/publish backend concluídos. A imagem Linux foi executada localmente, UID 1654, PORT 10000, ligada ao Neon com VerifyFull somente por variável de runtime. Quinze verificações HTTP passaram, incluindo health 200, cursos, pesquisa/filtros, detalhes, SPA e assets. [Evidência](docs/validacao-render-preparo-http.json).

A inspeção confirmou ausência de SDK, node_modules, EF Design, `.env` e `.neon` na imagem, e nenhuma variável de conexão/credencial em sua configuração. Logs consultados sem senha ou conexão completa. Sem migrations/seed no startup, sem neon deploy nesta fase e sem escrita de dados no Neon. Containers temporários encerrados. As suítes já aprovadas na Fase 3.4 não foram repetidas; a validação adicional cobriu a execução Linux com TLS/Neon, antes não comprovada.

Configuração a usar na criação futura do Render, **não executada nesta fase**:

| Campo | Configuração |
| --- | --- |
| Repositório | `https://github.com/synchronist/sesi-catalogo-cursos` |
| Branch | `main` |
| Tipo / Language | Web Service / Docker |
| Root Directory | Raiz do repositório; campo vazio |
| Dockerfile Path / Docker Build Context | `./Dockerfile` / `.` |
| Instance Type | Free, se disponível à conta |
| Health Check Path | `/health` |
| Docker Command / pre-deploy command | Não sobrescrever / não configurar migrations automáticas |
| Variáveis próprias | `ConnectionStrings__Catalogo` (segredo Npgsql com VerifyFull) e `ASPNETCORE_ENVIRONMENT` |
| Variável fornecida pela plataforma | `PORT` |

Não configurar VITE_*, DATABASE_URL, NEON_API_KEY ou ASPNETCORE_URLS no serviço. Não usar o build npm da raiz: o Dockerfile constrói frontend/ e backend/. A conexão validada deve ser cadastrada de modo privado no painel, sem ser copiada para arquivos versionados, argumentos de build ou frontend. Publicação e verificações efetivas na infraestrutura Render continuam pendentes de uma rodada autorizada. Fonte dos campos: [Docker](https://render.com/docs/docker), [Web Service/PORT](https://render.com/docs/web-services), [health](https://render.com/docs/health-checks).
