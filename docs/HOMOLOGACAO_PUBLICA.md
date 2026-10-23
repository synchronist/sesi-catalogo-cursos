# Homologação pública — Fase 3.7

Data: 22/10/2026, horário de São Paulo. URL: https://sesi-catalogo-cursos.onrender.com.

Auditoria somente leitura. Não foram alterados código, banco, infraestrutura ou configurações. Sem builds, commits ou deploy. Foram criados apenas este relatório e evidências em `docs/evidencias/publica/`.

## Resultado por verificação

| Verificação | Resultado realmente observado |
| --- | --- |
| GET /health | Primeira consulta: **503**, ProblemDetails com título **Banco indisponível**, após 7,25 s. Sem intervenção, consultas seguintes: **200**, `{"status":"Healthy"}`. Em uma amostra posterior, 1,52 s. |
| GET /api/cursos | **200**, total **6**. |
| Pesquisa | `busca=Medidas`: **200**, total **1**. Pesquisa por nome observada na UI, seguida da combinação dos filtros. |
| Categoria individual | `categoria=matematica`: **200**, total **2**. |
| Modalidade individual | `modalidade=online`: **200**, total **3**. |
| Status individual | `status=disponivel`: **200**, total **3**. |
| Filtros combinados | Medidas + Matemática + Online + Disponível: **200**, total **1**, confirmado na interface e na URL. |
| Limpeza | Botão de limpeza removeu a query e restaurou **6 cursos**. |
| Vazio | `busca=zzzz-sem-curso`: **200**, total **0**; UI mostrou “Nenhum curso encontrado”. “Limpar busca e filtros” restaurou seis. |
| Detalhes | `/api/cursos/1`: **200**, **2 módulos e 4 aulas**. UI abriu o curso Medidas e Escalas na Metalurgia; Enter expandiu o acordeão com as quatro aulas. |
| Navegação | Landing → Catálogo → Detalhes → Catálogo pelo menu móvel → Landing pelo logotipo, todos observados. GET direto das rotas SPA também respondeu 200 no script. |
| Responsividade | Três telas em **1440 e 390 px**, sem overflow horizontal nas seis medições. Nenhum corte ou desalinhamento impeditivo identificado nas capturas. |
| Imagens/fontes | Imagens visíveis carregadas, sem falhas. Dois cartões lazy fora da tela estavam inicialmente pendentes; após navegar o carrossel, zero pendentes/falhas. Open Sans confirmada por CSS/FontFaceSet; arquivo da fonte e fotografias responderam 200. |
| Console/rede | Consulta do console retornou zero error/warn na amostra. Endpoints e assets foram verificados por HTTP; houve o 503 inicial real. Resource Timing/HAR completo não ficou disponível na ferramenta, portanto não se declara captura integral da rede do navegador. |
| HTTPS | HTTPS aceito pelos clientes sem desativar validação de certificado. HTTP retornou **301** para a URL HTTPS. Nenhum src/href HTTP de imagem/script/stylesheet observado no DOM. Não foi auditada toda configuração de protocolos/ciphers do servidor. |
| Credenciais | Nenhum match de padrões de segredo ou valores locais conhecidos no HTML, JS, CSS e respostas examinadas. Sem URLs localhost no bundle. `/.env`, `/.env.local`, `/.neon` e `/appsettings.json`: **404**. É uma verificação dirigida, não garantia absoluta de ausência de toda exposição. |
| Script existente | `scripts/Test-Publicacao.ps1 -BaseUrl https://sesi-catalogo-cursos.onrender.com`: **15 verificações aprovadas**, após a primeira consulta de health. Inclui 400, 404, SPA, assets e conteúdo real da API. |
| Commit | GitHub/main e main local: **29e997bbf597f84a2f4e8061dbfd36d5a4e8d25c**. SHA efetivamente implantado no Render: **não verificado**, conforme limites abaixo. |

As verificações individuais de filtros foram feitas por HTTP; combinação, contagem, URL, limpeza e vazio também foram acionados manualmente no navegador. Não se afirma que todas as combinações possíveis tenham sido percorridas.

## Disponibilidade e cold start

O 503 inicial veio da aplicação ASP.NET com ProblemDetails de banco indisponível, não de uma página de carregamento Render observada. A recuperação aconteceu sem mudança ou reinício provocado pela auditoria. A causa exata não foi identificada; não é correto atribuí-la conclusivamente a cold start Render ou Neon apenas com esses dados.

A documentação do [Render Free](https://render.com/docs/free) informa suspensão após 15 minutos sem tráfego e reativação que pode levar cerca de um minuto. **Não foi feito ensaio controlado após 15 minutos de inatividade**, nem consultados logs privados para correlacionar o 503. Duração da ferramenta do navegador não foi usada como métrica de cold start.

Ressalva operacional: o primeiro acesso pode falhar ou demorar. Antes da entrega formal, recomenda-se observar um acesso após inatividade e correlacionar os logs já existentes, sem presumir que o 503 esteja definitivamente resolvido.

## Comparação da versão implantada

- API pública GitHub e `git ls-remote` confirmaram `main` em `29e997bbf597f84a2f4e8061dbfd36d5a4e8d25c`.
- Endpoints públicos de deployments, commit statuses e check runs do GitHub não forneceram registro Render correlacionável: zero deployments/check runs/statuses. O estado agregado `pending` sem checks não comprova deploy pendente ou malsucedido.
- HTML/cabeçalhos da aplicação consultados não expuseram SHA. Não havia aba autenticada do Dashboard Render disponível na ferramenta; nenhum login adicional foi realizado.
- JavaScript e CSS públicos são byte a byte iguais ao dist local validado. Isso **não prova o SHA do backend/container**, especialmente porque esses arquivos frontend não diferenciam os dois commits existentes.
- Pendente: conferir o SHA do deploy ativo na tela de deploys Render e compará-lo com main. Não houve novo deploy para obter essa informação.

## Evidências

| Tela | 1440 px | 390 px |
| --- | --- | --- |
| Landing | [captura](evidencias/publica/landing-1440.jpg) | [captura](evidencias/publica/landing-390.jpg) |
| Catálogo | [captura](evidencias/publica/catalogo-1440.jpg) | [captura](evidencias/publica/catalogo-390.jpg) |
| Detalhes | [captura](evidencias/publica/detalhes-1440.jpg) | [captura](evidencias/publica/detalhes-390.jpg) |

- [Filtros combinados](evidencias/publica/filtros-combinados.jpg) e [estado vazio](evidencias/publica/estado-vazio.jpg).
- [HTTP e segurança](evidencias/publica/http-seguranca.json), [assets/fontes](evidencias/publica/https-assets.json), [viewport, console e recursos](evidencias/publica/viewport-fontes-rede.json), [versão/HTTPS](evidencias/publica/versao-https.json).
- Largura útil das capturas: 1425/375 px, pois a scrollbar ocupa 15 px. Detalhes estão com módulos abertos. A barra fixa pode aparecer no corpo da captura completa, artefato da captura de uma página rolável; não foi montada ou editada.

## Parecer

**Fluxos funcionais públicos aprovados após recuperação do banco, com ressalvas.** Não foi encontrado bloqueio persistente de navegação ou dados durante a amostra aquecida. A homologação final para entrega fica condicionada à confirmação do SHA ativo no Render e à avaliação do 503 no primeiro acesso. Não se declara disponibilidade contínua, teste de carga, auditoria integral de acessibilidade/segurança ou teste em dispositivos físicos/múltiplos motores.

Nenhuma correção foi aplicada nesta auditoria.

## Fechamento — Fase 3.8, 23/10/2026

Esta seção atualiza o parecer acima, preservando o registro histórico da Fase 3.7. Não houve alteração de implementação, banco, infraestrutura, deploy, commit ou push. As 15 verificações públicas anteriores continuam representativas do comportamento funcional do código revisado; não foram repetidas integralmente e não garantem disponibilidade contínua.

### Health: conclusão e limite da investigação

Revisados `backend/Services/DatabaseHealthCheck.cs` e `backend/Program.cs`: a sonda executa `Database.CanConnectAsync`, com timeout configurado de cinco segundos. Falha de conectividade ou da execução da sonda resulta em unhealthy; o endpoint retorna 503 ProblemDetails genérico, sem detalhes internos. Não verifica integridade do schema, migrations ou seed.

O 503 anterior foi produzido pela aplicação, que estava atendendo HTTP naquele instante. Indica falha na verificação de conectividade, mas não distingue timeout, rede, TLS, autenticação, limite de conexões ou indisponibilidade do PostgreSQL. Não demonstra, isoladamente, suspensão Render, suspensão Neon ou falha geral de inicialização. A diferença entre timeout configurado e duração HTTP total não identifica a causa. Logs Render/Neon correlacionados ao incidente não estavam disponíveis nesta investigação: **causa raiz não confirmada**. Não se atribui automaticamente a cold start.

Nova consulta real nesta rodada: **GET /health → 200, `{"status":"Healthy"}`**. Recuperação observada, sem prova de resolução permanente. Nenhuma indisponibilidade foi provocada.

### Commit: confirmação documental

- `git log -1` local e nova consulta `git ls-remote origin refs/heads/main` confirmaram **29e997bbf597f84a2f4e8061dbfd36d5a4e8d25c**.
- Fonte Render: **informação explícita do usuário nesta solicitação**, de que o Dashboard mostrou anteriormente **29e997b como Live**. O identificador corresponde ao commit completo acima. Não foi obtida nova captura autenticada do Dashboard nesta rodada.
- A pendência documental é encerrada com essa confirmação humana. Não houve verificação automatizada do SHA executado pelo backend, nem se transforma a comparação de JS/CSS em prova do container. A evidência JSON da Fase 3.7 mantém corretamente o estado “não verificado” daquela data.

### Barra fixa de detalhes

Inspeção real de `/cursos/1`, em viewport **1440 × 960** e **390 × 844**. O link “Conhecer o conteúdo” alcançou a seção; Enter expandiu módulos/aulas em ambos. No móvel também foram expandidos Modalidade/público-alvo e Dúvidas frequentes. Foco visível, quatro aulas acessíveis por rolagem, rodapé alcançável; nenhum bloqueio permanente de leitura ou dos controles observado. Console amostrado: zero error/warn.

Desktop: barra de 1200 × 78 px, topo y=862; summary focado y=434–498. Móvel: barra de 351 × 71,69 px, topo y=760,31; summary focado y=376–440, sem overflow horizontal. A barra sobrepõe transitoriamente a faixa inferior enquanto se rola; o conteúdo pode ser levado à região livre. Reserva inferior do rodapé (140/145 px) permite alcançar seu conteúdo acima da barra. Não foi identificado defeito concreto que justifique alteração visual.

Comparação com a captura oficial já existente `evidencias/figma-detalhes-viewport.jpg` e os registros da auditoria visual: o Figma apresenta barra inferior escura com título e CTA. A aplicação preserva essa composição; título breve e matrícula explicitamente indisponível são adaptações da demonstração. Esta rodada comparou a captura anterior, sem novo ensaio interativo no Figma. Comportamento móvel do protótipo permanece não verificado.

Evidências novas, sem montagem ou edição, somente viewport:

- [Barra desktop](evidencias/fechamento/barra-desktop-1440.jpg) e [rodapé desktop](evidencias/fechamento/rodape-desktop-1440.jpg).
- [Barra móvel](evidencias/fechamento/barra-mobile-390.jpg), [acordeões móveis](evidencias/fechamento/acordeoes-mobile-390.jpg) e [rodapé móvel](evidencias/fechamento/rodape-mobile-390.jpg).

### Inventário de entrega

Requisitos conferidos no PDF original, seção 5 (página 3). O PDF também exige registrar/justificar diferenças do Figma e possibilitar execução local, mesmo em indisponibilidade temporária da hospedagem.

| Entregável | Situação e ação restante |
| --- | --- |
| GitHub | Disponível: https://github.com/synchronist/sesi-catalogo-cursos. Relatório público/evidências e fechamento ainda locais, sem commit nesta rodada. |
| URL pública | Disponível: https://sesi-catalogo-cursos.onrender.com; ressalva transitória de disponibilidade documentada. |
| Frontend e backend | Presentes em `frontend/` e `backend/`, publicados no GitHub/main confirmado. |
| Scripts/migrations/banco | Migrations EF em `backend/Persistence/Migrations/`; procedimentos e scripts existentes. Não é necessário entregar dump com credenciais/dados de infraestrutura. |
| README.md | Existente; estado atual corrigido nesta rodada. Alteração ainda local. |
| USO_DE_IA.md | Contém ferramenta, finalidade, prompts/aproveitamento, validações e erros/correções reais; fechamento acrescentado, ainda local. |
| ZIP/relação de arquivos de publicação | **Falta preparar e conferir o pacote final e seu inventário.** O ZIP de materiais fornecidos não é o entregável. Não foi encontrado ZIP final em artifacts. |
| Limitações/itens não concluídos | Documentados no README e auditorias: fotos oficiais reutilizadas por falta de imagens individuais; catálogo/mobile adaptados sem variantes Figma verificadas; sem matrícula, vídeo, autenticação ou Moodle; disponibilidade gratuita sem garantia, causa do 503 não confirmada. Moodle é diferencial opcional no PDF. Consolidar essa relação dentro do pacote final. |

### Parecer final e empacotamento recomendado

**Avançar para a preparação da entrega. Nenhum defeito crítico foi identificado nesta rodada.** Homologação funcional mantida, confirmação documental do commit recebida, ressalva operacional do 503 preservada sem diagnóstico inventado. Builds e suítes completas não foram repetidos porque não houve implementação nova.

Próxima rodada: preparar ZIP de publicação acompanhado de manifesto com caminhos, finalidade, tamanhos e SHA-256; incluir instruções de execução local, configuração por nomes de variáveis, migrations, limitações e links. Para evitar ambiguidade da expressão “arquivos de publicação” no PDF, incluir os arquivos efetivos do publish ASP.NET com React em wwwroot, indicando plataforma/runtime alvo, além da relação dos fontes/Dockerfile/lockfiles disponíveis no GitHub. Gerar/verificar esses artefatos em etapa própria e provar extração/execução do pacote. Excluir .env/.neon, segredos, dependências de desenvolvimento, caches, logs e dados locais. Não entregar diretórios temporários de builds anteriores como pacote final sem revisão.

Antes do envio formal, sincronizar a documentação final e suas evidências com o repositório somente após autorização específica para commit/push. Não falta nova funcionalidade obrigatória identificada nesta revisão; faltam empacotamento, conferência do inventário e consolidação/versionamento final da documentação.

## Encerramento de empacotamento — Fase 4, 23/10/2026

ZIP real de publicação Linux x64 gerado, com React em wwwroot, manifesto e checksums. CRC/hash/extração aprovados; quinze GETs aprovados executando o pacote extraído contra PostgreSQL local em sessões somente leitura. Não houve consulta/escrita Neon, nova homologação pública completa ou deploy; essa validação local não resolve nem diagnostica o 503 público anterior. Detalhes em [VALIDACAO_PACOTE.md](VALIDACAO_PACOTE.md), inventário em [ENTREGAVEIS.md](../ENTREGAVEIS.md). Pendência de empacotamento encerrada; permanecem revisão/versionamento autorizado da documentação e envio formal. Ressalvas e confirmação humana do Dashboard Render permanecem como registradas.

## Finalização documental — Fase 5, 23/10/2026

Documentação e evidências incluídas na finalização autorizada de main; os estados “ainda local/pendente” das seções anteriores retratam suas respectivas datas/fases. Revisão funcional 29e997b preservada. Links GitHub e aplicação pública responderam HTTP 200 nesta rodada; não foram repetidos os testes funcionais/visuais completos. CRC, manifesto e checksums do ZIP reconferidos sem recriação. Não houve nova investigação dos logs Render ou atribuição de causa ao 503. A confirmação humana anterior do Dashboard não se converte em verificação automatizada do SHA do backend. Release/upload do pacote permanece somente proposto, sem execução; envio formal ao SESI pendente.
