# Entregáveis — teste técnico SESI-SP

1. **GitHub:** https://github.com/synchronist/sesi-catalogo-cursos. Revisão funcional/fonte do ZIP: `29e997bbf597f84a2f4e8061dbfd36d5a4e8d25c`. A finalização documental da Fase 5 acrescenta documentos, evidências, scripts de entrega e exclusões Git, preservando a implementação. O hash documental é identificado no histórico de main.
2. **Aplicação pública:** https://sesi-catalogo-cursos.onrender.com.
3. **Frontend:** `frontend/`, React + TypeScript + Vite; assets oficiais em `frontend/public/assets/oficiais/`.
4. **Backend:** `backend/`, ASP.NET Core .NET 10 + EF Core/Npgsql.
5. **Migrations:** `backend/Persistence/Migrations/20261021233255_InitialCatalogo.cs`, Designer e snapshot. Seed versionado: 3 categorias, 6 cursos, 12 módulos e 24 aulas. Aplicação explícita conforme README; nunca apagar/recriar Neon. Cópias auditáveis das migrations no ZIP.
6. **README:** `README.md`, execução local, banco, API, modelo, arquitetura, publicação e limites.
7. **IA:** `USO_DE_IA.md`, ferramenta, objetivos, prompts, aproveitamentos/descartes, validações e erros/correções reais.
8. **ZIP:** `entregaveis/sesi-catalogo-publicacao-linux-x64.zip` — publish Release real, framework-dependent, Linux x64/glibc. Requer ASP.NET Core Runtime 10 e PostgreSQL externo. Checksum externo: mesmo nome com sufixo `.sha256`.
9. **Arquivos de publicação:** dentro do ZIP, `app/` (DLL/dependências/configuração sem segredo), `app/wwwroot/` (React e assets oficiais), `EXECUCAO.md`, `PUBLICACAO.json`, `docs/` (README/IA/limitações), `migrations/` (referências). Relação exaustiva com tamanho/hash em `MANIFESTO.csv`; `SHA256SUMS` cobre conteúdo + manifesto. Pasta não compactada em `entregaveis/sesi-catalogo-publicacao-linux-x64/`; extração de conferência em `entregaveis/verificacao-linux-x64/`. Evidências de validação em `docs/VALIDACAO_PACOTE.md` e JSONs associados.
10. **Limitações/itens não concluídos:** `docs/LIMITACOES_ENTREGA.md`, `docs/AUDITORIA_VISUAL.md` e `docs/HOMOLOGACAO_PUBLICA.md`. Incluem disponibilidade Render Free/503 de causa não confirmada, imagens oficiais reutilizadas, adaptações do Figma, ausência de matrícula/vídeo/AVA/Moodle e limites dos testes de acessibilidade.

## Auditoria e envio

Matriz item a item: `docs/MATRIZ_CONFORMIDADE.md`, conferida contra o PDF oficial. Instruções do pacote: `docs/EXECUCAO_PACOTE.md`. Reprodução: `scripts/Empacotar-Publicacao.ps1`; auditoria/ZIP/hash/extração: `scripts/Verificar-Pacote.py` (Python 3, biblioteca padrão, sem pacotes extras).

A pasta entregaveis está ignorada no Git: publish e ZIP são artefatos binários gerados, não fontes. Não há runtime .NET autocontido, mas há DLLs da aplicação e suas dependências. Distribuir ZIP e checksum como artefatos separados; não adicioná-los automaticamente ao histórico. Preservar fontes, lockfiles, migrations, assets e documentos no repositório.

Distribuição: GitHub Release proposto em `docs/PROPOSTA_RELEASE.md`, ainda não criado/publicado e sem upload do ZIP. Antes do envio: obter autorização para distribuir o pacote, publicar os dois artefatos conforme o procedimento e encaminhar ZIP + checksum + links ao SESI pelo canal indicado ao candidato, até 26/10/2026 às 08h (prazo informado). Commit/push documental são autorizados na Fase 5; seu resultado efetivo deve ser conferido no histórico remoto. Nenhum envio ao SESI ou comando de deploy integra esta etapa.
