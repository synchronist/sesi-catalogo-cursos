using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sesi.Catalogo.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlignFeaturedCoursesWithFigma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 111,
                column: "Descricao",
                value: "Identifique os conceitos de unidades e escalas e relacione-os a grandezas, proporções e escalas na metalurgia.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 112,
                column: "Descricao",
                value: "Analise um caso fictício de grandezas, proporções e escalas na metalurgia, registre sua solução e explique os critérios utilizados.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 121,
                column: "Descricao",
                value: "Identifique os conceitos de medição no chão de fábrica e relacione-os a grandezas, proporções e escalas na metalurgia.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 122,
                column: "Descricao",
                value: "Analise um caso fictício de grandezas, proporções e escalas na metalurgia, registre sua solução e explique os critérios utilizados.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 211,
                column: "Descricao",
                value: "Identifique os conceitos de formas e simetria e relacione-os a formas geométricas, simetria e padrões em tecidos.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 212,
                column: "Descricao",
                value: "Analise um caso fictício de formas geométricas, simetria e padrões em tecidos, registre sua solução e explique os critérios utilizados.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 221,
                column: "Descricao",
                value: "Identifique os conceitos de planejamento de cortes e relacione-os a formas geométricas, simetria e padrões em tecidos.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 222,
                column: "Descricao",
                value: "Analise um caso fictício de formas geométricas, simetria e padrões em tecidos, registre sua solução e explique os critérios utilizados.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 311,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de dados e indicadores e relacione-os a leitura de dados e controle estatístico da qualidade.", "Dados e indicadores: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 312,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de leitura de dados e controle estatístico da qualidade, registre sua solução e explique os critérios utilizados.", "Dados e indicadores: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 321,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de controle estatístico da qualidade e relacione-os a leitura de dados e controle estatístico da qualidade.", "Controle estatístico da qualidade: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 322,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de leitura de dados e controle estatístico da qualidade, registre sua solução e explique os critérios utilizados.", "Controle estatístico da qualidade: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 411,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de forças e movimento e relacione-os a forças, atrito, calor e energia em máquinas.", "Forças e movimento: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 412,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de forças, atrito, calor e energia em máquinas, registre sua solução e explique os critérios utilizados.", "Forças e movimento: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 421,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de calor e energia nos processos e relacione-os a forças, atrito, calor e energia em máquinas.", "Calor e energia nos processos: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 422,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de forças, atrito, calor e energia em máquinas, registre sua solução e explique os critérios utilizados.", "Calor e energia nos processos: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 511,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de reações e soluções e relacione-os a reações químicas na tinturaria e no acabamento de tecidos.", "Reações e soluções: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 512,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de reações químicas na tinturaria e no acabamento de tecidos, registre sua solução e explique os critérios utilizados.", "Reações e soluções: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 521,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de tinturaria e sustentabilidade e relacione-os a reações químicas na tinturaria e no acabamento de tecidos.", "Tinturaria e sustentabilidade: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 522,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de reações químicas na tinturaria e no acabamento de tecidos, registre sua solução e explique os critérios utilizados.", "Tinturaria e sustentabilidade: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 611,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de estrutura dos metais e relacione-os a estrutura, resistência e propriedades dos metais.", "Estrutura dos metais: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 612,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de estrutura, resistência e propriedades dos metais, registre sua solução e explique os critérios utilizados.", "Estrutura dos metais: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 621,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de resistência e seleção de materiais e relacione-os a estrutura, resistência e propriedades dos metais.", "Resistência e seleção de materiais: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 622,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de estrutura, resistência e propriedades dos metais, registre sua solução e explique os critérios utilizados.", "Resistência e seleção de materiais: situação prática" });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Descricao", "NomeCompleto", "Resumo" },
                values: new object[] { "Curso fictício de matemática para a precisão: medidas e escalas na metalurgia, voltado a grandezas, proporções e escalas na metalurgia. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "Matemática para a Precisão: Medidas e Escalas na Metalurgia", "Aplicação de grandezas, proporções e escalas em processos de corte, furação e usinagem." });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Descricao", "NomeCompleto", "Resumo" },
                values: new object[] { "Curso fictício de geometria e modelagem no design têxtil, voltado a formas geométricas, simetria e padrões em tecidos. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "Geometria e Modelagem no Design Têxtil", "Explora formas geométricas, simetria e padrões matemáticos presentes em tecidos e estampas." });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoriaId", "Descricao", "NomeBreve", "NomeCompleto", "PublicoAlvo", "Resumo" },
                values: new object[] { 1, "Curso fictício de estatística e controle de qualidade na produção, voltado a leitura de dados e controle estatístico da qualidade. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "Estatística na Produção", "Estatística e Controle de Qualidade na Produção", "Inspetores de qualidade e estudantes de processos industriais", "Leitura e interpretação de dados de controle de qualidade e produtividade." });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Descricao", "NomeBreve", "NomeCompleto", "PublicoAlvo", "Resumo" },
                values: new object[] { "Curso fictício de física do movimento e da energia na metalurgia, voltado a forças, atrito, calor e energia em máquinas. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "Física na Metalurgia", "Física do Movimento e da Energia na Metalurgia", "Auxiliares de manutenção e estudantes de mecânica", "Estudo de forças, atrito, calor e energia em máquinas, fornos e prensas." });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoriaId", "Descricao", "NomeBreve", "NomeCompleto", "PublicoAlvo", "Resumo" },
                values: new object[] { 2, "Curso fictício de química e sustentabilidade, voltado a reações químicas na tinturaria e no acabamento de tecidos. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "Química Sustentável", "Química e Sustentabilidade", "Técnicos de laboratório e profissionais do acabamento têxtil", "Estudo das reações químicas envolvidas na tinturaria e acabamento de tecidos." });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoriaId", "Descricao", "NomeBreve", "NomeCompleto", "PublicoAlvo", "Resumo" },
                values: new object[] { 2, "Curso fictício de ciência dos materiais na metalurgia, voltado a estrutura, resistência e propriedades dos metais. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "Materiais na Metalurgia", "Ciência dos Materiais na Metalurgia", "Aprendizes de metalurgia e estudantes de materiais", "Conceitos básicos de estrutura, resistência e propriedades dos metais." });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 11,
                column: "Descricao",
                value: "Estudo de unidades e escalas no contexto de grandezas, proporções e escalas na metalurgia.");

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 12,
                column: "Descricao",
                value: "Estudo de medição no chão de fábrica no contexto de grandezas, proporções e escalas na metalurgia.");

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 21,
                column: "Descricao",
                value: "Estudo de formas e simetria no contexto de formas geométricas, simetria e padrões em tecidos.");

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 22,
                column: "Descricao",
                value: "Estudo de planejamento de cortes no contexto de formas geométricas, simetria e padrões em tecidos.");

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de dados e indicadores no contexto de leitura de dados e controle estatístico da qualidade.", "Dados e indicadores" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de controle estatístico da qualidade no contexto de leitura de dados e controle estatístico da qualidade.", "Controle estatístico da qualidade" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de forças e movimento no contexto de forças, atrito, calor e energia em máquinas.", "Forças e movimento" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de calor e energia nos processos no contexto de forças, atrito, calor e energia em máquinas.", "Calor e energia nos processos" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de reações e soluções no contexto de reações químicas na tinturaria e no acabamento de tecidos.", "Reações e soluções" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de tinturaria e sustentabilidade no contexto de reações químicas na tinturaria e no acabamento de tecidos.", "Tinturaria e sustentabilidade" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de estrutura dos metais no contexto de estrutura, resistência e propriedades dos metais.", "Estrutura dos metais" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de resistência e seleção de materiais no contexto de estrutura, resistência e propriedades dos metais.", "Resistência e seleção de materiais" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 111,
                column: "Descricao",
                value: "Identifique os conceitos de unidades e escalas e relacione-os a medição de peças e interpretação de escalas.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 112,
                column: "Descricao",
                value: "Analise um caso fictício de medição de peças e interpretação de escalas, registre sua solução e explique os critérios utilizados.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 121,
                column: "Descricao",
                value: "Identifique os conceitos de medição no chão de fábrica e relacione-os a medição de peças e interpretação de escalas.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 122,
                column: "Descricao",
                value: "Analise um caso fictício de medição de peças e interpretação de escalas, registre sua solução e explique os critérios utilizados.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 211,
                column: "Descricao",
                value: "Identifique os conceitos de formas e simetria e relacione-os a formas geométricas, simetria e aproveitamento de tecidos.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 212,
                column: "Descricao",
                value: "Analise um caso fictício de formas geométricas, simetria e aproveitamento de tecidos, registre sua solução e explique os critérios utilizados.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 221,
                column: "Descricao",
                value: "Identifique os conceitos de planejamento de cortes e relacione-os a formas geométricas, simetria e aproveitamento de tecidos.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 222,
                column: "Descricao",
                value: "Analise um caso fictício de formas geométricas, simetria e aproveitamento de tecidos, registre sua solução e explique os critérios utilizados.");

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 311,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de forças e movimento e relacione-os a forças, movimento e transformação de energia em máquinas.", "Forças e movimento: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 312,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de forças, movimento e transformação de energia em máquinas, registre sua solução e explique os critérios utilizados.", "Forças e movimento: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 321,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de eficiência energética e relacione-os a forças, movimento e transformação de energia em máquinas.", "Eficiência energética: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 322,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de forças, movimento e transformação de energia em máquinas, registre sua solução e explique os critérios utilizados.", "Eficiência energética: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 411,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de propriedades dos materiais e relacione-os a propriedades químicas, corrosão e seleção de materiais.", "Propriedades dos materiais: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 412,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de propriedades químicas, corrosão e seleção de materiais, registre sua solução e explique os critérios utilizados.", "Propriedades dos materiais: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 421,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de prevenção da corrosão e relacione-os a propriedades químicas, corrosão e seleção de materiais.", "Prevenção da corrosão: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 422,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de propriedades químicas, corrosão e seleção de materiais, registre sua solução e explique os critérios utilizados.", "Prevenção da corrosão: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 511,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de clareza e organização e relacione-os a comunicação clara de procedimentos e ocorrências de produção.", "Clareza e organização: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 512,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de comunicação clara de procedimentos e ocorrências de produção, registre sua solução e explique os critérios utilizados.", "Clareza e organização: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 521,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de relatos de ocorrências e relacione-os a comunicação clara de procedimentos e ocorrências de produção.", "Relatos de ocorrências: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 522,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de comunicação clara de procedimentos e ocorrências de produção, registre sua solução e explique os critérios utilizados.", "Relatos de ocorrências: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 611,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de estrutura dos documentos e relacione-os a interpretação de manuais, instruções e registros industriais.", "Estrutura dos documentos: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 612,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de interpretação de manuais, instruções e registros industriais, registre sua solução e explique os critérios utilizados.", "Estrutura dos documentos: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 621,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de interpretação de procedimentos e relacione-os a interpretação de manuais, instruções e registros industriais.", "Interpretação de procedimentos: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 622,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de interpretação de manuais, instruções e registros industriais, registre sua solução e explique os critérios utilizados.", "Interpretação de procedimentos: situação prática" });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Descricao", "NomeCompleto", "Resumo" },
                values: new object[] { "Curso fictício de medidas e escalas na metalurgia, voltado a medição de peças e interpretação de escalas. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "Medidas e Escalas na Metalurgia", "Aprenda sobre medição de peças e interpretação de escalas com exemplos do ambiente industrial." });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Descricao", "NomeCompleto", "Resumo" },
                values: new object[] { "Curso fictício de geometria aplicada ao design têxtil, voltado a formas geométricas, simetria e aproveitamento de tecidos. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "Geometria Aplicada ao Design Têxtil", "Aprenda sobre formas geométricas, simetria e aproveitamento de tecidos com exemplos do ambiente industrial." });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoriaId", "Descricao", "NomeBreve", "NomeCompleto", "PublicoAlvo", "Resumo" },
                values: new object[] { 2, "Curso fictício de energia e movimento na indústria, voltado a forças, movimento e transformação de energia em máquinas. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "Energia Industrial", "Energia e Movimento na Indústria", "Auxiliares de manutenção e estudantes de mecânica", "Aprenda sobre forças, movimento e transformação de energia em máquinas com exemplos do ambiente industrial." });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Descricao", "NomeBreve", "NomeCompleto", "PublicoAlvo", "Resumo" },
                values: new object[] { "Curso fictício de química dos materiais industriais, voltado a propriedades químicas, corrosão e seleção de materiais. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "Química Industrial", "Química dos Materiais Industriais", "Técnicos de laboratório e estudantes de materiais", "Aprenda sobre propriedades químicas, corrosão e seleção de materiais com exemplos do ambiente industrial." });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoriaId", "Descricao", "NomeBreve", "NomeCompleto", "PublicoAlvo", "Resumo" },
                values: new object[] { 3, "Curso fictício de comunicação técnica na produção, voltado a comunicação clara de procedimentos e ocorrências de produção. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "Comunicação na Produção", "Comunicação Técnica na Produção", "Líderes de equipe e assistentes de produção", "Aprenda sobre comunicação clara de procedimentos e ocorrências de produção com exemplos do ambiente industrial." });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoriaId", "Descricao", "NomeBreve", "NomeCompleto", "PublicoAlvo", "Resumo" },
                values: new object[] { 3, "Curso fictício de leitura de documentos industriais, voltado a interpretação de manuais, instruções e registros industriais. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "Documentação Industrial", "Leitura de Documentos Industriais", "Aprendizes industriais e auxiliares administrativos", "Aprenda sobre interpretação de manuais, instruções e registros industriais com exemplos do ambiente industrial." });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 11,
                column: "Descricao",
                value: "Estudo de unidades e escalas no contexto de medição de peças e interpretação de escalas.");

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 12,
                column: "Descricao",
                value: "Estudo de medição no chão de fábrica no contexto de medição de peças e interpretação de escalas.");

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 21,
                column: "Descricao",
                value: "Estudo de formas e simetria no contexto de formas geométricas, simetria e aproveitamento de tecidos.");

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 22,
                column: "Descricao",
                value: "Estudo de planejamento de cortes no contexto de formas geométricas, simetria e aproveitamento de tecidos.");

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de forças e movimento no contexto de forças, movimento e transformação de energia em máquinas.", "Forças e movimento" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de eficiência energética no contexto de forças, movimento e transformação de energia em máquinas.", "Eficiência energética" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de propriedades dos materiais no contexto de propriedades químicas, corrosão e seleção de materiais.", "Propriedades dos materiais" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de prevenção da corrosão no contexto de propriedades químicas, corrosão e seleção de materiais.", "Prevenção da corrosão" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de clareza e organização no contexto de comunicação clara de procedimentos e ocorrências de produção.", "Clareza e organização" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de relatos de ocorrências no contexto de comunicação clara de procedimentos e ocorrências de produção.", "Relatos de ocorrências" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de estrutura dos documentos no contexto de interpretação de manuais, instruções e registros industriais.", "Estrutura dos documentos" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de interpretação de procedimentos no contexto de interpretação de manuais, instruções e registros industriais.", "Interpretação de procedimentos" });
        }
    }
}
