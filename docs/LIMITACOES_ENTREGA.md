# Limitações e itens não concluídos

Os itens abaixo registram a publicação e o pacote de entrega homologados anteriormente. A versão local de revisão já possui AVA textual sem contas, descrito em [AVA.md](AVA.md), e outras revisões documentadas; essas mudanças não foram publicadas nem incorporadas ao ZIP. No AVA local, progresso existe somente no navegador, sem matrícula, sincronização, vídeo, avaliação ou emissão de certificados. A integração Moodle permanece não implementada.

- Render Free pode hibernar; disponibilidade e tempo de resposta não são garantidos. Não foi medido um ciclo controlado de inatividade/reativação. Não houve contratação de plano pago por esta implementação.
- Primeira consulta pública da Fase 3.7 retornou health 503, seguida de 200 sem intervenção. A resposta indicou falha da sonda de conectividade; sem logs correlacionados, a causa permanece não confirmada. Não foi atribuída automaticamente a cold start. Fase 3.8 observou health 200.
- Dashboard Render com `29e997b Live` foi informado pelo usuário. Git local/GitHub main confirmados em `29e997bbf597f84a2f4e8061dbfd36d5a4e8d25c`; não há atestado automatizado do SHA executado pelo backend.
- Faltam fotografias individuais oficiais de cursos: `banner-ava.png` é reutilizada com recortes CSS. Não foram inventadas fotografias ou duplicados físicos para aparentar imagens distintas.
- Figma: sem Dev Mode/medidas exatas ou variantes móveis verificadas. Catálogo independente, busca/filtros/estados e layouts móveis são adaptações. Tipografia/assets oficiais e hierarquia servem de referência, sem equivalência pixel a pixel. Conteúdo fictício do seed tem dois módulos/quatro aulas por curso, diferente dos dez módulos do exemplo do protótipo.
- Moodle/AVA funcional não implementado; integração Moodle é diferencial opcional no enunciado. Sem autenticação, matrícula, pagamentos, aulas reproduzíveis, vídeo ou emissão de certificados. CTAs indisponíveis identificam a demonstração. Não há CRUD de cursos, dispensado pelo enunciado.
- Cursos, datas, indicadores e depoimentos são fictícios/ilustrativos. Não representam ofertas reais SESI.
- Acessibilidade verificada por semântica, foco, teclado e amostras responsivas; sem certificação WCAG, leitor de tela real ou dispositivos físicos/múltiplos motores. Sem teste de carga ou auditoria integral de vulnerabilidades.
- Contratos TypeScript não fazem validação completa de payload em runtime. Health confere conectividade, não integridade do schema.
- Pacote Linux x64 depende do ASP.NET Core Runtime 10 e PostgreSQL externo configurado pelo avaliador. Não transporta credenciais, banco remoto ou runtime autocontido. Quotas gratuitas e continuidade dos provedores são dependências externas.

## Revisão definitiva de 26/10/2026

Os registros acima descrevem o pacote/publicação anteriores. Na versão final há AVA demonstrativo com progresso local por curso, navegação livre, conclusão condicionada aos requisitos de cada aula, perguntas de revisão e prévia de 12s com imagem oficial. Não há vídeo educacional real, contas, matrícula, Moodle ou certificados. Os seis cursos possuem fotografias distintas recuperadas do Figma; não foram inventadas fotos nem reivindicada autoria dos assets. Metalurgia tem 10 módulos/30 aulas; total 20 módulos/50 aulas. Catálogo, mobile e interações adicionais do AVA são adaptações explícitas. A pausa por aba oculta continua sem comprovação manual no navegador de teste.

Pacote definitivo separado em entregaveis/final-2026-10-26/. ZIP antigo preservado, sem equivalência com o código final. Estado de banco/deploy, testes e integridade em FECHAMENTO_FINAL.md. Sem permissão geral expressa para Release público adicional de assets: entrega do pacote por canal privado da comissão. Disponibilidade gratuita sem garantia; 503 histórico de causa não confirmada; sem certificação WCAG, teste físico/leitor de tela ou auditoria exaustiva de segurança.
