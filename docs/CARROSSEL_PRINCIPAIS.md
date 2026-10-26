# Refinamento — Principais cursos, 24/10/2026

**Registro histórico da tentativa anterior.** Medidas, imagens e avaliação visual abaixo foram substituídas pela revisão em [CARROSSEL_FIDELIDADE.md](CARROSSEL_FIDELIDADE.md), após nova inspeção do Figma em 100% e recuperação das fotografias do protótipo. Não utilizar esta primeira tentativa como evidência do visual atual.

Escopo restrito ao carrossel da Landing. Correção de âncoras anterior preservada. Sem alterações de API, backend, banco, seed, Hero/Header ou outras seções nesta rodada; sem dependências novas, deploy, commit/push ou recriação do ZIP.

## Referência e diferenças

Comparação visual com a captura oficial existente `evidencias/figma-cartoes-viewport.jpg`, associada ao fluxo Landing/frame 6017:342. Não houve nova inspeção interativa do Figma ou acesso ao Dev Mode nesta rodada. A captura do visualizador mostra quatro cartões e parte de outro na escala capturada; conforme orientação atual do usuário, a composição implementada mostra cinco completos e parte do sexto em 1440px. Não se declara equivalência pixel a pixel nem comparação com variante móvel oficial, que não foi verificada.

O código já possuía trilho flex/overflow, não uma grade CSS: a largura percentual de 24% e a contenção central limitavam a apresentação. Foram ajustados largura/alinhamento, proporções e controles. Cartões inteiros continuam links de detalhes e usam exclusivamente os seis resultados reais da API, sem duplicação para preencher espaço.

- Trilho flex/scroll-snap com gap 14px; sem overflow horizontal do documento.
- Desktop: cards 238x390px; primeiro cartão x=112,5px; cinco completos e trecho do sexto. Título/CTA mantêm alinhamento do conteúdo; trilho se estende à borda direita.
- Mobile: cards medidos em 283x390px, com parte do seguinte; textos cabem sem corte. Imagem cover com recortes existentes, gradiente vermelho intenso abaixo, badge no topo e texto branco na região inferior.
- “Ver todos os cursos” preservado abaixo à esquerda para /cursos. Setas oficiais SVG de 48px abaixo à direita, nomes acessíveis, disabled nativo, limites recalculados por ResizeObserver e scroll.
- Movimento calculado pelos inícios reais dos cartões e gap, limitado ao máximo rolável. ArrowLeft/ArrowRight, Home/End e ativação nativa dos botões por teclado; código usa auto em prefers-reduced-motion, smooth nos demais casos. CSS de movimento reduzido existente preservado.

## Fotografias realmente disponíveis

Três PNGs oficiais com SHA-256 distintos: banner-ava.png (1940x1080), hero-image.png (1920x1080) e video-thumb.png (1920x1080). Não há fotografias individuais oficiais equivalentes a todos os cartões do Figma. Os seis cursos continuam referenciando banner-ava.png da API; recortes CSS por ID reaproveitados, sem cópias físicas ou fotografias externas. Os outros dois assets mantêm seus usos existentes; não foram atribuídos artificialmente a cursos. Repetição visual e diferença dos títulos/descrições reais frente ao conteúdo do protótipo permanecem.

## Validação observada

Vite local em http://127.0.0.1:5175 com proxy para API existente em 5080; mantido ativo para revisão humana.

| Verificação | Resultado |
| --- | --- |
| Frontend | 16 testes aprovados, incluindo quatro novos casos de deslocamento/limites; build TypeScript/Vite aprovado. Sem novos testes backend. |
| 1440px | Documento 1425px úteis, scrollWidth 1425; trilho 1316px, conteúdo 1525px. Avanço até scrollLeft 209 (máximo), seta seguinte disabled; retorno habilitou avanço. |
| 768px | Documento 753px úteis/scrollWidth 753; trilho 732px, cartões 238px; quantidade simultânea reduzida. |
| 390px | Documento 375px úteis/scrollWidth 375; trilho 358px; cartões 283x390. Conteúdo textual dos seis cards coube na altura. |
| Teclado móvel | ArrowRight deslocou 297px (283+14); Home retornou a zero; End alcançou máximo 1437 com seguinte disabled. Ativação do botão seguinte por Space observada. |
| Gesto horizontal | Rolagem horizontal nativa simulada sobre o trilho, a partir de zero estabilizado, terminou em 297px. Não foi teste de swipe em dispositivo touch físico. |
| Resize sem overflow | Em 1920px: trilho/conteúdo 1556px e ambas as setas disabled; retorno a 1440 recalculou os limites. |
| Detalhes | Clique e Enter no cartão abriram /cursos/5 com nome/dados reais; navegação de volta pela âncora preservada. |
| Catálogo | “Ver todos os cursos” abriu /cursos, mostrando seis cursos reais. |

Capturas: [desktop 1440](evidencias/carrossel-principais/desktop-1440.jpg), [mobile 390](evidencias/carrossel-principais/mobile-390.jpg), [gesto móvel](evidencias/carrossel-principais/mobile-gesto.jpg). Sem edição/montagem. Movimento reduzido foi conferido na implementação; não houve alteração da preferência do sistema para ensaio visual. Não há teste em dispositivo físico/múltiplos motores ou medição exata do frame Figma.

Arquivos desta rodada: frontend/src/pages/Home.tsx, frontend/src/styles.css, novos frontend/src/useCourseCarousel.ts, frontend/src/carouselTarget.ts e frontend/tests/carouselTarget.test.mjs, este relatório e capturas. Outras modificações locais já existentes pertencem à correção de navegação anterior. Pacote final permanece da versão anterior e não contém estes refinamentos ainda não publicados.
