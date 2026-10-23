# Validação final do pacote — Fase 4

Data: 23/10/2026. Implementação fonte: `29e997bbf597f84a2f4e8061dbfd36d5a4e8d25c`; `git diff` não identificou mudanças em backend/frontend/Dockerfile. Documentação e scripts finais ainda locais, sem commit/push. Não foi realizada nova publicação ou alteração de infraestrutura/banco Neon.

## Build e conteúdo

- `scripts/Empacotar-Publicacao.ps1`: frontend `npm --prefix frontend run build` aprovado (TypeScript e Vite); publish .NET Release aprovado, RID `linux-x64`, framework-dependent, `UseAppHost=false`, `ProductionImage=true`, sem símbolos de depuração.
- Ambiente de geração: Windows, SDK .NET **10.0.401**, Node **24.20.0**, npm **11.19.0**. Execução do alvo Linux confirmada no teste abaixo, não inferida do build Windows.
- Publish em `entregaveis/sesi-catalogo-publicacao-linux-x64/app`; inclui DLL/dependências da API e React em `wwwroot`.
- Configurações JSON publicáveis sem conexão; sem EF Design, SDK, node_modules, PDB, .env/.neon, logs, banco ou chaves privadas.
- **65 assets oficiais** comparados byte a byte com `frontend/public/assets/oficiais`, todos idênticos após a extração.
- Aviso inicial **NU1900** por falta de acesso à auditoria NuGet no sandbox. `dotnet restore backend/Sesi.Catalogo.Api.csproj -r linux-x64 -p:ProductionImage=true` repetido fora dele: aprovado, sem aviso. Isso não equivale a auditoria integral de vulnerabilidades.
- Não foram reinstaladas dependências npm, repetidas suítes unitárias completas ou modificadas entidades/migrations/seed.

## ZIP, manifesto e segurança

Artefato: `entregaveis/sesi-catalogo-publicacao-linux-x64.zip`.

- Tamanho: **16.158.146 bytes**, aproximadamente **15,41 MiB**.
- SHA-256: `d03a9b091008971a4469072e7c7c647b315cf00ee5e50b941849c035c4f22ce9`.
- **89 arquivos** no ZIP, sem duplicidade de nomes ou caminhos inseguros.
- **87 entradas** de conteúdo no MANIFESTO.csv, com tamanho e SHA-256; **88 checksums** em SHA256SUMS, incluindo o manifesto. SHA256SUMS não contém a si próprio.
- CRC de todas as entradas aprovado; bytes do ZIP iguais aos arquivos originais; extração em `entregaveis/verificacao-linux-x64` conferida arquivo a arquivo.
- Auditoria de conteúdo (incluindo DLLs) sem matches de valores privados conhecidos em UTF-8/UTF-16, padrões dirigidos de tokens/chaves privadas ou arquivos proibidos. Valores locais lidos apenas em memória, não impressos. JS sem URL localhost. Nenhuma connection string real em appsettings.
- O checksum externo está em `.zip.sha256`. A comparação novamente após os testes confirmou ZIP/manifestos inalterados.
- Limite: scanner dirigido não constitui certificação ou prova absoluta contra todo segredo/vulnerabilidade. Não foi contratado scanner externo.
- Auditoria final dos **203 arquivos candidatos ao versionamento**: nenhum match dos valores privados de conexão/senha conhecidos; nenhum .env/.env.local/.neon rastreado. Git diff --check sem erros de whitespace; apenas avisos de normalização CRLF/LF nos documentos. ZIP/publish confirmados como ignorados.

Reprodução/verificação: `scripts/Verificar-Pacote.py`, somente biblioteca padrão Python. JSON da execução: [VALIDACAO_PACOTE.json](VALIDACAO_PACOTE.json). Para conferir artefato já existente, usar o script sem `--create`; ele também confere o checksum externo. Instruções completas de geração no README.

## HTTP no conteúdo extraído

Executado **o app extraído do ZIP**, montado somente leitura em `/app`, na imagem local `mcr.microsoft.com/dotnet/aspnet:10.0`, usuário **1654:1654**, filesystem raiz somente leitura e `/tmp` temporário. Porta exposta apenas em loopback `127.0.0.1:55184`. Sem novo build Docker ou download de imagem (`--pull never`).

Conexão ao PostgreSQL local `sstp-dev-postgres-1`, sem Neon, com opção Npgsql `Options=-c default_transaction_read_only=on` para impedir escrita nas sessões da aplicação. Não foram executadas migrations, seed ou comandos de escrita. Credencial local somente no ambiente do container; ausente do pacote e do relatório. Revisão dos logs não encontrou a senha local.

`scripts/Test-Publicacao.ps1 -BaseUrl http://127.0.0.1:55184`: **15/15 aprovados**:

| Caso | Resultado |
| --- | --- |
| `/`, `/cursos`, `/cursos/1` | 200 HTML; fallback igual ao index real |
| `/health` | 200 Healthy |
| `/api/cursos` | 200, total seis |
| Pesquisa Medidas + Matemática + Online + Disponível | 200, total um |
| `/api/cursos/1` | 200, dois módulos/quatro aulas |
| Status inválido | 400 ProblemDetails |
| Curso/API/health/asset inexistentes | 404 ProblemDetails; sem fallback HTML indevido |
| Fotografia oficial, JS e CSS do index | 200 com Content-Type esperado; JS sem origem localhost |

Evidências: [HTTP](VALIDACAO_PACOTE_HTTP.json) e [metadados seguros da execução](VALIDACAO_PACOTE_EXECUCAO.json). Container temporário `sstp-pacote-fase4` encerrado e removido por `--rm`. Container/banco de desenvolvimento preservados. Não se repetiu a auditoria visual: layout não mudou. Não se testou instalação limpa do runtime em outro computador nem todas as distribuições Linux.

## Conformidade e envio

[Matriz do PDF](MATRIZ_CONFORMIDADE.md) e [inventário](../ENTREGAVEIS.md). Requisitos funcionais obrigatórios atendidos segundo as evidências acumuladas; fidelidade/acessibilidade com limites declarados, sem afirmação de equivalência pixel a pixel ou conformidade integral WCAG. Moodle/AVA, matrícula e vídeo não implementados, conforme escopo documentado. 503 público transitório mantém causa não confirmada.

**Pacote apto para entrega técnica, com as limitações documentadas.** Falta revisão/versionamento autorizado dos documentos/scripts/evidências finais e envio formal dos links, ZIP e checksum ao SESI. Binários/ZIP ficam em entregaveis, ignorados no Git; não devem ser adicionados automaticamente. Nenhum commit, push, deploy, conta externa ou cobrança foi realizado nesta fase.

## Reconferência — Fase 5, 23/10/2026

ZIP e extração da Fase 4 reconferidos pelo verificador existente, sem --create e sem nova extração: 89 arquivos, 87 itens de manifesto, 88 checksums, CRC/conteúdo/hash externo aprovados. Tamanho e SHA-256 acima preservados. Nenhum build ou teste HTTP local completo repetido. O código funcional e os arquivos de build/deploy permanecem iguais à revisão 29e997b. A finalização documental da Fase 5 não recria o pacote nem atualiza seu snapshot interno; diferenças posteriores são documentos, evidências, scripts de entrega e exclusões Git. Distribuição proposta em [PROPOSTA_RELEASE.md](PROPOSTA_RELEASE.md); Release/upload não executados. A pendência documental da conclusão histórica acima é tratada pelo commit autorizado da Fase 5; envio formal e autorização para distribuir o ZIP ainda pendentes.
