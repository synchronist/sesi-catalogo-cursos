using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sesi.Catalogo.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCatalogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Nome = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeCompleto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NomeBreve = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Resumo = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(6000)", maxLength: 6000, nullable: false),
                    CategoriaId = table.Column<int>(type: "integer", nullable: false),
                    Modalidade = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CargaHoraria = table.Column<int>(type: "integer", nullable: false),
                    PublicoAlvo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Imagem = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DataDisponibilizacao = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cursos", x => x.Id);
                    table.CheckConstraint("ck_curso_carga", "\"CargaHoraria\" > 0");
                    table.CheckConstraint("ck_curso_modalidade", "\"Modalidade\" IN ('online', 'semipresencial')");
                    table.CheckConstraint("ck_curso_status", "\"Status\" IN ('disponivel', 'em-breve', 'encerrado')");
                    table.ForeignKey(
                        name: "FK_cursos_categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "modulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CursoId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Ordem = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modulos", x => x.Id);
                    table.CheckConstraint("ck_modulo_ordem", "\"Ordem\" > 0");
                    table.ForeignKey(
                        name: "FK_modulos_cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aulas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuloId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Ordem = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aulas", x => x.Id);
                    table.CheckConstraint("ck_aula_ordem", "\"Ordem\" > 0");
                    table.ForeignKey(
                        name: "FK_aulas_modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categorias",
                columns: new[] { "Id", "Codigo", "Nome" },
                values: new object[,]
                {
                    { 1, "matematica", "Matemática" },
                    { 2, "ciencias-da-natureza", "Ciências da Natureza" },
                    { 3, "linguagens", "Linguagens" }
                });

            migrationBuilder.InsertData(
                table: "cursos",
                columns: new[] { "Id", "CargaHoraria", "CategoriaId", "DataDisponibilizacao", "Descricao", "Imagem", "Modalidade", "NomeBreve", "NomeCompleto", "PublicoAlvo", "Resumo", "Status" },
                values: new object[,]
                {
                    { 1, 20, 1, new DateOnly(2026, 9, 1), "Curso fictício de medidas e escalas na metalurgia, voltado a medição de peças e interpretação de escalas. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "/assets/oficiais/banner-ava.png", "online", "Medidas na Metalurgia", "Medidas e Escalas na Metalurgia", "Operadores de usinagem e estudantes de metrologia", "Aprenda sobre medição de peças e interpretação de escalas com exemplos do ambiente industrial.", "disponivel" },
                    { 2, 24, 1, new DateOnly(2026, 11, 9), "Curso fictício de geometria aplicada ao design têxtil, voltado a formas geométricas, simetria e aproveitamento de tecidos. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "/assets/oficiais/banner-ava.png", "semipresencial", "Geometria Têxtil", "Geometria Aplicada ao Design Têxtil", "Estudantes de moda e profissionais da indústria têxtil", "Aprenda sobre formas geométricas, simetria e aproveitamento de tecidos com exemplos do ambiente industrial.", "em-breve" },
                    { 3, 16, 2, new DateOnly(2026, 9, 1), "Curso fictício de energia e movimento na indústria, voltado a forças, movimento e transformação de energia em máquinas. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "/assets/oficiais/banner-ava.png", "online", "Energia Industrial", "Energia e Movimento na Indústria", "Auxiliares de manutenção e estudantes de mecânica", "Aprenda sobre forças, movimento e transformação de energia em máquinas com exemplos do ambiente industrial.", "disponivel" },
                    { 4, 32, 2, new DateOnly(2026, 9, 1), "Curso fictício de química dos materiais industriais, voltado a propriedades químicas, corrosão e seleção de materiais. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "/assets/oficiais/banner-ava.png", "semipresencial", "Química Industrial", "Química dos Materiais Industriais", "Técnicos de laboratório e estudantes de materiais", "Aprenda sobre propriedades químicas, corrosão e seleção de materiais com exemplos do ambiente industrial.", "encerrado" },
                    { 5, 12, 3, new DateOnly(2026, 9, 1), "Curso fictício de comunicação técnica na produção, voltado a comunicação clara de procedimentos e ocorrências de produção. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "/assets/oficiais/banner-ava.png", "online", "Comunicação na Produção", "Comunicação Técnica na Produção", "Líderes de equipe e assistentes de produção", "Aprenda sobre comunicação clara de procedimentos e ocorrências de produção com exemplos do ambiente industrial.", "disponivel" },
                    { 6, 18, 3, new DateOnly(2026, 11, 9), "Curso fictício de leitura de documentos industriais, voltado a interpretação de manuais, instruções e registros industriais. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.", "/assets/oficiais/banner-ava.png", "semipresencial", "Documentação Industrial", "Leitura de Documentos Industriais", "Aprendizes industriais e auxiliares administrativos", "Aprenda sobre interpretação de manuais, instruções e registros industriais com exemplos do ambiente industrial.", "em-breve" }
                });

            migrationBuilder.InsertData(
                table: "modulos",
                columns: new[] { "Id", "CursoId", "Descricao", "Ordem", "Titulo" },
                values: new object[,]
                {
                    { 11, 1, "Estudo de unidades e escalas no contexto de medição de peças e interpretação de escalas.", 1, "Unidades e escalas" },
                    { 12, 1, "Estudo de medição no chão de fábrica no contexto de medição de peças e interpretação de escalas.", 2, "Medição no chão de fábrica" },
                    { 21, 2, "Estudo de formas e simetria no contexto de formas geométricas, simetria e aproveitamento de tecidos.", 1, "Formas e simetria" },
                    { 22, 2, "Estudo de planejamento de cortes no contexto de formas geométricas, simetria e aproveitamento de tecidos.", 2, "Planejamento de cortes" },
                    { 31, 3, "Estudo de forças e movimento no contexto de forças, movimento e transformação de energia em máquinas.", 1, "Forças e movimento" },
                    { 32, 3, "Estudo de eficiência energética no contexto de forças, movimento e transformação de energia em máquinas.", 2, "Eficiência energética" },
                    { 41, 4, "Estudo de propriedades dos materiais no contexto de propriedades químicas, corrosão e seleção de materiais.", 1, "Propriedades dos materiais" },
                    { 42, 4, "Estudo de prevenção da corrosão no contexto de propriedades químicas, corrosão e seleção de materiais.", 2, "Prevenção da corrosão" },
                    { 51, 5, "Estudo de clareza e organização no contexto de comunicação clara de procedimentos e ocorrências de produção.", 1, "Clareza e organização" },
                    { 52, 5, "Estudo de relatos de ocorrências no contexto de comunicação clara de procedimentos e ocorrências de produção.", 2, "Relatos de ocorrências" },
                    { 61, 6, "Estudo de estrutura dos documentos no contexto de interpretação de manuais, instruções e registros industriais.", 1, "Estrutura dos documentos" },
                    { 62, 6, "Estudo de interpretação de procedimentos no contexto de interpretação de manuais, instruções e registros industriais.", 2, "Interpretação de procedimentos" }
                });

            migrationBuilder.InsertData(
                table: "aulas",
                columns: new[] { "Id", "Descricao", "ModuloId", "Ordem", "Titulo" },
                values: new object[,]
                {
                    { 111, "Identifique os conceitos de unidades e escalas e relacione-os a medição de peças e interpretação de escalas.", 11, 1, "Unidades e escalas: conceitos essenciais" },
                    { 112, "Analise um caso fictício de medição de peças e interpretação de escalas, registre sua solução e explique os critérios utilizados.", 11, 2, "Unidades e escalas: situação prática" },
                    { 121, "Identifique os conceitos de medição no chão de fábrica e relacione-os a medição de peças e interpretação de escalas.", 12, 1, "Medição no chão de fábrica: conceitos essenciais" },
                    { 122, "Analise um caso fictício de medição de peças e interpretação de escalas, registre sua solução e explique os critérios utilizados.", 12, 2, "Medição no chão de fábrica: situação prática" },
                    { 211, "Identifique os conceitos de formas e simetria e relacione-os a formas geométricas, simetria e aproveitamento de tecidos.", 21, 1, "Formas e simetria: conceitos essenciais" },
                    { 212, "Analise um caso fictício de formas geométricas, simetria e aproveitamento de tecidos, registre sua solução e explique os critérios utilizados.", 21, 2, "Formas e simetria: situação prática" },
                    { 221, "Identifique os conceitos de planejamento de cortes e relacione-os a formas geométricas, simetria e aproveitamento de tecidos.", 22, 1, "Planejamento de cortes: conceitos essenciais" },
                    { 222, "Analise um caso fictício de formas geométricas, simetria e aproveitamento de tecidos, registre sua solução e explique os critérios utilizados.", 22, 2, "Planejamento de cortes: situação prática" },
                    { 311, "Identifique os conceitos de forças e movimento e relacione-os a forças, movimento e transformação de energia em máquinas.", 31, 1, "Forças e movimento: conceitos essenciais" },
                    { 312, "Analise um caso fictício de forças, movimento e transformação de energia em máquinas, registre sua solução e explique os critérios utilizados.", 31, 2, "Forças e movimento: situação prática" },
                    { 321, "Identifique os conceitos de eficiência energética e relacione-os a forças, movimento e transformação de energia em máquinas.", 32, 1, "Eficiência energética: conceitos essenciais" },
                    { 322, "Analise um caso fictício de forças, movimento e transformação de energia em máquinas, registre sua solução e explique os critérios utilizados.", 32, 2, "Eficiência energética: situação prática" },
                    { 411, "Identifique os conceitos de propriedades dos materiais e relacione-os a propriedades químicas, corrosão e seleção de materiais.", 41, 1, "Propriedades dos materiais: conceitos essenciais" },
                    { 412, "Analise um caso fictício de propriedades químicas, corrosão e seleção de materiais, registre sua solução e explique os critérios utilizados.", 41, 2, "Propriedades dos materiais: situação prática" },
                    { 421, "Identifique os conceitos de prevenção da corrosão e relacione-os a propriedades químicas, corrosão e seleção de materiais.", 42, 1, "Prevenção da corrosão: conceitos essenciais" },
                    { 422, "Analise um caso fictício de propriedades químicas, corrosão e seleção de materiais, registre sua solução e explique os critérios utilizados.", 42, 2, "Prevenção da corrosão: situação prática" },
                    { 511, "Identifique os conceitos de clareza e organização e relacione-os a comunicação clara de procedimentos e ocorrências de produção.", 51, 1, "Clareza e organização: conceitos essenciais" },
                    { 512, "Analise um caso fictício de comunicação clara de procedimentos e ocorrências de produção, registre sua solução e explique os critérios utilizados.", 51, 2, "Clareza e organização: situação prática" },
                    { 521, "Identifique os conceitos de relatos de ocorrências e relacione-os a comunicação clara de procedimentos e ocorrências de produção.", 52, 1, "Relatos de ocorrências: conceitos essenciais" },
                    { 522, "Analise um caso fictício de comunicação clara de procedimentos e ocorrências de produção, registre sua solução e explique os critérios utilizados.", 52, 2, "Relatos de ocorrências: situação prática" },
                    { 611, "Identifique os conceitos de estrutura dos documentos e relacione-os a interpretação de manuais, instruções e registros industriais.", 61, 1, "Estrutura dos documentos: conceitos essenciais" },
                    { 612, "Analise um caso fictício de interpretação de manuais, instruções e registros industriais, registre sua solução e explique os critérios utilizados.", 61, 2, "Estrutura dos documentos: situação prática" },
                    { 621, "Identifique os conceitos de interpretação de procedimentos e relacione-os a interpretação de manuais, instruções e registros industriais.", 62, 1, "Interpretação de procedimentos: conceitos essenciais" },
                    { 622, "Analise um caso fictício de interpretação de manuais, instruções e registros industriais, registre sua solução e explique os critérios utilizados.", 62, 2, "Interpretação de procedimentos: situação prática" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_aulas_ModuloId_Ordem",
                table: "aulas",
                columns: new[] { "ModuloId", "Ordem" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categorias_Codigo",
                table: "categorias",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cursos_CategoriaId_Modalidade_Status",
                table: "cursos",
                columns: new[] { "CategoriaId", "Modalidade", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_modulos_CursoId_Ordem",
                table: "modulos",
                columns: new[] { "CursoId", "Ordem" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aulas");

            migrationBuilder.DropTable(
                name: "modulos");

            migrationBuilder.DropTable(
                name: "cursos");

            migrationBuilder.DropTable(
                name: "categorias");
        }
    }
}
