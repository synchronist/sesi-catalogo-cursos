using Microsoft.EntityFrameworkCore;
using Sesi.Catalogo.Api.Entities;

namespace Sesi.Catalogo.Api.Persistence;

// Dados versionados nas migrations: IDs e datas fixos, sem recriação a cada inicialização.
public static class CatalogoSeed
{
    public static void Configure(ModelBuilder model)
    {
        model.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Codigo = "matematica", Nome = "Matemática" },
            new Categoria { Id = 2, Codigo = "ciencias-da-natureza", Nome = "Ciências da Natureza" },
            new Categoria { Id = 3, Codigo = "linguagens", Nome = "Linguagens" });

        (string NomeCompleto, string NomeBreve, int CategoriaId, string Modalidade, string Status,
            int CargaHoraria, string PublicoAlvo, string Tema, string ModuloInicial, string ModuloAplicado)[] dados =
        {
            ("Medidas e Escalas na Metalurgia", "Medidas na Metalurgia", 1, "online", "disponivel", 20,
                "Operadores de usinagem e estudantes de metrologia", "medição de peças e interpretação de escalas", "Unidades e escalas", "Medição no chão de fábrica"),
            ("Geometria Aplicada ao Design Têxtil", "Geometria Têxtil", 1, "semipresencial", "em-breve", 24,
                "Estudantes de moda e profissionais da indústria têxtil", "formas geométricas, simetria e aproveitamento de tecidos", "Formas e simetria", "Planejamento de cortes"),
            ("Energia e Movimento na Indústria", "Energia Industrial", 2, "online", "disponivel", 16,
                "Auxiliares de manutenção e estudantes de mecânica", "forças, movimento e transformação de energia em máquinas", "Forças e movimento", "Eficiência energética"),
            ("Química dos Materiais Industriais", "Química Industrial", 2, "semipresencial", "encerrado", 32,
                "Técnicos de laboratório e estudantes de materiais", "propriedades químicas, corrosão e seleção de materiais", "Propriedades dos materiais", "Prevenção da corrosão"),
            ("Comunicação Técnica na Produção", "Comunicação na Produção", 3, "online", "disponivel", 12,
                "Líderes de equipe e assistentes de produção", "comunicação clara de procedimentos e ocorrências de produção", "Clareza e organização", "Relatos de ocorrências"),
            ("Leitura de Documentos Industriais", "Documentação Industrial", 3, "semipresencial", "em-breve", 18,
                "Aprendizes industriais e auxiliares administrativos", "interpretação de manuais, instruções e registros industriais", "Estrutura dos documentos", "Interpretação de procedimentos")
        };

        for (var i = 0; i < dados.Length; i++)
        {
            var d = dados[i]; var cursoId = i + 1;
            model.Entity<Curso>().HasData(new
            {
                Id = cursoId, NomeCompleto = d.NomeCompleto, NomeBreve = d.NomeBreve, CategoriaId = d.CategoriaId,
                Modalidade = d.Modalidade, Status = d.Status, CargaHoraria = d.CargaHoraria, PublicoAlvo = d.PublicoAlvo,
                Resumo = $"Aprenda sobre {d.Tema} com exemplos do ambiente industrial.",
                Descricao = $"Curso fictício de {d.NomeCompleto.ToLowerInvariant()}, voltado a {d.Tema}. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.",
                Imagem = "/assets/oficiais/banner-ava.png",
                DataDisponibilizacao = (DateOnly?)(d.Status == "em-breve" ? new DateOnly(2026, 11, 9) : new DateOnly(2026, 9, 1))
            });
            string[] titulos = [d.ModuloInicial, d.ModuloAplicado];
            for (var m = 1; m <= 2; m++)
            {
                var moduloId = cursoId * 10 + m;
                model.Entity<Modulo>().HasData(new { Id = moduloId, CursoId = cursoId, Titulo = titulos[m - 1],
                    Descricao = $"Estudo de {titulos[m - 1].ToLowerInvariant()} no contexto de {d.Tema}.", Ordem = m });
                for (var a = 1; a <= 2; a++)
                    model.Entity<Aula>().HasData(new Aula { Id = moduloId * 10 + a, ModuloId = moduloId, Ordem = a,
                        Titulo = $"{titulos[m - 1]}: {(a == 1 ? "conceitos essenciais" : "situação prática")}",
                        Descricao = a == 1 ? $"Identifique os conceitos de {titulos[m - 1].ToLowerInvariant()} e relacione-os a {d.Tema}."
                            : $"Analise um caso fictício de {d.Tema}, registre sua solução e explique os critérios utilizados." });
            }
        }
    }
}
