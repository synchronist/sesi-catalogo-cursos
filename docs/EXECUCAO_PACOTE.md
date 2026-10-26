# Execução do pacote de publicação

Alvo: Linux x64 com glibc (por exemplo Debian/Ubuntu), framework-dependent. Instalar **ASP.NET Core Runtime 10**, não apenas o runtime .NET. O pacote não inclui SDK, Node.js, PostgreSQL ou runtime autocontido; não se destina a Windows/ARM/Alpine. React já está compilado em `app/wwwroot`, sem build na máquina de execução.

Fonte: https://github.com/synchronist/sesi-catalogo-cursos, revisão de implementação `29e997bbf597f84a2f4e8061dbfd36d5a4e8d25c`. Consulte `PUBLICACAO.json` para ambiente de geração. Documentos finais podem estar posteriores a essa revisão; não se declara compilação bit a bit idêntica entre SDKs ou ao container Render.

## Integridade

Na raiz extraída, execute `sha256sum -c SHA256SUMS`. O manifesto CSV relaciona caminhos, tamanhos e SHA-256 dos arquivos de conteúdo. `SHA256SUMS` cobre também o manifesto; nenhum arquivo tenta conter seu próprio hash. O checksum externo do ZIP deve ser conferido antes da extração. Não alterar arquivos antes dessa conferência.

## Banco e migrations: operação explícita, antes da aplicação

Use PostgreSQL dedicado ao projeto, sem apagar dados existentes. Conexão Npgsql é obrigatória em `ConnectionStrings__Catalogo`. Exemplos abaixo usam somente marcadores fictícios. Nunca usar variáveis `VITE_*`, appsettings ou arquivos versionados para credenciais. Para Neon use conexão **direta**, banco/branch confirmados e `SSL Mode=VerifyFull`, sem desativar validação de certificados. O pacote não conhece credenciais ou contexto da conta Neon.

As migrations compiladas estão na DLL da API; `migrations/*.cs` são referências auditáveis, não scripts executáveis. Para preparar um banco vazio, obtenha a revisão fonte indicada e use .NET SDK 10 e a ferramenta local dotnet-ef 10.0.13:

```bash
git clone https://github.com/synchronist/sesi-catalogo-cursos.git
cd sesi-catalogo-cursos
# Usar o sourceCommit registrado no PUBLICACAO.json do pacote recebido.
dotnet tool restore
dotnet restore backend/Sesi.Catalogo.Api.csproj
dotnet ef migrations list --project backend
dotnet ef migrations script --idempotent --project backend --output catalogo-revisar.sql
# Revisar destino, histórico __EFMigrationsHistory, tabelas e SQL antes de aplicar.
# Somente depois de aprovar o SQL e confirmar as migrations pendentes:
dotnet ef database update --project backend
```

Configure `ConnectionStrings__Catalogo` no ambiente dessa sessão antes dos comandos EF, usando um gerenciador de segredos ou entrada protegida. Exemplo fictício de formato: `Host=SEU_HOST;Database=SEU_BANCO;Username=SEU_USUARIO;Password=SUA_SENHA;SSL Mode=VerifyFull`. Em PostgreSQL local sem TLS, ajuste à política da instância; a exigência VerifyFull é obrigatória para Neon.

A migration inicial cria schema e seed; as três migrations posteriores alinham os cursos, imagens e programa de Metalurgia ao Figma. Estado final: 3 categorias, 6 cursos, 20 módulos e 50 aulas; curso 1 com 10 módulos e 30 aulas. Não execute Down, não use EnsureCreated e não recrie bancos. Em banco existente compare o histórico e aplique somente migrations pendentes compatíveis após revisar seu SQL e destino. Não há migrations/seed automáticos no startup nem comando avulso de seed necessário.

## Executar

Na raiz do pacote extraído, com a conexão já configurada em ambiente:

```bash
export ASPNETCORE_ENVIRONMENT=Production
export PORT=8080
cd app
dotnet Sesi.Catalogo.Api.dll
```

`PORT` aceita 1024–65535 e vincula HTTP em 0.0.0.0. O diretório corrente precisa ser `app` para servir os assets. Use usuário sem privilégios. Para exposição pública, coloque proxy HTTPS à frente; não exponha banco, logs ou diretórios privados. A aplicação não aplica migrations nem seed na inicialização.

## Verificar e diagnosticar

```bash
curl -i http://127.0.0.1:8080/health
curl -i http://127.0.0.1:8080/api/cursos
curl -i http://127.0.0.1:8080/api/cursos/1
curl -i http://127.0.0.1:8080/cursos/1
```

Esperado com banco preparado: health 200 Healthy, seis cursos, detalhe do curso 1 com dez módulos/trinta aulas (demais cursos: dois módulos/quatro aulas); rotas SPA e assets com 200. Rotas API inexistentes devem retornar 404 ProblemDetails, nunca HTML da SPA. Health 503 significa falha na verificação de conectividade, não diagnóstico definitivo da causa; confira DNS, TLS, rede e disponibilidade sem imprimir conexão/senha. Health não verifica schema/seed. Se o banco estiver indisponível, a SPA pode abrir e as consultas falharem corretamente. Se faltar runtime ou configuração, corrija o requisito sem apagar banco. Não aplicar migrations como tentativa genérica de corrigir conectividade.

## Reproduzir o empacotamento

No repositório, PowerShell 7, Node >=22.12 e .NET SDK 10: `npm --prefix frontend ci`, `dotnet tool restore`, `./scripts/Empacotar-Publicacao.ps1 -OutputDirectory entregaveis-nova-geracao`. O script não instala dependências npm, preserva pastas existentes e gera publish + manifesto/checksums. Depois auditar segredos, compactar a pasta gerada com ZIP e conferir CRC, hashes e execução do conteúdo extraído. O script interrompe se houver .env privado no frontend; permite somente .env.development.local com API_PROXY_TARGET de loopback, excluído do pacote e do build Production. Credenciais de banco não são necessárias para compilar. Não incluir a saída no Git.

Consulte `docs/LIMITACOES.md`; o README e registro de IA copiados em docs são documentação do desenvolvimento, cujos caminhos de fontes/evidências referem-se ao repositório.
