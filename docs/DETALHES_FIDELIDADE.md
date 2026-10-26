# Revisão da página de detalhes — 24/10/2026

Referência observada: protótipo público Figma, frame `6017:707`, em 100%. Editor/camadas de estilos não acessíveis. Comparação por captura; não se declara equivalência pixel a pixel.

## Alterações

- Breadcrumb preservado com Início / Cursos / nome completo do curso, último item com aria-current.
- Colunas, margens, título 40/48px, badge com ícone, data e certificação aproximados da referência. Foto oficial em 16:9, escurecimento e play demonstrativo desabilitado.
- Textos dos quatro benefícios conforme protótipo, apresentados como benefícios institucionais ilustrativos. Não há emissão de certificados ou suporte real neste projeto.
- Sobre o curso e acordeões Módulos, Aulas online e avaliações e Dúvidas frequentes. Modalidade, carga, público e status preservados dentro do segundo acordeão, com aviso de ausência de AVA/avaliações. Módulos/aulas continuam provenientes da API.
- Depoimentos sobre fundo bege, tipografia ajustada; chamada Fale com a gente e rodapé padronizado. Contato desabilitado, sem integração externa.
- Barra fixa com nome completo, Matricule-se e seta oficial. Matrícula desabilitada com explicação acessível. Botão superior também desabilitado.

## Verificações reais

Build TypeScript/Vite aprovado e 21 testes frontend aprovados. Sem execução backend adicional: API, seed e migrations não mudaram nesta rodada.

Navegador em /cursos/1: imagem 664x373,5px, título em três linhas, barra desktop 1150x72px. Módulos abriu dois módulos e quatro aulas. Em 1440 e 390px, documento sem overflow horizontal. Mobile: imagem 335px, barra com 72,6px de altura. Enter abriu Aulas online e avaliações; foco do summary terminou em y=440, abaixo do cabeçalho e acima da barra fixa (y=759). Não houve ensaio em aparelho físico nem auditoria integral de acessibilidade.

Evidências em `evidencias/carrossel-fidelidade/`: `detalhes-revisao-1440.jpg` (viewport 1440x1200), `detalhes-revisao-390.jpg`, `figma-detalhes-topo.jpg` e `figma-detalhes-final.jpg`. Os screenshots da aplicação e do visualizador têm áreas úteis distintas; não são comparação sobreposta calibrada.

## Diferenças preservadas

- Data real fictícia do seed: 01/09/2026, em vez de copiar 05/01/2026 do Figma.
- Descrição longa, público, módulos e aulas permanecem dados fictícios da API; não foram substituídos por texto de apresentação apenas no frontend.
- Breadcrumb é adição solicitada e altera o espaço superior em relação ao protótipo.
- Vídeo, matrícula, contato, avaliações e AVA indisponíveis; controles desabilitados explicitamente.
- Quatro depoimentos existentes, enquanto o protótipo contém mais cartões. Quantidade preservada; limites de navegação refletem o overflow real.
- Layout mobile adaptado; variante móvel oficial não verificada. Recortes, detalhes de ícones e métricas finas ainda aproximados.

Arquivos funcionais alterados: `frontend/src/pages/Details.tsx`, `frontend/src/components.tsx`, `frontend/src/styles.css`. Sem banco, Neon, deploy, commit, push ou regeneração do ZIP. Revisão disponível em http://localhost:5173/cursos/1.

## Texto e programa de /cursos/1 — 24/10/2026

A pedido do usuário, a ressalva anterior da descrição longa divergente foi resolvida neste curso. Descrição e programa completos observados no frame público 6017:707: dez módulos, três tópicos por módulo. Texto versionado em backend/Persistence/CursoMetalurgiaSeed.cs e aplicado pelo seed/migration 20261024225428_AlignMetalurgiaSyllabusWithFigma somente ao banco local catalogo_figma_review. Up revisado contém UpdateData/InsertData, sem DeleteData ou mudanças de esquema. Down reverte os registros acrescentados e não foi executado.

Preservados IDs dos dois módulos/quatro aulas preexistentes; acrescidos oito módulos e 26 aulas/tópicos neste curso. Totais locais atuais: seis cursos, três categorias, 20 módulos e 50 aulas; /cursos/1 tem dez módulos e trinta aulas/tópicos. Os demais cinco cursos permanecem com dois módulos e quatro aulas cada. Isso substitui as contagens históricas 12/24 apenas no ambiente local; Neon e produção permanecem intocados.

Frontend renderiza títulos e tópicos reais da API em listas simples, sem repetir descrições genéricas de aulas. Títulos de módulo 18/26px, tópicos 18/26px em #767676, espaçamento de 32px entre módulos, sem divisórias intermediárias. Coluna Sobre o curso fica sticky apenas acima de 800px, respeitando o cabeçalho. Dados didáticos continuam demonstrativos, inclusive o tópico sobre emissão de certificado; nenhuma emissão foi implementada.

Builds aprovados, 25 testes backend com PostgreSQL local e 21 frontend aprovados. Teste de integração verifica dez módulos, três tópicos em cada, descrição e contagem total, além da idempotência das migrations. Aviso NU1900 de consulta NuGet indisponível permanece. Navegador confirmou descrição, dez módulos/trinta tópicos, tipografia 18px; mobile 390 sem overflow e Enter fechou Módulos. Sem teste de movimento em aparelho físico. Evidências: evidencias/carrossel-fidelidade/sobre-modulos-texto-1440.jpg e figma-modulos-finais.jpg. Sem commit/push, deploy ou regeneração do ZIP.

## Aulas, avaliações e dúvidas compartilhadas nos seis cursos

Frame público 6017:707 inspecionado integralmente: Aulas online, Provas e Avaliações e dez perguntas frequentes. Conteúdo editorial reutilizável em frontend/src/courseLearningContent.ts e CourseLearningPanels.tsx; Details.tsx usa os mesmos componentes para os seis IDs, sem duplicação por página. Tipografia 18/26px, títulos 18/26px, listas e intervalos de 32px, como aproximação da referência.

As respostas 1 e 2 são adaptações explícitas nos cursos semipresenciais (2, 4 e 6), para não prometer formato totalmente online ou ausência de horário presencial. Dados reais de modalidade, carga, público e status permanecem em um bloco complementar. Os textos do Figma descrevem funcionalidades não implementadas; um aviso visível em cada bloco identifica aulas, avaliações, suporte e certificados como conteúdo ilustrativo. Nenhum recurso de AVA, antifraude, biometria, emissão de certificado ou matrícula foi implementado.

Foi tentado abrir Geometria pelo cartão no Figma; o protótipo permaneceu no frame de Landing, sem programa próprio observado. Os módulos/descrições específicos dos cursos 2–6 continuam na API, sem copiar indevidamente os dez módulos de Metalurgia para outros temas. Nesta rodada a padronização dos novos blocos editoriais foi aplicada aos seis cursos; isso não afirma transcrição integral de programas oficiais indisponíveis.

23 testes frontend aprovados, incluindo conteúdo compartilhado e adaptação por modalidade; build aprovado. Um erro real de resolução de módulos por nomes que diferiam apenas em extensão/capitalização no Windows foi corrigido renomeando o componente para CourseLearningPanels.tsx. Banco/backend não alterados; testes backend não repetidos.

Navegador: /cursos/1 a /cursos/6 carregados via API local, dois acordeões abertos em cada rota, dez perguntas e oito tópicos por página. Todos sem overflow em 1440 e 390px. Registro factual em evidencias/carrossel-fidelidade/aulas-faq-seis-cursos-validacao.json. Capturas aulas-avaliacoes-compartilhado-1440.jpg e aulas-faq-compartilhado-390.jpg. Sem testes em aparelho físico ou alegação de fidelidade pixel a pixel. Sem Neon, commit/push, deploy ou recriação de ZIP.

### Remoção solicitada de conteúdo extra

A pedido explícito do usuário, removidos o aviso Conteúdo ilustrativo do protótipo dos dois blocos compartilhados e o bloco de modalidade/carga/público/status de Aulas online e avaliações, nos seis cursos. Dados da API e controles desabilitados não alterados. Removidos estilos/constante sem uso; teste ajustado. Dois testes focados e build aprovados. Navegador confirmou ausência do bloco e do aviso em /cursos/1. Evidência aulas-sem-bloco-extra-1440.jpg. Esta remoção substitui a decisão anterior de exibir o aviso nesses acordeões. Sem banco, commit ou deploy.

## Depoimentos e fim da página — revisão de 24/10/2026

Frame público 6017:707 novamente observado em 1440px. Cinco depoimentos identificados, incluindo Gabriel Santos; preservados os cargos observados (Gestora/Técnico nos cartões 2/3). Acrescentado o quinto depoimento somente na variante bege e ajustada a frase de Fernanda conforme o protótipo. Cartões 275 × 396px, intervalo 16px, trilho com parte do próximo cartão, sem scrollbar visual ou overflow da página. A Home mantém seus quatro depoimentos.

As setas da variante bege navegam suavemente entre as extremidades e fazem retorno circular quando acionadas no limite. Ficam desabilitadas somente quando não há overflow; respeitam reduced motion. Cliques de avanço/retorno e Enter observados no navegador; desktop terminou em scrollLeft 187/0, mobile em 1117/0. Não houve teste de swipe em aparelho físico nem emulação de reduced motion nesta rodada.

Medidas DOM finais em desktop: seção de depoimentos 698px (referência visual aproximadamente 700px), contato 264px, footer 360px. Removido padding adicional de 116px do footer desktop e 121px mobile. A barra fixa de matrícula se oculta quando o footer entra no viewport: adaptação deliberada para não cobrir links e ícones; no Figma ela permanece sobreposta. Contato/matrícula continuam demonstrativos e desabilitados.

Build frontend e três testes focados de scrollBounds aprovados. Em 390px os cinco cartões têm altura 396px, sem conteúdo cortado e sem overflow horizontal da página; rodapé acessível com barra oculta. Sem alterações backend/banco, commit, push, deploy ou ZIP. Evidências: depoimentos-revisao-1440.jpg, contato-footer-revisao-1440.jpg, depoimentos-revisao-390.jpg e footer-revisao-390.jpg, em docs/evidencias/carrossel-fidelidade/. Não se afirma equivalência pixel a pixel; largura do frame Figma 1366px dentro do viewport de 1440px.
