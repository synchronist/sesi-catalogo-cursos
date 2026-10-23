# Limitações e itens não concluídos

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
