# Revisão visual comparativa — Principais cursos

24/10/2026. Esta revisão substitui as conclusões visuais da tentativa anterior em CARROSSEL_PRINCIPAIS.md; não altera registros históricos de testes. Implementação somente local, sem backend/banco/API/migrations/seed, commit, push, deploy ou novo ZIP.

## Acesso realmente obtido

- Protótipo oficial Landing 6017:342 aberto novamente no navegador. Selecionada a opção **Tamanho real (100%)**. Não se utilizou a antiga captura reduzida como se fosse escala 1:1.
- Arquivo de design tentou abrir, mas exibiu “Cadastre-se ou faça login”. Medidas das camadas, CSS original e Dev Mode **não acessíveis** nesta sessão. Dimensões/cores abaixo são medições da renderização/screenshot em 100%, não propriedades exportadas do editor.
- A apresentação é canvas. Inventário de recursos observados identificou nove imagens: oito fotografias e uma imagem de quadriculado. O exportador do navegador classificou as fotografias como `other` e não as exportou. Foram recuperados os arquivos pelas URLs de imagem já observadas no carregamento público, sem consultar APIs privadas, fontes externas ou inventar fotografias. URLs assinadas temporárias ficaram somente em artifacts, ignorado no Git.

## Diferenças encontradas e correção

| Item | Tentativa anterior | Revisão atual |
| --- | --- | --- |
| Cartões desktop | 238x390; cinco completos forçados | **275x396**, alinhados à renderização oficial, quatro completos e parte do seguinte na área útil equivalente |
| Gap | 14px | **16px**; inícios x=145, 436, 727 e 1018 no par calibrado |
| Título da seção | Letter-spacing comprimido | Open Sans 40px/48px, sem tracking negativo |
| Texto dos cartões | Título 22px, descrição 14px | Título **24px/31px**, descrição **16px/21px**, padding 24px e alinhamento inferior |
| Badge | Menor e deslocado | Recuo 24px; fonte 11px/500, gap 8px; badge Matemática medido em 109,95px, aproximadamente 110px na referência |
| Cor/gradiente | Uma imagem já avermelhada recebia nova sobreposição | Fotos originais sem o tratamento do banner, gradiente vermelho progressivo e base aproximada do RGB observado na captura |
| Controles | Distância inferior excessiva | Margem compensada pelo padding do trilho; CTA à esquerda, setas oficiais 48px à direita |
| Fotografia | banner-ava repetido nos seis cards | Seis fotografias distintas recuperadas do próprio Figma, sem modificar os arquivos oficiais anteriores |
| Ordem | Alfabética recebida da API | Apresentação por ID na Landing: Medidas, Geometria, Energia, Química, Comunicação, Documentação; mesmos objetos e conteúdo real, sem mutar resposta/ordem do catálogo |

Fotografias originais em `frontend/public/assets/oficiais/figma/`. [Origem e SHA-256](evidencias/carrossel-fidelidade/assets-origem.json): seis arquivos conferidos byte a byte com os recursos baixados. Os 65 arquivos originais do ZIP permanecem preservados; os seis recursos adicionais vieram do protótipo, não daquele ZIP.

Recortes foram comparados às fotos originais. Uma análise de correlação de bordas no trecho superior dos cards, sem alterar imagens, ajudou a recuperar ampliações/deslocamentos — especialmente o recorte têxtil, que no protótipo mostra tecidos, e não o enquadramento inteiro com a pessoa. [Estimativas e scores](evidencias/carrossel-fidelidade/recortes-estimados.json). São aproximações inferidas da imagem renderizada, **não transforms extraídos das camadas Figma**. A imagem mantém sua proporção; recorte via CSS apenas.

Associações: Medidas/metalurgia, Geometria/têxtil, Energia/soldagem, Química/laboratório; Comunicação/controle da produção e Documentação/equipamento são associações ilustrativas, pois esses dois cursos reais não têm correspondentes exatos no frame. Apenas o placeholder `banner-ava.png` é substituído na apresentação de destaque; imagem específica que a API venha a fornecer mantém prioridade. Catálogo/detalhes e contrato da API não foram alterados.

## Comparação verificável e escala

- [Figma, captura original 1440x960, 100%](evidencias/carrossel-fidelidade/figma-1440-100.jpg).
- [Aplicação real, captura calibrada 1440x960](evidencias/carrossel-fidelidade/aplicacao-1440-calibrada.jpg).
- [Captura lado a lado](evidencias/carrossel-fidelidade/comparacao-lado-a-lado.jpg) e [HTML autossuficiente com as duas imagens originais](evidencias/carrossel-fidelidade/comparacao.html).
- [Aplicação nativa 1440](evidencias/carrossel-fidelidade/aplicacao-1440-nativa.jpg) e [mobile 390](evidencias/carrossel-fidelidade/mobile-390.jpg).

No protótipo em 100%, a área visível branca ocupa **1366x768**, com origem **(37,96)** no viewport 1440x960. Para evitar a comparação enganosa entre essa área e uma página útil de 1425px, a aplicação real foi carregada em iframe com conteúdo útil **1366x768** na mesma origem. O iframe mede 1381px e sua scrollbar de 15px fica fora da janela visível de 1366px; nenhum estilo dos cartões foi injetado pelo comparador. O cabeçalho e os cartões são da aplicação real integrada à API. Rolagem manual alinhou o título; não houve montagem, pintura, zoom das fotos ou edição das capturas. O comparador reduz ambas as imagens igualmente para exibi-las lado a lado.

No par calibrado, o primeiro card começa em (145,272), mede 275x396, e os seguintes começam em x=436/727/1018. As bordas medidas na referência coincidem aproximadamente com essas posições. Fontes dos elementos da aplicação foram conferidas no DOM. A referência usa canvas e não permite afirmar a família/peso/tamanho de cada camada como metadado original. A leitura de cores por JPEG também não é garantia do valor hexadecimal do arquivo de design.

Foram descartadas e refeitas capturas de comparação que pegaram recarregamento HMR, hover ou destaque transitório de hotspots. O par final foi novamente aberto e inspecionado com as duas imagens de 1440x960 carregadas.

## Funcionalidade observada nesta revisão

- 19 testes frontend aprovados, incluindo ordem sem mutação/perda de cursos, prioridade da imagem real da API e existência de seis fotografias distintas. Após o último ajuste de recorte, sete testes focados de apresentação/carrossel foram novamente aprovados. Build TypeScript/Vite final aprovado. Sem testes backend, pois não houve impacto nele.
- Desktop: seta avançou **291px** (275+16); End alcançou máximo **466px** e desabilitou a seta seguinte; Home voltou a zero e desabilitou a anterior.
- Mobile: largura real de viewport **390**, client/scrollWidth **375**, card **283x396**. Texto dos seis cards coube na altura. ArrowRight e gesto horizontal nativo simulado deslocaram **299px** (283+16); Home voltou a zero. Não é teste de swipe em dispositivo touch físico.
- Enter no primeiro card abriu **Medidas e Escalas na Metalurgia**, com dados reais. Menu retornou à seção; “Ver todos os cursos” abriu /cursos com **seis cursos**.
- Desktop nativo 1440: client/scrollWidth **1425**, seis imagens distintas carregadas. Sem overflow horizontal da página nos viewports medidos.
- Resize real de 390 para 1440: card passou de 283 para 275px; documento permaneceu sem overflow e os limites foram recalculados (anterior disabled no início, seguinte habilitada).
- Lógica de ResizeObserver, scroll-snap, foco/teclado e prefers-reduced-motion preservada. A preferência do sistema não foi alterada para um ensaio visual nesta rodada; o teste automatizado de movimento reduzido anterior continuou aprovado.

## O que permanece diferente

1. **Conteúdo:** nomes e resumos reais da API são diferentes dos exemplos do Figma; mudam quebras de linha e altura do bloco textual. Não foi inventado o curso de Estatística que aparece na terceira posição do frame. Assim, a sequência de categorias/fotografias após os dois primeiros não pode ser idêntica mantendo os seis cursos existentes.
2. **Fotos dos cursos sem equivalente:** associações ilustrativas para Comunicação e Documentação, sem afirmar fotografia original específica desses cursos no Figma. As fotos são originais do protótipo; seus recortes ainda são inferências visuais.
3. **Medidas finas/gradiente:** sem acesso às camadas não se garante CSS original, opacidades exatas, métricas tipográficas ou equivalência pixel a pixel. A comparação comprova a aproximação geométrica, não identidade total.
4. **Header/demais seções:** continuam com adaptações anteriores da demonstração e fora deste escopo. Diferenças aparecem no par completo, mas não foram ocultadas nem corrigidas aqui.
5. **Mobile:** adaptação funcional em 390px; variante móvel oficial não foi acessada. Sem dispositivo físico ou auditoria de acessibilidade integral.

Vite de revisão: http://127.0.0.1:5175/#principais-cursos. Comparador local temporário: http://127.0.0.1:5191/comparacao.html; o HTML autossuficiente acima também permite abrir a comparação posteriormente sem esse servidor. Os novos refinamentos ainda não estão no pacote ZIP ou na aplicação pública.

## Revisão após a captura humana — 24/10/2026

A comparação anterior calibrada não comprovava o alinhamento em uma janela larga: o contêiner centralizado deslocava a seção para a direita conforme o viewport aumentava. A captura humana tornou esse desvio evidente. A referência continua acessível no protótipo público em 100%; o editor e os estilos originais das camadas continuam indisponíveis sem login.

Correção restrita ao CSS da seção: margem esquerda desktop de 108px, extraída da posição relativa ao frame; padding superior reduzido de 80 para 64px; linha de ações fluida com margem direita de 108px, em lugar do limite de 1150px. Breakpoints móveis preservados. Não houve alteração do Hero, Header, conteúdo da API, fotos, entidades ou quantidade de cursos nesta revisão.

Evidências sem iframe de calibração:
- `evidencias/carrossel-fidelidade/revisao-1440-sem-calibracao.jpg`
- `evidencias/carrossel-fidelidade/revisao-lado-a-lado.jpg`
- `evidencias/carrossel-fidelidade/revisao-comparacao.html`
- `evidencias/carrossel-fidelidade/revisao-mobile-390.jpg`

O comparador usa as duas capturas inteiras, reduzidas igualmente, e explicita a margem preta do visualizador Figma. A rolagem foi ajustada manualmente para comparar a linha dos cartões; portanto essa evidência não comprova equivalência da altura do Hero. Em 1440px, o cartão da aplicação mede 275x396 e começa em x=108; o Figma começa em x=145 no screenshot, sendo 37px margem do visualizador e 108px margem interna. Em 1710px a margem da aplicação também foi observada em 108px, sem o deslocamento crescente anterior.

Validação desta revisão: dez testes focados de apresentação, movimento e limites aprovados; build TypeScript/Vite aprovado. Clique na seta desktop avançou 291px. Teclado ArrowRight mobile avançou 299px; Home foi acionado para retorno. Viewport 390: seis cartões, 283x396, clientWidth e scrollWidth iguais a 375px. Viewport 1440: clientWidth e scrollWidth iguais a 1425px. Não houve teste físico de toque ou nova execução backend.

Ressalvas mantidas: textos, categorias e ordem das fotografias diferem dos exemplos do Figma por preservar os seis cursos reais. A altura/enquadramento do Hero e a largura do Header ainda diferem e não foram alterados neste escopo. Sombras, gradiente e métricas tipográficas não foram confirmados como valores originais de camada; não se declara fidelidade integral. Vite permanece disponível em http://127.0.0.1:5175/. Sem commit, push, deploy ou regeneração do ZIP.

## Revisão da chamada final e rodapé — 24/10/2026

Referência: captura comparativa fornecida pelo usuário nesta rodada. Medidas são inferidas visualmente da captura, não extraídas de camadas do editor Figma.

Alterações: chamada final com margens desktop de 108px, coluna textual de 360px, intervalo de 44px e imagem ocupando a largura restante em 16:9; alinhamento superior e padding vertical de 64px. Título aproximado ao texto da referência. Legenda sobreposta à imagem para preservar o aviso de ausência de vídeo sem aumentar sua altura. Rodapé da Landing alinhado à margem de 108px, largura máxima 1150px e quatro colunas iguais; alterações de rodapé limitadas à página que contém a chamada final. Tablet e mobile mantêm duas/uma colunas conforme seus breakpoints.

Permanecem diferentes por finalidade demonstrativa: botão leva ao catálogo e não promete matrícula; não foi acrescentado ícone de play sem vídeo; não foram inventados contatos, cursos populares ou links de suporte. Texto de apresentação e avisos do rodapé continuam factuais. Não se declara equivalência integral ao protótipo.

Validações reais: 19 testes frontend aprovados, build TypeScript/Vite aprovado. Em 1440px: imagem 805x452,8px, sem overflow da página (clientWidth=scrollWidth=1425). Em 390px: coluna única de 335px na chamada e no rodapé, clientWidth=scrollWidth=375. Clique em Conhecer cursos abriu /cursos. Evidências: `evidencias/carrossel-fidelidade/cta-rodape-1440.jpg` e `cta-rodape-390.jpg`. Sem alterações backend, deploy, commit ou pacote ZIP.

### Complemento solicitado: efeito de vídeo e textos

A pedido explícito do usuário, a chamada passou a reproduzir o parágrafo do protótipo e o rótulo Matricule-se; o destino continua /cursos e o nome acessível informa a ausência de matrícula. Esse texto promocional é conteúdo demonstrativo do Figma, não comprovação de milhares de usuários reais. A imagem recebeu escurecimento e play central desabilitado, com identificação acessível de vídeo indisponível; a legenda foi mantida para leitores de tela. A seta agora usa o SVG oficial. Este complemento substitui a decisão anterior de não exibir play. Build aprovado e inspeção desktop registrada em `evidencias/carrossel-fidelidade/cta-video-1440.jpg`. Sem vídeo, matrícula real, commit ou deploy.

### Complemento: rodapé da referência

Textos do rodapé reproduzidos da captura fornecida: contato/endereço, Navegação, Cursos populares, Suporte e direitos reservados. Cursos populares são exclusivamente conteúdo ilustrativo do protótipo, sem alteração dos seis cursos da API. Itens sem destino são spans com aria-disabled e explicação em title; não foram criados links vazios nem páginas extras. Início, Cursos e Sobre preservam destinos reais. Removida a linha divisória que não aparece na referência. Build aprovado; inspeção em 1440 e 390 sem overflow, coluna móvel de 335px. Evidência: `evidencias/carrossel-fidelidade/footer-referencia-1440.jpg`. Sem commit ou deploy.

### Ajuste de tamanho, cor e pesos após nova comparação

CSS restrito à chamada final e ao rodapé da Landing: título desktop 40/48px, peso 700 e sem tracking negativo; parágrafo 18/26px e margem inferior 32px; botão peso 600. Título mobile 32/40px. Rodapé com texto 14/20px, cor #a79996, padding superior 48px, títulos com margem inferior 12px e bloco inferior com margem de 40px. Cursos populares ficam em uma linha apenas a partir de 1366px, sem alterar seus conteúdos.

Medição no navegador em 1440px: rodapé de 360px de altura, sem overflow horizontal. Mobile 390px também sem overflow e título de 32px. Build frontend aprovado nesta revisão; após o build, o limiar da regra de linha única foi ajustado de 801 para 1366px e verificado no navegador. Não houve repetição de testes de lógica, pois a alteração é somente CSS. As cores e medidas são aproximações fundamentadas nas capturas, não metadados extraídos do editor Figma. Evidência: `evidencias/carrossel-fidelidade/cta-footer-tipografia-1440.jpg`. Sem commit, push ou deploy.

## Alinhamento autorizado dos seis cursos — 24/10/2026

O usuário autorizou explicitamente alinhar código/seed e ambiente local ao Figma. Esta revisão substitui a ressalva anterior de conteúdo divergente. Os seis títulos e resumos foram observados no protótipo público, inclusive os dois últimos por navegação horizontal (evidência `figma-seis-cursos-final.jpg`). Ordem: Matemática para a Precisão: Medidas e Escalas na Metalurgia; Geometria e Modelagem no Design Têxtil; Estatística e Controle de Qualidade na Produção; Física do Movimento e da Energia na Metalurgia; Química e Sustentabilidade; Ciência dos Materiais na Metalurgia.

Seed e duas migrations aditivas atualizam registros por IDs, sem excluir cursos/tabelas ou reescrever a migration inicial. A primeira alinha conteúdo e módulos/aulas fictícios; a segunda associa as seis fotos oficiais pela API, mantendo catálogo, Landing e detalhes coerentes. Os módulos, aulas, descrições longas, modalidades, status e públicos-alvo continuam demonstrações, não conteúdo integral extraído do Figma. Mantidos 6 cursos, 3 categorias, 2 modalidades, 3 status, 12 módulos e 24 aulas. Os seis cursos agora usam Matemática e Ciências da Natureza; Linguagens continua como categoria válida sem cursos neste seed.

Ambiente isolado: banco novo `catalogo_figma_review`, PostgreSQL local 127.0.0.1:55432; API 127.0.0.1:5082; Vite 127.0.0.1:5175 com proxy para 5082. Credencial obtida do contêiner local somente para variáveis do processo, sem gravar ou imprimir. Nenhuma conexão Neon foi utilizada. Instâncias anteriores 5080/5173 não foram reconfiguradas. Migrations futuras em produção exigem autorização e revisão dos registros existentes: UpdateData sobrescreve os campos dos IDs do seed, embora não exclua dados.

Validação: 25 testes backend aprovados com PostgreSQL real local, inclusive migrations repetidas em schema isolado, contagens, nomes/categorias/fotos, pesquisa completa/breve, filtros e detalhes; teste Neon excluído. 19 testes frontend aprovados; builds Release/backend e TypeScript/Vite aprovados. Aviso NU1900: consulta de vulnerabilidades NuGet indisponível; não foi afirmada auditoria atualizada de dependências. Build Debug inicial falhou por DLL usada pela API anterior; Release permitiu preservar esse processo. Sem instalação de dependências.

HTTP local: health 200, seis cursos, busca por Estatística e combinação Matemática/online/disponível com um resultado. Navegador abriu /cursos/3 com o título correto de Estatística. Em 390px, os seis textos couberam nos cards 283x396, sem overflow da página; ArrowRight deslocou 299px. Seis fotos carregadas em 1440px. Cartões desktop 275x396; recortes dos IDs 3/4/5 reassociados e sombra aproximada ao protótipo.

Evidências finais: `cursos-alinhados-1440.jpg`, `cursos-alinhados-390.jpg`, `cursos-alinhados-comparacao.html` e `cursos-alinhados-comparacao.jpg` nesta pasta de evidências. Comparação usa screenshots inteiros, sem montagem dos cartões; margens do visualizador Figma permanecem visíveis. Rolagem manual alinha a linha dos cards. Restam aproximações de recorte, sombra, cor e métricas tipográficas porque as camadas do editor não estão disponíveis. Hero, Header e variante mobile oficial não são validados como idênticos por essa comparação. O cartão adicional Todos os cursos do protótipo não foi criado; a ação inferior existente continua /cursos.

Sem commit, push, deploy, alteração Neon ou recriação do ZIP. Os artefatos publicados e o pacote antigo não contêm esta revisão.

### Revisão de Impacto e depoimentos — 24/10/2026

Composição desktop aproximada da captura humana: colunas de Impacto 360px + intervalo de 32px + área restante, margens de 108px; título 40/48px sem tracking negativo, parágrafo 18/26px até 300px; indicadores peso 600, legendas 18px/peso 600 e linhas com padding vertical de 8px. Textos dos três benefícios da Landing alinhados à referência; os textos da página de detalhes foram preservados. Rótulo visível passa a Depoimentos, título 40/48px, rótulo 10/14px. Aviso de indicadores ilustrativos mantido em texto para leitores de tela; depoimentos continuam identificados como fictícios no nome acessível do trilho. Textos promocionais são demonstrativos, não comprovação de suporte/certificação oferecidos por este projeto.

Corrigida também a origem da divergência de dados na porta 5173: criado frontend/.env.development.local, ignorado pelo Git, contendo apenas API_PROXY_TARGET para a API local 5082. O navegador confirmou em localhost:5173 os seis títulos novos. Esta configuração local substitui a observação anterior de que 5173 ainda usa o catálogo antigo. Não altera banco, variável secreta, bundle de produção ou infraestrutura.

19 testes frontend e build aprovados. Fontes carregadas verificadas no navegador. Em 1440 e 390, sem overflow horizontal; linhas dos indicadores em 390 com scrollWidth=clientWidth=335. Títulos mobile 32/40px. Evidência: impacto-depoimentos-revisao-1440.jpg. Medidas e pesos aproximados da captura, sem afirmar valores originais das camadas. Sem commit, push ou deploy.

### Cartão final e hover — 24/10/2026

Solicitação explícita: maior elevação no hover e cartão Todos os cursos como no protótipo. Adicionado um link branco ao fim do trilho, sem criar um sétimo curso na API; ação inferior Ver todos os cursos preservada. Mesmas dimensões dos cards (275x396 desktop), texto em uma linha e SVG oficial. Hover/foco nos cards da Landing eleva 8px, com padding superior no trilho para evitar recorte; movimento reduzido cancela a transformação.

Sete testes focados de movimento/limites aprovados e build final aprovado. Navegador desktop: sete elementos no trilho, fim em scrollLeft=max=728 e próxima seta disabled; transform de hover observado em -8px. Clique Todos os cursos abriu /cursos. Evidência: card-todos-hover-1440.jpg. Sem alterações de dados, backend, Neon, commit ou deploy.

### Setas com percurso completo — 24/10/2026

Conforme observação do usuário no Figma, as setas da Landing agora chamam início/fim do trilho em vez de anterior/próximo cartão. Mantida rolagem nativa smooth e preferência de movimento reduzido; teclado ArrowLeft/ArrowRight mantém passos por cartão e Home/End alcançam os extremos. Não foi introduzida animação com duração arbitrária.

Sete testes focados aprovados e build aprovado. Navegador em 1440: um clique à direita mostrou posição intermediária 695px e terminou em 728px (máximo), com seta seguinte desabilitada. Evidência: carrossel-clique-fim-1440.jpg. Sem backend, banco, commit ou deploy.

### Deslizamento mais lento e seta do cartão final

A pedido do usuário, o movimento nativo foi substituído por interpolação requestAnimationFrame: 850ms para percurso início/fim, aceleração/desaceleração cúbica; 400ms para teclado entre cartões. O tempo é um ajuste visual, não duração extraída do Figma. Snap e comportamento suave nativos ficam suspensos somente durante a interpolação e são restaurados ao terminar/interromper. Pointerdown, roda e teclado no trilho cancelam a animação; desmontagem limpa o frame. Movimento reduzido vai diretamente ao destino.

SVG oficial do cartão Todos os cursos ampliado de 24 para 32px, alinhado com texto na linha inferior, gap de 6px. Seis testes focados de interpolação e destinos aprovados; build aprovado. Navegador observou posição intermediária de 76px e final de 728px, snap restaurado; interação horizontal também restaurou o snap. Seta medida em 32px. Evidência carrossel-lento-seta-1440.jpg. Não foi medido o tempo original no protótipo nem emulado movimento reduzido nesta verificação visual. Sem commit/deploy.


### Proporção superior — 24/10/2026
Desktop conferido: Hero 602px, cabeçalho 76px, botão 40px; título 72/76px, descrição 18/26px e margem de 108px. Foto com posição vertical 43%. Cards preservados em 275x396. Build aprovado e quatro testes de âncora aprovados. Ajuste final de 1px no padding do botão verificado no navegador. Título da seção após âncora em y=176, cabeçalho termina em y=76; mobile 390 sem overflow. Evidência proporcoes-hero-cursos-1440.jpg (1440x1300). Sem equivalência integral afirmada, banco, commit ou deploy.
