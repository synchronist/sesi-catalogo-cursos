# Entregáveis — teste técnico SESI-SP

1. **GitHub:** https://github.com/synchronist/sesi-catalogo-cursos — branch main. O SHA exato do pacote consta em PUBLICACAO.json; conferir main e a evidência do Dashboard no fechamento final.
2. **Aplicação:** https://sesi-catalogo-cursos.onrender.com.
3. **Frontend:** frontend/, React + TypeScript + Vite, Landing, catálogo, detalhes e AVA demonstrativo. Imagens oficiais fornecidas e seis fotografias recuperadas do próprio Figma; licença OFL da fonte preservada.
4. **Backend:** backend/, ASP.NET Core .NET 10, EF Core 10 e Npgsql/PostgreSQL.
5. **Migrations:** backend/Persistence/Migrations/, quatro migrations. Seed final: 3 categorias, 6 cursos, 20 módulos e 50 aulas. Aplicação explícita após revisão de SQL/destino/histórico; sem seed ou migration automática no startup.
6. **README:** README.md — arquitetura, execução local, banco, endpoints, responsividade/acessibilidade e publicação.
7. **IA:** USO_DE_IA.md — ferramenta, prompts, aproveitamentos/descartes, validações e erros/correções reais.
8. **ZIP final:** entregaveis/final-2026-10-26/sesi-catalogo-publicacao-linux-x64.zip e .zip.sha256. O pacote anterior na raiz de entregaveis/ é histórico e NÃO corresponde à revisão final. Estado real da geração/verificação em docs/FECHAMENTO_FINAL.md.
9. **Relação de publicação:** MANIFESTO.csv, SHA256SUMS e PUBLICACAO.json dentro do ZIP. Conteúdo: app/ com DLL/dependências, app/wwwroot/ com React/assets, EXECUCAO.md, docs/ e migrations/. Linux x64/glibc, framework-dependent; requer ASP.NET Core Runtime 10 e PostgreSQL externo. Nunca contém senha, .env, .neon, banco ou node_modules.
10. **Limitações:** docs/LIMITACOES_ENTREGA.md, docs/AVA.md e docs/FECHAMENTO_FINAL.md; matriz em docs/MATRIZ_CONFORMIDADE.md. Moodle não integrado; sem contas, matrícula, vídeo educacional real ou certificado. AVA com progresso local, perguntas demonstrativas e prévia com imagem temporizada.

## Envio

O e-mail oficial indica GitHub e aplicação publicada; o PDF exige também ZIP de publicação e limitações. ZIP/checksum preparados para envio privado à comissão: não há autorização geral expressa para redistribuição pública adicional dos assets em Release. Nenhum Release público ou e-mail automático será enviado nesta missão. O candidato deve encaminhar links e anexos pelo canal oficial, até 26/10/2026 às 08h de Brasília. Submissão só estará concluída após esse envio pelo candidato.

Código, migrations, assets necessários, testes e documentação são versionados. Binários e ZIP ficam como artefatos separados, ignorados pelo Git. Não confundir SHA do pacote com confirmação automática de SHA do backend: o deploy é confirmado documentalmente pelo Dashboard Render.
