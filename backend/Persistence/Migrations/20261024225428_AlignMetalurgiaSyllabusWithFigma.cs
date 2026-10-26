using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sesi.Catalogo.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlignMetalurgiaSyllabusWithFigma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Tópico demonstrativo do protótipo: Conceitos fundamentais de medidas e escalas aplicados à metalurgia.", "Conceitos fundamentais de medidas e escalas aplicados à metalurgia." });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Tópico demonstrativo do protótipo: Importância da precisão nas medições e controle de qualidade.", "Importância da precisão nas medições e controle de qualidade." });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Tópico demonstrativo do protótipo: Tipos de medidas utilizadas: comprimento, massa, volume e temperatura.", "Tipos de medidas utilizadas: comprimento, massa, volume e temperatura." });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Tópico demonstrativo do protótipo: Técnicas de análise de precisão nas medições.", "Técnicas de análise de precisão nas medições." });

            migrationBuilder.InsertData(
                table: "aulas",
                columns: new[] { "Id", "Descricao", "ModuloId", "Ordem", "Titulo" },
                values: new object[,]
                {
                    { 113, "Tópico demonstrativo do protótipo: Panorama das normas e regulamentos no Brasil relacionados à metalurgia.", 11, 3, "Panorama das normas e regulamentos no Brasil relacionados à metalurgia." },
                    { 123, "Tópico demonstrativo do protótipo: Elaboração de gráficos e tabelas de medidas.", 12, 3, "Elaboração de gráficos e tabelas de medidas." }
                });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descricao",
                value: "Explore nosso curso de Matemática para a Precisão: Medidas e Escalas na Metalurgia, perfeito para quem deseja se destacar nesse campo! Este programa abrangente foi desenvolvido para capacitar profissionais com as habilidades e conhecimentos fundamentais para garantir medições precisas e eficientes no ambiente metalúrgico.");

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de introdução à matemática na metalurgia no contexto de grandezas, proporções e escalas na metalurgia.", "Introdução à Matemática na Metalurgia" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de identificação e avaliação de medidas no contexto de grandezas, proporções e escalas na metalurgia.", "Identificação e Avaliação de Medidas" });

            migrationBuilder.InsertData(
                table: "modulos",
                columns: new[] { "Id", "CursoId", "Descricao", "Ordem", "Titulo" },
                values: new object[,]
                {
                    { 13, 1, "Estudo de normas e regulamentações no contexto de grandezas, proporções e escalas na metalurgia.", 3, "Normas e Regulamentações" },
                    { 14, 1, "Estudo de instrumentos de medição no contexto de grandezas, proporções e escalas na metalurgia.", 4, "Instrumentos de Medição" },
                    { 15, 1, "Estudo de prevenção de erros de medição no contexto de grandezas, proporções e escalas na metalurgia.", 5, "Prevenção de Erros de Medição" },
                    { 16, 1, "Estudo de ergonomia e eficiência na medição no contexto de grandezas, proporções e escalas na metalurgia.", 6, "Ergonomia e Eficiência na Medição" },
                    { 17, 1, "Estudo de análise de resultados de medição no contexto de grandezas, proporções e escalas na metalurgia.", 7, "Análise de Resultados de Medição" },
                    { 18, 1, "Estudo de cultura de precisão e comportamento seguro no contexto de grandezas, proporções e escalas na metalurgia.", 8, "Cultura de Precisão e Comportamento Seguro" },
                    { 19, 1, "Estudo de tecnologias aplicadas à medição no contexto de grandezas, proporções e escalas na metalurgia.", 9, "Tecnologias Aplicadas à Medição" },
                    { 20, 1, "Estudo de avaliação final e certificação no contexto de grandezas, proporções e escalas na metalurgia.", 10, "Avaliação Final e Certificação" }
                });

            migrationBuilder.InsertData(
                table: "aulas",
                columns: new[] { "Id", "Descricao", "ModuloId", "Ordem", "Titulo" },
                values: new object[,]
                {
                    { 131, "Tópico demonstrativo do protótipo: Estudo das principais normas aplicáveis à metalurgia (ABNT, ISO, entre outras).", 13, 1, "Estudo das principais normas aplicáveis à metalurgia (ABNT, ISO, entre outras)." },
                    { 132, "Tópico demonstrativo do protótipo: Interpretação e aplicação prática das normas de medição.", 13, 2, "Interpretação e aplicação prática das normas de medição." },
                    { 133, "Tópico demonstrativo do protótipo: Atualizações e impactos legais nas práticas de medição.", 13, 3, "Atualizações e impactos legais nas práticas de medição." },
                    { 141, "Tópico demonstrativo do protótipo: Tipos de instrumentos utilizados na metalurgia: paquímetros, micrômetros e balanças.", 14, 1, "Tipos de instrumentos utilizados na metalurgia: paquímetros, micrômetros e balanças." },
                    { 142, "Tópico demonstrativo do protótipo: Critérios de seleção, uso e calibração.", 14, 2, "Critérios de seleção, uso e calibração." },
                    { 143, "Tópico demonstrativo do protótipo: Responsabilidades legais de operadores e supervisores.", 14, 3, "Responsabilidades legais de operadores e supervisores." },
                    { 151, "Tópico demonstrativo do protótipo: Tipos de erros e incertezas nas medições.", 15, 1, "Tipos de erros e incertezas nas medições." },
                    { 152, "Tópico demonstrativo do protótipo: Procedimentos para garantir a precisão e exatidão.", 15, 2, "Procedimentos para garantir a precisão e exatidão." },
                    { 153, "Tópico demonstrativo do protótipo: Treinamento prático em técnicas de medição.", 15, 3, "Treinamento prático em técnicas de medição." },
                    { 161, "Tópico demonstrativo do protótipo: Princípios ergonômicos aplicados ao uso de instrumentos de medição.", 16, 1, "Princípios ergonômicos aplicados ao uso de instrumentos de medição." },
                    { 162, "Tópico demonstrativo do protótipo: Prevenção de lesões por uso inadequado de ferramentas.", 16, 2, "Prevenção de lesões por uso inadequado de ferramentas." },
                    { 163, "Tópico demonstrativo do protótipo: Qualidade de vida no ambiente de trabalho.", 16, 3, "Qualidade de vida no ambiente de trabalho." },
                    { 171, "Tópico demonstrativo do protótipo: Técnicas de análise de dados de medições.", 17, 1, "Técnicas de análise de dados de medições." },
                    { 172, "Tópico demonstrativo do protótipo: Elaboração de relatórios e planos de ação para melhorias.", 17, 2, "Elaboração de relatórios e planos de ação para melhorias." },
                    { 173, "Tópico demonstrativo do protótipo: Indicadores de precisão e melhoria contínua.", 17, 3, "Indicadores de precisão e melhoria contínua." },
                    { 181, "Tópico demonstrativo do protótipo: Desenvolvimento de uma cultura organizacional voltada à precisão nas medições.", 18, 1, "Desenvolvimento de uma cultura organizacional voltada à precisão nas medições." },
                    { 182, "Tópico demonstrativo do protótipo: Comunicação eficaz e liderança em práticas de medição.", 18, 2, "Comunicação eficaz e liderança em práticas de medição." },
                    { 183, "Tópico demonstrativo do protótipo: Programas de conscientização e engajamento dos colaboradores.", 18, 3, "Programas de conscientização e engajamento dos colaboradores." },
                    { 191, "Tópico demonstrativo do protótipo: Uso de plataformas digitais, sensores e automação na medição de precisão.", 19, 1, "Uso de plataformas digitais, sensores e automação na medição de precisão." },
                    { 192, "Tópico demonstrativo do protótipo: Sistemas de gestão de qualidade.", 19, 2, "Sistemas de gestão de qualidade." },
                    { 193, "Tópico demonstrativo do protótipo: Tendências e inovações no setor metalúrgico.", 19, 3, "Tendências e inovações no setor metalúrgico." },
                    { 201, "Tópico demonstrativo do protótipo: Teste de conhecimentos adquiridos sobre medidas e escalas.", 20, 1, "Teste de conhecimentos adquiridos sobre medidas e escalas." },
                    { 202, "Tópico demonstrativo do protótipo: Atividades práticas e estudos de caso.", 20, 2, "Atividades práticas e estudos de caso." },
                    { 203, "Tópico demonstrativo do protótipo: Emissão de certificado digital de conclusão.", 20, 3, "Emissão de certificado digital de conclusão." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de unidades e escalas e relacione-os a grandezas, proporções e escalas na metalurgia.", "Unidades e escalas: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de grandezas, proporções e escalas na metalurgia, registre sua solução e explique os critérios utilizados.", "Unidades e escalas: situação prática" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Identifique os conceitos de medição no chão de fábrica e relacione-os a grandezas, proporções e escalas na metalurgia.", "Medição no chão de fábrica: conceitos essenciais" });

            migrationBuilder.UpdateData(
                table: "aulas",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Analise um caso fictício de grandezas, proporções e escalas na metalurgia, registre sua solução e explique os critérios utilizados.", "Medição no chão de fábrica: situação prática" });

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descricao",
                value: "Curso fictício de matemática para a precisão: medidas e escalas na metalurgia, voltado a grandezas, proporções e escalas na metalurgia. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.");

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de unidades e escalas no contexto de grandezas, proporções e escalas na metalurgia.", "Unidades e escalas" });

            migrationBuilder.UpdateData(
                table: "modulos",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Descricao", "Titulo" },
                values: new object[] { "Estudo de medição no chão de fábrica no contexto de grandezas, proporções e escalas na metalurgia.", "Medição no chão de fábrica" });
        }
    }
}
