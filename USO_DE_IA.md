# Registro de uso de inteligência artificial

## Ferramenta e objetivo

Codex, assistente de IA no aplicativo desktop, utilizado em 21/10/2026 para analisar os materiais, propor arquitetura e implementar a fase 1 autorizada: API ASP.NET Core/.NET 10, EF Core/PostgreSQL, migrations/seed, base React/TypeScript/Vite, testes e documentação. Não se registra uma versão específica do modelo porque ela não foi confirmada ao usuário.

## Prompts relevantes e aproveitamento

1. Solicitação inicial: “Sua primeira tarefa é realizar exclusivamente uma análise técnica dos materiais fornecidos”, com proibição de implementar. Aproveitado: inventário do ZIP, requisitos do PDF/DOCX e levantamento inicial do Figma. Não foi produzido código nessa etapa.
2. Reconhecimento complementar: “Não invente conteúdo do Figma ou recursos de serviços de hospedagem”. Aproveitado: inspeção visual dos três fluxos disponíveis, separação entre observado e não confirmado e consulta a documentação oficial de hospedagem. Descartada a suposição de que “Ver todos os cursos” abria uma listagem: a interação observada levou aos detalhes.
3. Implementação: “Inicie a FASE 1”, com DTOs públicos, consultas sem tracking, filtros no banco, seed determinístico, health check real, testes e proibição de deploy/commits. Aproveitado: organização por responsabilidades, consultas ILIKE, dados fictícios versionados, cliente HTTP tipado e testes PostgreSQL. Não foram incorporados SQLite, autenticação, Moodle, telas finais nem imagens geradas.

As decisões e o código sugeridos pela IA foram tratados como propostas a validar. Este registro não equivale a aprovação humana do código final.

## Validação realizada

- Restore de pacotes e build .NET; instalação npm e build TypeScript/Vite.
- Conferência das constraints, relações e dados na migration gerada pelo EF Core; verificação de mudanças pendentes do modelo.
- Testes de validação de parâmetros e tradução para SQL PostgreSQL sem servidor de banco.
- Testes HTTP de respostas ProblemDetails e health check com banco propositalmente inacessível.
- Integração com PostgreSQL 17.6 real, em container exclusivo: schema temporário, migration aplicada duas vezes, seis cursos/três categorias/12 módulos/24 aulas, pesquisa, filtros, detalhes e conectividade.
- Resultado final da suíte: 13 aprovados, zero falhas e zero ignorados.
- SHA-256 dos 65 assets copiados comparado ao conteúdo do ZIP, sem diferenças.
- Verificação HTTP pela porta Vite: filtro combinado com um resultado, health Healthy, detalhes com dois módulos/quatro aulas e imagem oficial com status 200.

Não foram validados layout final, acessibilidade completa, instalação limpa em outro computador nem deploy. As primeiras tentativas de acesso HTTP local ao Vite foram bloqueadas/expiraram no ambiente restrito; após executar os servidores e a conferência fora do sandbox, a integração HTTP passou. Não foi realizada inspeção visual da página React no navegador nesta fase.

## Erro identificado e correção

**Erro concreto:** o handler inicialmente reconhecia apenas NpgsqlException/TimeoutException no nível externo. Um teste HTTP com PostgreSQL inacessível esperava 503, mas recebeu 500, pois o EF Core encapsulava a falha transitória.

**Correção:** percorrer a cadeia InnerException procurando a falha transitória do provedor antes de decidir entre 503 e 500. A resposta pública permanece genérica, sem SQL, conexão ou stack trace.

**Validação:** o teste `ConsultaRetorna503SemExporDetalhesQuandoBancoNaoResponde` passou após a correção; a suíte completa com PostgreSQL também passou.

Outro problema detectado no build foi a resolução de versões EF Core diferentes entre API e testes, devido a referências transitivas e ao pacote Design privado. A correção foi declarar explicitamente EF Core e Relational na versão 10.0.13 no projeto da API. O build posterior terminou sem avisos.

## Fase 2 — implementação visual e integração

Solicitação recebida no arquivo de texto anexado pelo usuário, intitulada “FASE 2 — IMPLEMENTAÇÃO VISUAL E INTEGRAÇÃO”. Objetivo: interface profissional, responsiva e acessível, fiel ao Figma, com Landing Page, catálogo complementar e detalhes integrados à API. Restrições preservadas: sem autenticação, matrícula, Moodle/AVA funcional, deploy, custos ou commits.

### Conteúdo aproveitado e decisões

- Fluxos Landing Page e detalhes foram revisitados no navegador antes das alterações. Foram aproveitados composição, hero com fotografia/vermelho, hierarquia, benefícios, carrosséis, seções expansíveis e identidade do rodapé.
- Criados estilos/tokens e componentes React sem biblioteca de UI. Open Sans e imagens/logotipos são exclusivamente os oficiais preservados. Não foram geradas imagens.
- Catálogo e variantes responsivas são adaptações propostas pela IA; não foram apresentados como telas observadas no Figma. Textos, CTA e FAQ foram ajustados para não prometer matrícula, vídeo, aulas ou certificados inexistentes.
- Cursos vêm dos endpoints existentes. Indicadores e depoimentos institucionais estáticos são identificados como ilustrativos; não substituem dados de cursos da API.
- URL representa pesquisa/filtros aplicados. AbortSignal e revisão da requisição protegem contra respostas obsoletas. Não foram alterados o contrato da API nem o banco.
- Prettier foi executado temporariamente para formatar o código; não foi adicionado às dependências do aplicativo.

### Validações reais

- Build frontend executado e cinco testes de URL/concorrência aprovados.
- Treze testes existentes do backend aprovados novamente com PostgreSQL real, nenhum ignorado.
- Landing Page, catálogo e detalhes inspecionados em 360, 390, 768, 1024 e 1440 px; largura do documento medida sem overflow horizontal indevido.
- Busca combinada por Medidas/Matemática/Online/Disponível retornou um resultado na UI; lista de seis, vazio, limpeza, recarga e Voltar também conferidos.
- Detalhes reais com dois módulos e quatro aulas; acionamento por Enter/Espaço. Menu móvel, Escape e devolução do foco ao botão verificados.
- Curso inexistente e erros de comunicação observados: API temporariamente interrompida e depois reiniciada. Recuperação pelos botões da interface confirmou seis cursos e filtros habilitados.
- Fonte oficial carregada; loading foi observado na árvore acessível. Não houve auditor automático, teste com leitor de tela real ou inspeção em dispositivo físico.

### Inadequação encontrada durante a inspeção visual

Em 360px, ocultar o elemento `br` para permitir quebra natural uniu palavras no CTA (“Pronto paradar”) e poderia unir o título de impacto. A correção acrescentou espaços explícitos em JSX antes das quebras. O texto renderizado foi conferido no DOM como “Pronto para dar o próximo passo?” e “Impacto transformador”.

Foi identificado também que repetir o link Sobre com a mesma âncora na URL não reposicionava a página, pois o efeito dependia de uma alteração na URL. O componente de link passou a tratar o destino atual sem criar outra entrada no histórico; a repetição da âncora foi conferida no navegador.

Não se declara fidelidade pixel a pixel: fotografias de cursos ausentes foram substituídas pela reutilização de uma foto oficial, e conteúdo, estados, CTA/FAQ, rodapé e layouts responsivos foram adaptados conforme as limitações registradas no README.

## Fase 2.2 — correções pontuais — 21/10/2026

A IA comparou as capturas oficiais do Figma com a interface e ajustou somente frontend: composição dos destaques, recortes CSS da imagem já retornada pela API, enquadramento/vermelho do hero, limites dos controles de depoimentos e ação de limpeza no erro 400. As três fotos oficiais foram verificadas como distintas por SHA-256; continuam ausentes as fotos individuais dos outros cursos. Nenhum asset foi gerado, baixado externamente ou duplicado.

Oito testes Node passaram, incluindo três novos casos de limites de rolagem; build frontend e checagem TypeScript passaram. Nenhum teste backend foi reexecutado, pois não houve alteração de backend. Durante a inspeção móvel foi observado scrollLeft inicial de 3 px devido ao scroll snap, habilitando a seta anterior; scroll-padding corrigiu o encaixe e os limites foram novamente verificados no navegador.

Foram capturadas as três telas em 390/1440, sem overflow horizontal nas medições finais. Busca combinada retornou um curso; limpeza de status inválido recuperou seis; detalhes e acordeão com dois módulos/quatro aulas e menu móvel por teclado foram conferidos.

Para observar loading, foi criado um proxy temporário em memória que atrasava 2500 ms antes de encaminhar requisições à API real, numa instância Vite separada. Tentativas iniciais não ficaram acessíveis ao navegador; execução fora do sandbox resolveu o acesso. Vazio (200) e inexistente (404) vieram da API. O proxy foi interrompido e reiniciado para verificar falha de conexão e recuperação; isso não é um 503 da API nem um teste de queda do PostgreSQL. Os dois serviços temporários foram encerrados, sem alterar a aplicação principal ou dados.

Evidências preservadas em `docs/evidencias/fase-2-2/`; auditoria atualizada mantendo o histórico anterior. Sem novas dependências, deploy ou commits. Não se declara auditoria WCAG completa nem configuração de produção validada.

## Fase 3.1 — preparação de deploy — 21–22/10/2026

A IA preparou Dockerfile multi-stage Node 24/SDK .NET 10/runtime ASP.NET 10, .dockerignore, configuração Production, PORT, publicação do React em wwwroot e fallback com fronteira para API/health. Não houve mudança visual, de entidades, seed ou migrations. O nome ConnectionStrings__Catalogo foi preservado; validação exige VerifyFull para hosts Neon tanto em runtime como na fábrica EF. HTTPS é terminado no Render, sem redirecionamento pela API ou confiança irrestrita em forwarded headers.

Foram consultadas fontes oficiais Render, Neon, Npgsql e Microsoft. Limites de planos foram documentados com data e links, sem afirmar elegibilidade da conta ou criar serviços. Preparação do banco usa migration explícita local, conexão direta e SQL idempotente; nenhuma operação automática de schema foi adicionada ao startup.

Validações realizadas: Docker Desktop operacional; build Linux com npm ci/build e dotnet publish; imagem final executada como UID 1654 sem SDK/EF Design/config Development; 25 testes .NET Release e oito testes frontend aprovados. PostgreSQL temporário exclusivo recebeu migration duas vezes e reaplicação do SQL idempotente sem duplicar 3/6/12/24 registros. Quinze verificações HTTP passaram com React real, API, SPA e assets. Outra instância com banco inacessível retornou 503 genérico em API/health e 200 no index. Bundle sem URLs locais; senha de teste não encontrada nos logs consultados.

Ocorrências reais: sandbox bloqueou Docker/rede inicialmente, exigindo execução autorizada; executável Debug estava em uso, resolvido com testes Release; uso incorreto de -c no comando EF foi corrigido para --configuration antes de aplicar migrations. Inspeção revelou configuração Development no publish, excluída e imagem reconstruída. Detalhes/digest/resultados estão em docs/VALIDACAO_DEPLOY.md e docs/validacao-container.json.

Nenhuma conta, Neon, Render ou GitHub foi provisionado/publicado; não houve conexão TLS real com Neon, deploy, teste de carga ou restauração de backup. Nenhum segredo completo foi impresso ou gravado nos arquivos do projeto. Credencial de teste existiu apenas em ambiente local descartável; revisão dirigida não substitui scanner dedicado de segredos. Os três containers de teste foram encerrados e removidos; PostgreSQL de desenvolvimento e containers de outros projetos foram preservados. Sem commits ou custos.

## Fase 3.2 — auditoria pré-commit e preparação Git — 22/10/2026

Solicitação explícita do usuário para inicializar Git em main e criar commit inicial somente após auditoria aprovada, sem remoto/publicação. Não havia repositório no workspace nem em diretórios ascendentes; Git foi inicializado em main. A identidade Git já estava configurada e não foi alterada.

A IA inventariou os candidatos, revisou .gitignore/.dockerignore, exemplos de ambiente, configurações, código, referências a conexão e documentação. Varredura local por padrões de chave privada, tokens conhecidos, JWT e URI com credencial não encontrou ocorrências; referências genéricas foram revisadas como variáveis, exemplos ou conexões fictícias de teste sem senha. Gitleaks não estava instalado; não foi instalado scanner ou dependência. A inspeção textual não faz OCR de imagens nem garante ausência de todo segredo possível.

Foram reforçadas exclusões de logs, bancos locais, temporários e secrets.json; adicionadas regras de texto LF/binários em .gitattributes, preservando bytes originais dos assets oficiais. Dois avisos de espaços finais pertenciam à licença oficial e à transcrição de evidência; seus conteúdos foram preservados com exceção específica de verificação de whitespace. Mantidos os assets oficiais e evidências referenciadas; arquivos ignorados foram preservados no disco. README recebeu instruções de remoto com marcadores e distinção entre preparação local e futura publicação. Nenhuma alteração funcional exige repetir builds/testes da aplicação nesta fase.

A revisão final do stage foi aprovada: 163 arquivos, 92 de texto varridos, nenhum padrão de segredo encontrado e nenhum artefato inesperado. Os 65 assets oficiais no stage foram comparados byte a byte com o ZIP original; a normalização inicial da licença foi revertida somente no stage para preservar os bytes. Dez casos de exclusão de arquivos locais passaram. A criação do commit inicial foi autorizada pelo usuário e liberada por essa auditoria; seu hash é consultável no próprio Git. Nenhum remoto, conta ou recurso Neon/Render foi acessado ou criado, e nenhuma configuração global foi alterada.

## Fase 3.4 — tentativa de integração Neon — 22/10/2026

A instalação global solicitada inicialmente falhou por bloqueio do sandbox; repetida com permissão, instalou Neon 8.10.3. A CLI informou que o postinstall de esbuild não estava autorizado pela política npm; essa política não foi alterada. `--version` e `me` executaram; `me` retornou 1 indicando ausência de autenticação válida. Saída de identidade/credenciais foi filtrada, sem imprimir tokens, senhas ou conexões.

Conforme a instrução do usuário, a integração foi interrompida antes de init/deploy ou operações remotas. Nenhum neon.ts/.env foi gerado, migration/seed aplicado, build executado ou integração end-to-end declarada. IDs e branch fornecidos pelo usuário ainda não foram confirmados no Neon. Documentação pública oficial de CLI/login/init/config foi consultada; não foram encontradas skills Neon instaladas nas pastas de skills verificadas.

Foram adicionadas exclusões de contexto .neon ao .gitignore/.dockerignore e registrada a pendência no README. Próximo passo depende de login humano via neon login. Nenhum commit, push, deploy Render ou recurso externo foi criado nesta rodada.

### Retomada autenticada da Fase 3.4 — 22/10/2026

Após login manual, CLI 8.10.3 confirmou autenticação. API confirmou organização informada/Free, projeto sesi-catalogo-cursos, production br-lively-sun-b6s1tndm, região São Paulo e PostgreSQL 18.6. Não foram encontradas skills oficiais instaladas; foram consultados help e documentação oficial. Init usou os identificadores autorizados e --no-agent-setup para não instalar agentes/MCP globais. Gerou neon.ts, manifests npm da raiz e contexto/ambiente ignorados, sem imprimir valores secretos.

Plano somente Postgres/noop revisado antes de deploy. Deploy concluiu sem modificar recursos; não foram criados duplicados nem habilitados outros serviços. A política gerada de expiração de novas branches foi removida por estar fora do escopo; novo dry-run continuou noop. Dependências @neon/config e @neon/env são da configuração, não drivers da aplicação.

Inspeção remota somente leitura confirmou neondb sem tabelas de usuário e sem histórico EF. SQL ascendente revisado, sem DROP/DELETE/TRUNCATE. Script Usar-Neon.ps1 converte a URI direta para Npgsql usando DbConnectionStringBuilder, valida contexto/banco e exporta apenas ConnectionStrings__Catalogo com VerifyFull. Entidades, DbContext, migrations e seed existentes foram preservados. InitialCatalogo aplicada uma vez, incluindo seed; repetição sem pendências. Consulta remota confirmou 3/6/12/24 registros.

Builds backend Release e frontend passaram. Oito testes frontend, 24 testes backend sem banco e um novo teste remoto em transação READ ONLY passaram. O teste PostgreSQL que cria/remove schemas não foi executado contra Neon. A aplicação publicada localmente serviu React e API com Neon; quinze verificações HTTP e o fluxo de catálogo/busca combinada/detalhes no navegador passaram. Evidências JSON e screenshots foram gravadas em docs sem segredos.

Ocorrências reais: runner de testes não conseguiu conectar ao testhost no sandbox, resolvido fora dele; uma expressão SQL foi inicialmente delimitada incorretamente no PowerShell, corrigida antes de executar a inspeção. Wrapper .cmd também não preservou identificadores SQL com aspas, portanto a consulta foi executada pelo entrypoint Node oficial. pg_stat_ssl na sessão da CLI retornou false, registrado separadamente. O teste inicial referiu IsSecure, propriedade ausente em Npgsql 10; foi corrigido para verificar a abertura real com VerifyFull e validação padrão, sem callbacks ou relaxamento de TLS. O teste corrigido passou.

README foi atualizado distinguindo a tentativa sem login da retomada validada e o Neon do Render ainda não publicado. A instância temporária local foi encerrada após validação. Nenhum commit/push, deploy Render, exclusão de registros ou mudança para plano pago foi realizado. Credenciais permaneceram nos arquivos locais ignorados/variáveis de processo, nunca em código, appsettings ou VITE_*.

## Fase 3.5 — auditoria e preparação para publicação GitHub — 22/10/2026

O usuário autorizou commit/push das alterações necessárias, condicionados à auditoria. Foram revisados diff desde 3c5f2ab, arquivos novos, neon.ts, manifests, scripts, teste, documentação e evidências. Varredura dos candidatos por padrões de segredos e pelos valores locais de conexão/senha, sem imprimi-los, não encontrou ocorrências. Exclusões verificadas para .env/.neon, dependências e artefatos; registry dos manifests é público, sem credencial. Identificadores de projeto/branch/banco são metadados, não segredos.

Stage conferido e exportado com checkout-index para artifacts/render-source, sem arquivos locais ignorados. Docker construiu a imagem a partir dessa cópia: restore/publish backend concluídos e cache frontend reutilizado. Imagem Linux como UID 1654, sem SDK/EF Design/Node e arquivos de ambiente; conexão apenas no runtime. Quinze GETs com Neon passaram; health 200. Sem migrations, neon deploy ou alterações no banco. Não foram repetidas suítes já aprovadas, pois não houve alteração funcional.

Uma checagem temporária de logs usou leitura incorreta do DbConnectionStringBuilder no PowerShell e gerou alerta por comparação com valor vazio; commit ficou bloqueado até esclarecer. Repetida com set_ConnectionString/TryGetValue e validação de valor não vazio: senha carregada, nenhum match de senha ou connection string nos logs. Não foi identificada exposição real. Container de teste encerrado. A transcrição de detalhes Neon manteve espaços originais com exceção específica de whitespace em .gitattributes, como a evidência anterior. Corrigida também a forma documentada de passar o filtro com & no PowerShell.

Documentação recebeu configuração exata futura do Render e evidência HTTP Linux/Neon. A auditoria final deve ser confirmada no stage antes do commit/push autorizado; resultado e hash ficam no Git e no relatório ao usuário. Não foi criado serviço Render ou outro recurso, nem alterada configuração global de Git. Credenciais não foram adicionadas ao stage ou à imagem.
