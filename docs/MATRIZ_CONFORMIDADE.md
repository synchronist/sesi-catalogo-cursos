# Matriz de conformidade — entrega SESI-SP

Fonte: `Instruções_Atividade prática_519_2026_DEV.pdf`, páginas 1–3, conferido integralmente por extração textual em 23/10/2026. C = concluído; P = parcialmente concluído/ressalva; N = não realizado. Evidências históricas não foram apresentadas como testes repetidos nesta fase. Situação final do pacote em `VALIDACAO_PACOTE.json` e `VALIDACAO_PACOTE.md`.

## Aplicação e dados (seções 1, 3 e 4a/4b)

| Requisito | Estado | Evidência/limite |
| --- | --- | --- |
| Frontend baseado no Figma | P | Interface concluída; comparação em AUDITORIA_VISUAL.md. Sem medidas Dev Mode/equivalência pixel a pixel; fotos individuais ausentes e telas complementares adaptadas. |
| Responsividade | C | Auditorias visuais em 390/768/1440; amostra pública 390/1440, sem overflow observado. Não equivale a todos os dispositivos. |
| Acessibilidade | P | Semântica, teclado/foco e estados observados; sem auditoria integral WCAG/leitor de tela. |
| Backend C#/.NET | C | backend/, .NET 10; publish Release Linux x64 nesta fase. |
| API de consulta | C | GET /api/cursos, /api/cursos/{id}, /api/catalogo/filtros. |
| Banco de código aberto | C | PostgreSQL, EF Core/Npgsql; migrations versionadas. |
| Integração frontend/API/banco | C | Neon real validado na Fase 3.4; HTTP/UI públicos na 3.7. |
| Publicação gratuita | C | URL Render publicada e Neon Free documentados; sem garantia de disponibilidade/quotas. Não houve nova contratação ou deploy nesta fase. |
| Documentação técnica | C | README, contrato, modelo, instruções do pacote e auditorias. |
| Uso registrado de IA | C | USO_DE_IA.md, com registros factuais por fase. |
| Nome completo do curso | C | Entidade Curso, DTOs e seed. |
| Nome breve | C | Entidade Curso, DTOs e seed. |
| Descrição | C | Entidade Curso, DTOs, detalhes e seed. |
| Categoria | C | Relação Categoria/Curso e filtro SQL. |
| Modalidade | C | online/semipresencial, constraint e filtro. |
| Carga horária | C | Seed de 12 a 32h; DTO/detalhes. |
| Público-alvo | C | Seed distinto por curso e detalhes. |
| Status | C | disponivel/em-breve/encerrado; constraint/filtro. |
| Imagem de apresentação | C | Caminho oficial em todos os cursos. Diversidade visual limitada, documentada separadamente. |
| Data quando aplicável | C | Data opcional no modelo, seed fixo, exibição pt-BR. |
| Mínimo seis cursos | C | Seed e homologação pública: 6. |
| Mínimo três categorias | C | Seed/Neon: 3; filtros públicos. |
| Mínimo duas modalidades | C | Seed: 2. |
| Mínimo três status | C | Seed: 3. |
| Diferentes cargas, módulos, descrições de aulas e públicos | C | CatalogoSeed: 12 módulos/24 aulas, temas e públicos distintos; contagens remotas anteriores. |
| Dados fictícios | C | Seed determinístico e textos identificados como demonstrativos. |

## Funcionalidades (seção 4c)

| Requisito | Estado | Evidência |
| --- | --- | --- |
| Listar por API | C | Homologação pública: seis cursos. |
| Pesquisar por nome | C | Medidas: um resultado, HTTP e UI. |
| Filtrar categoria | C | Matemática: dois resultados em HTTP público. |
| Filtrar modalidade | C | Online: três resultados em HTTP público. |
| Filtrar status | C | Disponível: três resultados em HTTP público. |
| Combinar busca/filtros | C | Medidas/Matemática/Online/Disponível: um, URL e UI. |
| Contagem | C | Casos 6/1/0 observados na homologação. |
| Limpar filtros | C | URL limpa e seis cursos recuperados na UI. |
| Abrir detalhes | C | Curso 1, dois módulos/quatro aulas; acordeões rechecados na 3.8. |
| Nenhum resultado | C | Busca zzzz-sem-curso, total zero e limpeza observados. |
| Carregamento | C | Auditoria 2.2: proxy com atraso de requisição real; captura/registro de loading. |
| Mensagem de erro | C | Auditoria 2.2: falha controlada do proxy e parâmetro inválido; health público 503 real registrado separadamente. |
| Diferentes tamanhos de tela | C | Evidências locais e públicas listadas acima. |
| Gerenciamento de cursos | N | CRUD não implementado; explicitamente dispensado no PDF. |
| Integração Moodle | N | Diferencial opcional no PDF; AVA funcional não implementado. |

## Documentação obrigatória (seções 4d/4e)

| Item | Estado | Localização |
| --- | --- | --- |
| IA: ferramenta | C | USO_DE_IA.md, Ferramenta e objetivo. |
| IA: objetivo | C | Objetivo e seções das fases. |
| IA: prompts relevantes | C | Prompts relevantes e aproveitamento. |
| IA: aproveitado/descartado | C | Decisões/descartes explícitos, inclusive suposições do Figma. |
| IA: validação | C | Validação realizada e evidências por fase. |
| IA: erro real e correção | C | Handler retornava 500 em vez de 503; cadeia InnerException corrigida e teste aprovado. |
| README: objetivo | C | Introdução. |
| README: tecnologias | C | Tecnologias e arquitetura. |
| README: arquitetura | C | Fluxo e responsabilidades. |
| README: pastas | C | Estrutura. |
| README: execução | C | Execução local e pacote Linux x64. |
| README: banco | C | Configurações, migrations e seed explícitos; sem segredos. |
| README: endpoints | C | Contrato da API, erros e parâmetros. |
| README: modelo | C | Modelo de dados e seed. |
| README: responsividade | C | Responsividade, acessibilidade e limites. |
| README: acessibilidade | C | Decisões sem alegação de certificação. |
| README: publicação | C | Dockerfile, Render/Neon e referência à homologação real. |
| README: limitações | C | Limites, diferenças do Figma e LIMITACOES_ENTREGA.md. |

## Entregáveis (seção 5)

| Entregável | Estado | Localização/pendência |
| --- | --- | --- |
| GitHub | C | https://github.com/synchronist/sesi-catalogo-cursos; revisão funcional 29e997b confirmada na 3.8. Documentos/evidências finais incluídos na finalização documental da Fase 5. |
| Aplicação publicada | C | https://sesi-catalogo-cursos.onrender.com; 503 transitório documentado, causa não confirmada. |
| Fontes frontend | C | frontend/, GitHub/main. |
| Fontes backend | C | backend/, GitHub/main. |
| Scripts/migrations ou banco | C | backend/Persistence/Migrations/, scripts/ e referência no pacote; dump não exigido. |
| README.md | C | Raiz; incluído na finalização documental da Fase 5. |
| USO_DE_IA.md | C | Raiz; incluído na finalização documental da Fase 5. |
| ZIP/relação dos arquivos de publicação | C | entregaveis/sesi-catalogo-publicacao-linux-x64.zip; MANIFESTO.csv, SHA256SUMS e checksum externo. Resultado da verificação em VALIDACAO_PACOTE.md/json. |
| Relação de limitações/itens não concluídos | C | LIMITACOES_ENTREGA.md, ENTREGAVEIS.md e cópia no ZIP. |
| Execução local em caso de indisponibilidade | C | README e EXECUCAO_PACOTE.md; pacote depende de runtime/banco configurados. |
| Diferenças do Figma justificadas | C | AUDITORIA_VISUAL.md, README e LIMITACOES_ENTREGA.md. |

Nenhuma funcionalidade obrigatória ausente foi identificada. Fidelidade e acessibilidade permanecem parciais nos limites declarados; opcionais ausentes não são apresentados como concluídos. A Fase 5 autoriza a finalização documental em main, sem alterar a implementação. Release/upload do ZIP e envio formal ao SESI ainda não realizados; proposta em PROPOSTA_RELEASE.md.

## Revisão final de 26/10/2026

Esta matriz histórica deve ser lida com FECHAMENTO_FINAL.md e ENTREGAVEIS.md. A implementação final acrescenta refinamentos de fidelidade, seis fotos do Figma e AVA demonstrativo. Moodle continua opcional e não integrado. Seed final 3 categorias/6 cursos/20 módulos/50 aulas; pacote final em entregaveis/final-2026-10-26/, preservando o anterior. O SHA exato consta em PUBLICACAO.json, evitando referência circular ao hash do próprio documento. Validação executada e estado efetivo do deploy estão no fechamento final; envio à comissão permanece ação do candidato.
