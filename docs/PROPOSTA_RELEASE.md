# Decisão final — 26/10/2026

A proposta histórica abaixo foi substituída: não publicar Release público, pois não há autorização explícita para redistribuição adicional dos assets. Preparar o ZIP final em entregaveis/final-2026-10-26/ para envio privado à comissão. O pacote antigo não representa o código final.

# Proposta de distribuição — não executada

Data: 23/10/2026. Não foi criado Release, tag, draft remoto ou upload. GitHub CLI não localizado nesta máquina; o procedimento recomendado usa o navegador autenticado, sem instalar ferramentas ou solicitar tokens.

## Identificação proposta

- Repositório: https://github.com/synchronist/sesi-catalogo-cursos.
- Tag proposta, ainda não criada: `entrega-sesi-2026-10-26`.
- Título: `Entrega do teste técnico SESI-SP — Linux x64`.
- Target: commit de finalização documental da Fase 5 em main; conferir seu SHA no histórico remoto antes de criar a tag. Ele mantém o código funcional da revisão `29e997bbf597f84a2f4e8061dbfd36d5a4e8d25c`.
- Arquivos: `entregaveis/sesi-catalogo-publicacao-linux-x64.zip` e `entregaveis/sesi-catalogo-publicacao-linux-x64.zip.sha256`.
- ZIP: 16.158.146 bytes, SHA-256 `d03a9b091008971a4469072e7c7c647b315cf00ee5e50b941849c035c4f22ce9`.

O ZIP real contém código compilado da revisão funcional acima, frontend em wwwroot, 65 assets oficiais, instruções e manifesto. A documentação interna é o snapshot da Fase 4; documentação final no GitHub é posterior. Não se declara identidade binária com o container Render nem atualização do backend pelo commit documental. Diferenças posteriores são somente documentação/evidências/scripts de entrega/.gitignore. Não é necessário recompilar ou recriar o ZIP por esse motivo.

## Uso dos materiais oficiais

O PDF fornece imagens/ícones para desenvolver a aplicação e exige frontend, GitHub, URL publicada e ZIP/relação de publicação. Distribuir esses assets **incorporados ao pacote do teste** é compatível com a finalidade descrita; as instruções não concedem licença geral expressa para redistribuição comercial/independente das fotografias ou marcas. Não apresentar os assets como autoria do candidato ou licenciá-los genericamente. Manter a licença OFL das fontes, já incluída em `app/wwwroot/assets/oficiais/_Font/OFL.txt`. Não incluir ZIP original dos materiais, PDF/DOCX do processo, credenciais ou arquivos da conta Neon. Esta análise é de aderência às instruções fornecidas, não certificação de titularidade/licença de terceiros.

## Procedimento exato, somente após nova autorização

1. Na raiz do workspace, conferir `git status --short --branch`, `git log -1 --format=%H` e `git ls-remote origin refs/heads/main`. O commit documental deve estar em main no remoto.
2. Confirmar ausência de alterações funcionais: `git diff --exit-code 29e997bbf597f84a2f4e8061dbfd36d5a4e8d25c HEAD -- frontend backend Dockerfile compose.yaml package.json package-lock.json neon.ts dotnet-tools.json`. Qualquer diferença exige revisão antes de distribuir.
3. Revalidar sem recriar: `python scripts/Verificar-Pacote.py --package entregaveis/verificacao-linux-x64 --zip entregaveis/sesi-catalogo-publicacao-linux-x64.zip`. Conferir tamanho/hash acima e presença do checksum externo. Usar Python disponível, sem instalar dependências.
4. Abrir https://github.com/synchronist/sesi-catalogo-cursos/releases/new com a conta autorizada. Se houver política de visibilidade/confidencialidade adicional do processo seletivo, respeitá-la antes de anexar arquivos.
5. Em Choose a tag, conferir que a tag proposta não existe. Criar a nova tag `entrega-sesi-2026-10-26`, selecionando como Target o commit documental confirmado (ou main somente se o SHA ainda for exatamente esse). Não mover/sobrescrever tags existentes.
6. Preencher o título acima e a descrição sugerida abaixo. Anexar **somente os dois arquivos indicados**. Os arquivos “Source code” gerados automaticamente pelo GitHub não substituem este ZIP compilado.
7. Conferir nomes, tamanho e escopo. Após a autorização de publicação, usar Publish release. Save draft também grava conteúdo remoto: não foi usado nesta rodada. Não iniciar workflow, redeploy Render ou mudança de banco.
8. Baixar os dois anexos publicados, comparar SHA-256 do ZIP com o arquivo externo e o hash deste documento; conferir que o Release aponta para o commit escolhido. Só então informar o link do Release ao SESI. Não considerar upload bem-sucedido sem essa verificação.

### Descrição sugerida

Pacote de entrega do teste técnico SESI-SP. Aplicação demonstrativa: https://sesi-catalogo-cursos.onrender.com.

Publish Release Linux x64/glibc, framework-dependent; requer ASP.NET Core Runtime 10 e PostgreSQL configurado em ambiente. Fonte funcional: 29e997bbf597f84a2f4e8061dbfd36d5a4e8d25c; a tag inclui a finalização documental posterior, sem alteração funcional. ZIP com React em wwwroot, manifesto, checksums e instruções. SHA-256: d03a9b091008971a4469072e7c7c647b315cf00ee5e50b941849c035c4f22ce9.

Dados fictícios; sem matrícula, vídeo ou AVA/Moodle funcional. Render Free pode hibernar. Houve 503 transitório de causa não confirmada, seguido de recuperação. Imagens oficiais individuais ausentes: fotografia institucional reutilizada. Consulte ENTREGAVEIS.md, docs/LIMITACOES_ENTREGA.md e docs/VALIDACAO_PACOTE.md no repositório. Materiais institucionais incluídos exclusivamente no contexto do teste, preservando a licença OFL da fonte.

## Fonte do procedimento

[Documentação oficial GitHub — gerenciamento de releases](https://docs.github.com/en/repositories/releasing-projects-on-github/managing-releases-in-a-repository), consultada em 23/10/2026. A documentação confirma seleção de tag/target, anexação de binários e publicação ou draft; disponibilidade da ação depende das permissões da conta. Nenhuma disponibilidade de upload autenticado foi testada nesta rodada.
