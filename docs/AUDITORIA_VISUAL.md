# Auditoria visual e funcional — Fase 2.1

## Resultado

Fluxo principal operacional com API e PostgreSQL reais. **Nenhum P0 observado; um P1 de fidelidade visual e três P2 de refinamento/recuperação.** Não foram realizadas correções, alterações de código, mudanças no banco/API, instalações, builds, deploy ou commits nesta rodada. As gravações ficaram exclusivamente em `docs/`.

Os resultados abaixo descrevem observações desta auditoria, sem reutilizar os testes da fase anterior como se tivessem sido executados novamente.

## Ambiente e método

- Aplicação: `http://127.0.0.1:5173/`; API: `http://localhost:5080`.
- `GET /health`: `Healthy`; `GET /api/cursos`: total 6.
- Container `sstp-dev-postgres-1`: em execução e saudável; sem reinicialização ou manipulação de dados.
- Navegador integrado, viewports de 1440×960, 768×960 e 390×960 CSS px. Não são dispositivos físicos.
- Capturas JPEG completas, da aplicação real; navegação até o final e retorno ao início antes de capturar para carregar imagens lazy. Detalhes do curso 1, seções inicialmente recolhidas.
- Dimensões dos arquivos conferidas com leitor de imagens. O navegador excluiu a barra vertical de 15 px: viewport 1440 resulta em imagem de largura 1425; 768 em 753; 390 em 375. Não houve preenchimento, recorte manual, montagem, mockup ou alteração das capturas.
- Uma captura desktop de detalhes apresentou viewport efetivo diferente do solicitado; foi descartada por sobrescrita e refeita após confirmar 1440 px. O manifesto final contém somente a versão correta.
- Todos os nove casos tiveram `scrollWidth == clientWidth`, Open Sans carregada e nenhuma imagem quebrada detectada. Isso não significa que todo texto recebeu auditoria individual.

## Nove capturas completas

| Tela | Desktop — viewport 1440 | Tablet — viewport 768 | Mobile — viewport 390 |
|---|---|---|---|
| Landing Page | [landing-desktop-1440.jpg](evidencias/landing-desktop-1440.jpg), 1425×3879 | [landing-tablet-768.jpg](evidencias/landing-tablet-768.jpg), 753×3725 | [landing-mobile-390.jpg](evidencias/landing-mobile-390.jpg), 375×4216 |
| Catálogo | [catalogo-desktop-1440.jpg](evidencias/catalogo-desktop-1440.jpg), 1425×2273 | [catalogo-tablet-768.jpg](evidencias/catalogo-tablet-768.jpg), 753×2864 | [catalogo-mobile-390.jpg](evidencias/catalogo-mobile-390.jpg), 375×4857 |
| Detalhes | [detalhes-desktop-1440.jpg](evidencias/detalhes-desktop-1440.jpg), 1425×2923 | [detalhes-tablet-768.jpg](evidencias/detalhes-tablet-768.jpg), 753×3267 | [detalhes-mobile-390.jpg](evidencias/detalhes-mobile-390.jpg), 375×3740 |

Metadados, URL, viewport, largura do documento, dimensões finais e SHA-256: [manifesto.json](evidencias/manifesto.json). As capturas foram abertas e inspecionadas. Elementos fixos aparecem na posição do viewport original dentro da imagem completa; especialmente a barra de matrícula pode sobrepor parte do corpo na captura, sem representar um segundo componente.

## Comparação com o Figma

Referência acessada nesta rodada: [protótipo oficial](https://www.figma.com/proto/YB0o91nnOEB5bXXCtmy5s6/-TEST-DEV--SESI-Ind%C3%BAstria?node-id=6017-342&starting-point-node-id=6017%3A342&page-id=3%3A2). Fluxos observados: Landing `6017:342` e detalhes `6017:707`. Não houve acesso ao Dev Mode, medidas exportadas ou variantes móveis; a comparação é visual, sem afirmação de equivalência pixel a pixel.

As quatro imagens seguintes são **recortes de viewport do visualizador Figma**, com controles e margens do próprio Figma; não são capturas completas da aplicação nem integram a matriz de nove:

- [Hero oficial](evidencias/figma-landing-viewport.jpg).
- [Cartões oficiais](evidencias/figma-cartoes-viewport.jpg).
- [Topo dos detalhes oficiais](evidencias/figma-detalhes-viewport.jpg).
- [Descrição e acordeões oficiais](evidencias/figma-acordeoes-viewport.jpg).

| Aspecto | Observação | Classificação |
|---|---|---|
| Identidade | Logotipos oficiais, vermelho/branco/preto/bege e fotografia do hero preservados. Depoimentos com fundo vermelho na inicial e bege nos detalhes. | Alinhamento geral confirmado |
| Tipografia | Open Sans local carregada; títulos fortes e textos secundários hierarquizados. Escala e pesos não foram comparados com valores do editor Figma. | Alinhamento de família; precisão numérica não verificada |
| Hierarquia | Sequência hero → cursos → impacto/benefícios → depoimentos → CTA → rodapé mantida. | Alinhamento geral confirmado |
| Cartões de destaque | A versão implementada acrescenta status, modalidade/horas, divisor e “Conhecer curso”; sua altura e densidade divergem do cartão do protótipo. | Divergência real V01 |
| Hero | A foto é a mesma, mas enquadramento/escala e tonalidade da sobreposição não coincidem visualmente. | Divergência de refinamento V03 |
| Imagens dos cursos | Mesma foto `banner-ava.png` nos seis cursos; o Figma usa imagens distintas. Sem imagens deformadas nos trechos inspecionados; há recortes por `object-fit: cover`. | Limitação de assets, não falha de integração |
| Catálogo | Composição própria com pesquisa, filtros e grade. Não foi encontrada referência independente equivalente no protótipo disponibilizado. | Adaptação necessária; sem pontuação de fidelidade direta |
| Mobile/tablet | Menu recolhível, empilhamento e grades fluidas funcionaram. Não há variante móvel confirmada do Figma para comparação direta. | Adaptação necessária |
| Detalhes | Categoria/título à esquerda, imagem à direita no desktop, benefícios, descrição/acordeões, depoimentos e barra fixa preservam a estrutura geral. | Alinhamento estrutural confirmado |
| Conteúdo dos detalhes | Nome/data refletem o seed; dois módulos/quatro aulas, metadados extras e FAQ demonstrativa. “Modalidade e público-alvo” substitui a seção de aulas/avaliações do Figma. | Adaptação de dados/escopo, não erro funcional |
| CTA, vídeo e rodapé | Sem play funcional, matrícula ou área do aluno. Botões indisponíveis estão desabilitados e identificados. Rodapé direciona somente a rotas existentes; redes são imagens decorativas. | Adaptação necessária para não prometer funções inexistentes |

## Verificações manuais nesta rodada

Pesquisa e seleção foram feitas pelos controles da interface, com consultas à API real. Registro: [testes-funcionais.json](evidencias/testes-funcionais.json).

| Caso | Resultado observado |
|---|---|
| Pesquisa por nome completo: `Medidas e Escalas` | 1 cartão; quantidade 1 |
| Pesquisa por nome breve: `Química Industrial` | 1 cartão; quantidade 1 |
| Categoria Matemática, isolada | 2 cartões; quantidade 2 |
| Modalidade Online, isolada | 3 cartões; quantidade 3 |
| Status Encerrado, isolado | 1 cartão; quantidade 1 |
| Medidas + Matemática + Online + Disponível | 1 cartão; quatro parâmetros refletidos na URL |
| Busca `sem-curso-xyz` | Zero cartões, contagem zero e mensagem “Nenhum curso encontrado” |
| Limpar busca e filtros | URL `/cursos`, campos vazios e retorno dos seis cursos |
| Navegar pelo cartão de Medidas | `/cursos/1`, título e conteúdo correspondentes |
| Enter no primeiro acordeão | Expandiu dois módulos e quatro aulas reais; foco em `SUMMARY`, outline azul sólido de 3 px |
| Espaço no mesmo acordeão | Recolheu o conteúdo |
| Menu em 390 px + Tab | Menu abriu e foco alcançou “Início”, com outline azul sólido de 3 px |
| Escape no menu | Menu fechou e devolveu foco ao botão Menu |
| URL com `status=invalido` | Interface de erro “Revise os filtros informados”; consulta não mascarada com cursos locais |
| “Tentar novamente” no filtro inválido | Repetiu a mesma URL e o mesmo erro; F01 |
| Loading transitório | Tentativa por recarga não o capturou: a resposta chegou antes da observação. **Não confirmado visualmente nesta rodada.** |
| Falha de conexão/503 | **Não induzida nem testada nesta rodada**, para preservar API/banco em execução. O erro de validação acima não é evidência de tratamento de indisponibilidade. |

Não foram repetidos nesta auditoria os testes automatizados, todos os links de rodapé, cada botão de carrossel em cada breakpoint, nem todos os cursos individualmente. Recarga/Voltar com filtros não receberam um novo teste completo de persistência nesta rodada; a URL aplicada foi observada, sem reaproveitar resultados históricos como evidência nova.

## Problemas priorizados e correções sugeridas

### V01 — P1 — densidade e proporção dos cartões da Landing divergem do protótipo

**Evidência:** [Landing desktop](evidencias/landing-desktop-1440.jpg) versus [cartões Figma](evidencias/figma-cartoes-viewport.jpg). Os cartões oficiais enfatizam imagem/categoria/título/descrição. Na aplicação, status, metadados, divisor e outra chamada visual ocupam espaço adicional. A medição em execução registrou aproximadamente 467 px de altura do cartão implementado em viewport 1440; não se atribui uma altura CSS exata ao Figma a partir do visualizador escalado.

**Impacto:** reduz fidelidade da composição principal e aumenta sua densidade. Classificação P1 de fidelidade, **não bloqueio do catálogo**.

**Sugestão:** aproximar somente a variante de destaque da estrutura observada; manter informações completas na grade do catálogo e nos detalhes. Ajustar altura, gradiente e proporção usando a referência. Não remover dados da API nem inventar fotografias.

### V02 — P2 — controles de depoimentos ativos sem conteúdo para rolar

**Reprodução:** Landing em 1440 px, clicar “Próximos depoimentos”. `clientWidth=1206`, `scrollWidth=1206`, `scrollLeft=0` antes/depois e botão habilitado. Todos os quatro depoimentos já cabem no contêiner.

**Evidência:** [controle-depoimentos-desktop.jpg](evidencias/controle-depoimentos-desktop.jpg) e registro funcional.

**Impacto:** aparência de ação disponível sem mudança visível nesse viewport.

**Sugestão:** ocultar ou desabilitar controles quando não existir overflow e nos limites inicial/final. Revalidar teclado e mudanças de largura.

### F01 — P2 — ação principal de recuperação repete filtro inválido

**Reprodução:** abrir `/cursos?status=invalido` e acionar “Tentar novamente”. A mesma consulta inválida e o mesmo erro permanecem. “Limpar filtros” funciona, mas está separado da mensagem que oferece nova tentativa.

**Evidência:** [estado-erro-validacao.jpg](evidencias/estado-erro-validacao.jpg) e registro funcional.

**Impacto:** recuperação pouco direta; não impede retornar aos seis cursos pela limpeza.

**Sugestão:** para validação, oferecer “Limpar filtros” ou foco no campo inválido como ação principal. Reservar nova tentativa para falhas transitórias.

### V03 — P2 — enquadramento e sobreposição do hero diferem da referência

**Evidência:** [Landing desktop](evidencias/landing-desktop-1440.jpg) e [hero Figma](evidencias/figma-landing-viewport.jpg). A posição/escala do profissional e o balanço fotografia/vermelho não coincidem visualmente.

**Impacto:** refinamento da fidelidade visual; não compromete leitura ou navegação observadas.

**Sugestão:** calibrar `background-position`, altura e opacidade do gradiente em desktop, preservando contraste e composição móvel. Validar lado a lado; não prometer equivalência numérica sem medidas do editor.

## Contraste, semântica e integridade visual

- Amostra quantitativa de cores sólidas da Landing: descrição de benefício 7,00:1; depoimento 6,02:1; categoria Linguagens 15,45:1; texto de rodapé 13,81:1. Nenhuma dessas amostras ficou abaixo de 4,5:1. [Dados calculados a partir dos estilos em execução](evidencias/verificacoes-visuais.json).
- Essa amostragem **não cobre** todos os estados, componentes ou contraste sobre fotografia/gradientes. Não constitui auditoria WCAG completa.
- Na Landing observada: um H1, nenhum ID duplicado, nenhuma imagem sem atributo alt e nenhum link com href vazio ou apenas `#`. Isso não equivale a acionar todos os destinos.
- Foco visível e uso de Enter/Espaço/Escape confirmados nos controles descritos. [Captura de foco no menu](evidencias/foco-menu-mobile.jpg).
- Não foram identificados textos essenciais cortados, imagens esticadas, desalinhamento impeditivo ou overflow do documento nas nove capturas. Cartões parcialmente visíveis nas bordas pertencem aos carrosséis intencionais, com rolagem própria.
- A barra de matrícula fixa recobre uma faixa do conteúdo durante a rolagem e aparece dentro do corpo nas imagens completas. Esse comportamento também existe na referência. Não foi comprovado bloqueio permanente de leitura nem falha de foco causada por ela; não foi classificada como P0/P1 a partir do artefato de captura.
- Curso, modalidade, status e horas são legíveis; fotos repetidas e conteúdo ilustrativo são limitações já explicitadas. A área do aluno e matrícula estão desabilitadas e rotuladas, não simuladas como funcionais.
- O console acessível pela ferramenta não retornou entradas error/warn durante a amostra consultada: [console.json](evidencias/console.json). Isso não prova ausência de todos os erros possíveis, eventos de rede ou problemas em outros navegadores.

## Pendências e recomendação

1. Submeter V01 à aprovação antes de alterar a variante de destaque.
2. Corrigir V02 e F01; depois calibrar V03, sem expandir funcionalidades.
3. Completar observação de loading e indisponibilidade em ambiente de teste controlado, sem modificar o banco nem interromper serviços compartilhados.
4. Fazer auditoria completa de acessibilidade/contraste, leitor de tela e dispositivos/motores adicionais antes da entrega. Não foram instaladas ferramentas para isso nesta rodada.
5. Obter imagens oficiais individuais, se disponíveis; catálogo e mobile devem permanecer documentados como adaptações. Não são divergências a corrigir pela invenção de assets ou de funcionalidades fora de escopo.

Nenhuma correção foi aplicada na Fase 2.1. Os registros acima preservam o estado daquela auditoria; a resolução posterior está abaixo.

## Fase 2.2 — correções e regressão — 21/10/2026

Alterações limitadas ao frontend. API, banco, contrato, entidades, seed e migrations preservados. Referências comparadas: `figma-cartoes-viewport.jpg` e `figma-landing-viewport.jpg`, já capturadas na auditoria. Não houve nova medição no editor Figma nem declaração de equivalência pixel a pixel.

| Item | Resolução observada | Evidência nova |
| --- | --- | --- |
| V01 — P1 | Destaques com categoria, título e resumo, sem status/metadados/divisor/CTA interno. Cartões medidos em 288 × 390 px no desktop; tipografia, espaçamento e gradiente aproximados da referência. Catálogo mantém todos os metadados e o cartão inteiro continua navegável. | [Cartões](evidencias/fase-2-2/cartoes-desktop-1440.jpg) |
| V02 — P2 | `disabled` nativo e aparência atenuada, conforme limites de rolagem; atualização por scroll e ResizeObserver. Em 1440, ambas desabilitadas; em 390, início `[true,false]`, meio `[false,false]`, fim `[false,true]`. Medida final: scrollLeft 851, largura 341, conteúdo 1192. Corrigido também encaixe inicial de 3 px com scroll-padding. | [Fim móvel](evidencias/fase-2-2/depoimentos-fim-mobile.jpg), [medições](evidencias/fase-2-2/verificacoes.json) |
| F01 — P2 | Erro 400 continua sendo erro. A ação principal agora é “Limpar filtros e ver cursos”; acioná-la removeu status inválido da URL e recuperou seis cursos. Falhas de conexão mantêm “Tentar novamente”. | [Filtro inválido](evidencias/fase-2-2/filtro-invalido-1440.jpg) |
| V03 — P2 | Hero desktop com 640 px, fotografia ampliada e personagem à direita, sobreposição vermelha recalibrada. Regras móveis preservadas. Refinamento visual concluído, sem prometer medidas exatas do Figma. | [Hero](evidencias/fase-2-2/hero-desktop-1440.jpg) |

### Assets e limitações mantidas

As três fotografias PNG oficiais (`hero-image.png`, `banner-ava.png`, `video-thumb.png`) têm SHA-256 distintos. O hero retrata um profissional; banner-ava, medição de peça; video-thumb, profissionais no ambiente industrial. Não existem nesse conjunto as outras fotografias específicas dos cartões do Figma. Os seis cursos continuam usando a imagem informada pela API; três recortes CSS por ID variam posição/escala, sem esticar, duplicar arquivos ou atribuir fotografias inexistentes aos cursos. Isso reduz a uniformidade dos destaques, mas **não elimina a repetição**, sobretudo nas miniaturas largas do catálogo. Obter fotos oficiais individuais permanece uma melhoria pendente.

### Estados e integração realmente observados

Instância Vite temporária em 127.0.0.1:5174 e proxy descartável em 5081 encaminharam GETs para a API real em 5080, com atraso artificial de 2500 ms. Nenhum payload foi inventado. Essas instâncias foram encerradas ao terminar; a aplicação principal 5173 e a API não foram interrompidas.

- **Loading:** texto, spinner, aria-busy e selects indisponíveis observados antes das respostas reais 200. [Captura](evidencias/fase-2-2/carregamento-controlado.jpg) e [árvore acessível](evidencias/fase-2-2/carregamento-controlado.txt).
- **Vazio:** busca `zzzz-sem-curso` retornou 200 com nenhum resultado; UI mostrou orientação e limpeza. [Captura](evidencias/fase-2-2/estado-vazio.jpg).
- **Inexistente:** `/api/cursos/999999` retornou 404; UI mostrou “Este curso não está no catálogo”. [Captura](evidencias/fase-2-2/curso-inexistente.jpg).
- **Erro:** interrompido somente o proxy de teste. Vite recebeu ECONNREFUSED e respondeu com erro de proxy; UI exibiu falha de catálogo/filtros e ações de nova tentativa. **Não foi um 503 emitido pela API e não comprova comportamento de um provedor em produção.** [Captura](evidencias/fase-2-2/erro-conexao-controlado.jpg).
- **Recuperação:** proxy reiniciado; “Tentar novamente” recuperou seis cursos e “Recarregar filtros” voltou a consultar o endpoint de opções, com respostas 200 registradas no proxy.
- **Regressão:** Medidas + Matemática + Online + Disponível retornou um curso, URL com os quatro parâmetros; navegação ao detalhe e expansão por Enter exibiram dois módulos/quatro aulas. Menu móvel abriu, Escape fechou e devolveu foco ao botão.

### Verificação e capturas finais

- Oito testes frontend aprovados (cinco anteriores e três de limites/arrendondamento da rolagem); TypeScript sem erros. Build frontend aprovado. Backend sem impacto: sua suíte não foi reexecutada nesta rodada.
- Landing, catálogo e detalhes conferidos em 390 e 1440; documento sem overflow horizontal nas seis medições. Nenhuma imagem falhou após carregamento na amostra móvel e na Landing desktop. Console consultado na aplicação principal: nenhuma entrada error/warn nessa amostra.
- Capturas completas em `docs/evidencias/fase-2-2/`: `landing-desktop-1440.jpg`, `landing-mobile-390.jpg`, `catalogo-desktop-1440.jpg`, `catalogo-mobile-390.jpg`, `detalhes-desktop-1440.jpg`, `detalhes-mobile-390.jpg`. Detalhes foram capturados com módulos abertos. Largura útil exclui scrollbar de 15 px. Evidências anteriores permanecem intactas.
- Não foram refeitos tablet, múltiplos motores, dispositivos físicos, leitor de tela ou auditoria integral WCAG. Capturas completas podem posicionar a barra fixa de detalhes dentro do corpo: não são montagens.

Os quatro itens desta rodada foram tratados; nenhum novo P0/P1 foi identificado na amostra. Permanecem limitações de assets e fidelidade interpretativa, além da preparação/configuração de produção e validação no provedor antes de uma primeira publicação. Não houve deploy ou commits.
