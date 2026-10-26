# Fechamento da entrega SESI-SP — 26/10/2026

Referências: PDF oficial de três páginas e e-mail do processo relidos integralmente. Entrega por GitHub e aplicação publicada; PDF também pede ZIP de publicação, README, USO_DE_IA, migrations e limitações. Moodle é opcional. Prazo 26/10 às 08h de Brasília. Nenhuma submissão/e-mail foi enviado automaticamente.

## Congelamento e segurança

Preservados Landing, catálogo, detalhes, pesquisa/filtros, carrossel, navegação, AVA demonstrativo e CTA Acessar curso. Sem novos recursos ou ajustes cosméticos nesta missão. Auditoria inicial: 311 arquivos candidatos, 156 arquivos textuais; nenhum arquivo privado proibido, valor privado conhecido ou padrão forte de credencial encontrado. Arquivos .env/.neon, caches, node_modules, bin/obj, artifacts e entregaveis permanecem excluídos. Não equivale a auditoria exaustiva de segurança.

## Validação essencial

- Frontend: build aprovado e 35 testes aprovados em uma rodada final.
- Backend: 24 testes essenciais aprovados, sem usar testes mutáveis contra produção. Aviso NU1900: consulta ao feed de vulnerabilidades indisponível; não constitui aprovação de auditoria de dependências.
- Revisões visuais anteriores em 1440/390 e a revisão imediatamente anterior de /ava/aula/CTA estão em docs/evidencias/ava/acabamento-*. Sem nova alteração visual nesta missão.
- Erro operacional real: psql do container não tinha a cadeia de CAs necessária. Nenhum TLS foi relaxado. Consulta Neon realizada com o Npgsql do projeto e VerifyFull, usando o repositório confiável do Windows. Um erro do adaptador PowerShell ao atribuir ConnectionString foi corrigido apenas no helper temporário ignorado, usando o setter .NET; não houve alteração da aplicação.

## Banco e publicação

Projeto empty-tooth-58563543, organização org-noisy-tree-27200340, branch production/br-lively-sun-b6s1tndm e banco neondb confirmados pela CLI/contexto e consulta Npgsql TLS. Histórico remoto inicial: somente InitialCatalogo, 3 categorias, 6 cursos, 12 módulos e 24 aulas. Esquema das quatro entidades compatível com o código.

SQL idempotente das três migrations pendentes revisado: 55 updates, 37 inserts, sem DROP/DELETE/TRUNCATE/ALTER. Atualiza somente dados fictícios do seed e acrescenta o programa de Metalurgia. Down não será executado. As imagens serão alteradas somente após o novo frontend estar disponível, evitando referenciar assets ausentes no deploy anterior. Resultado real da aplicação e da homologação será acrescentado após execução.

Render existente confirmado como Docker Free e conectado a synchronist/sesi-catalogo-cursos/main, com Auto-Deploy. No início desta missão, Live c7b909be88079117e7ade06d5cc45a5ab7a2e459. Não criada infraestrutura nova, plano ou conta. A confirmação de SHA provém do Dashboard; o backend não expõe SHA próprio.

## Distribuição

As instruções permitem usar os assets no teste, mas não concedem autorização geral de redistribuição em Release público. Por cautela, não publicar Release com assets adicionais: ZIP e checksum serão preparados para envio privado à comissão. Fontes/marcas atribuídas, licença OFL preservada. O pacote anterior permanece preservado e não corresponde à implementação final; saída final separada em entregaveis/final-2026-10-26/.

Limites: Moodle não integrado, matrícula/contas/certificados não implementados; progresso do AVA somente local, perguntas demonstrativas e imagem temporizada de 12s sem áudio, sem vídeo educacional real. Desktop segue frame observado; catálogo, seleção de cursos, atividades AVA e mobile são adaptações documentadas. Pausa por aba oculta implementada, mas não comprovada no navegador de teste. Cold start/hibernação Render Free e 503 histórico de causa não confirmada. Fontes de evidência históricas não são testes repetidos nesta missão.
