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
            int CargaHoraria, string PublicoAlvo, string Tema, string Resumo, string ModuloInicial, string ModuloAplicado)[] dados =
        {
            ("Matemática para a Precisão: Medidas e Escalas na Metalurgia", "Medidas na Metalurgia", 1, "online", "disponivel", 20,
                "Operadores de usinagem e estudantes de metrologia", "grandezas, proporções e escalas na metalurgia",
                "Aplicação de grandezas, proporções e escalas em processos de corte, furação e usinagem.", "Unidades e escalas", "Medição no chão de fábrica"),
            ("Geometria e Modelagem no Design Têxtil", "Geometria Têxtil", 1, "semipresencial", "em-breve", 24,
                "Estudantes de moda e profissionais da indústria têxtil", "formas geométricas, simetria e padrões em tecidos",
                "Explora formas geométricas, simetria e padrões matemáticos presentes em tecidos e estampas.", "Formas e simetria", "Planejamento de cortes"),
            ("Estatística e Controle de Qualidade na Produção", "Estatística na Produção", 1, "online", "disponivel", 16,
                "Inspetores de qualidade e estudantes de processos industriais", "leitura de dados e controle estatístico da qualidade",
                "Leitura e interpretação de dados de controle de qualidade e produtividade.", "Dados e indicadores", "Controle estatístico da qualidade"),
            ("Física do Movimento e da Energia na Metalurgia", "Física na Metalurgia", 2, "semipresencial", "encerrado", 32,
                "Auxiliares de manutenção e estudantes de mecânica", "forças, atrito, calor e energia em máquinas",
                "Estudo de forças, atrito, calor e energia em máquinas, fornos e prensas.", "Forças e movimento", "Calor e energia nos processos"),
            ("Química e Sustentabilidade", "Química Sustentável", 2, "online", "disponivel", 12,
                "Técnicos de laboratório e profissionais do acabamento têxtil", "reações químicas na tinturaria e no acabamento de tecidos",
                "Estudo das reações químicas envolvidas na tinturaria e acabamento de tecidos.", "Reações e soluções", "Tinturaria e sustentabilidade"),
            ("Ciência dos Materiais na Metalurgia", "Materiais na Metalurgia", 2, "semipresencial", "em-breve", 18,
                "Aprendizes de metalurgia e estudantes de materiais", "estrutura, resistência e propriedades dos metais",
                "Conceitos básicos de estrutura, resistência e propriedades dos metais.", "Estrutura dos metais", "Resistência e seleção de materiais")
        };

        string[] imagens = ["medicao-metalurgia.png", "industria-textil.jpg", "controle-producao.jpg",
            "soldagem-industrial.jpg", "laboratorio-quimico.jpg", "equipamento-industrial.jpg"];
        for (var i = 0; i < dados.Length; i++)
        {
            var d = dados[i]; var cursoId = i + 1;
            model.Entity<Curso>().HasData(new
            {
                Id = cursoId, NomeCompleto = d.NomeCompleto, NomeBreve = d.NomeBreve, CategoriaId = d.CategoriaId,
                Modalidade = d.Modalidade, Status = d.Status, CargaHoraria = d.CargaHoraria, PublicoAlvo = d.PublicoAlvo,
                Resumo = d.Resumo,
                Descricao = cursoId == 1 ? CursoMetalurgiaSeed.Descricao : $"Curso fictício de {d.NomeCompleto.ToLowerInvariant()}, voltado a {d.Tema}. O percurso combina conceitos, análise de situações industriais e exercícios de aplicação. Ao final, o participante poderá justificar decisões e reconhecer cuidados necessários em sua rotina profissional.",
                Imagem = $"/assets/oficiais/figma/{imagens[i]}",
                DataDisponibilizacao = (DateOnly?)(d.Status == "em-breve" ? new DateOnly(2026, 11, 9) : new DateOnly(2026, 9, 1))
            });
            string[] titulos = cursoId == 1
                ? CursoMetalurgiaSeed.Modulos.Select(x => x.Titulo).ToArray()
                : [d.ModuloInicial, d.ModuloAplicado];
            for (var m = 1; m <= titulos.Length; m++)
            {
                var moduloId = cursoId * 10 + m;
                model.Entity<Modulo>().HasData(new { Id = moduloId, CursoId = cursoId, Titulo = titulos[m - 1],
                    Descricao = $"Estudo de {titulos[m - 1].ToLowerInvariant()} no contexto de {d.Tema}.", Ordem = m });
                var topicos = cursoId == 1 ? CursoMetalurgiaSeed.Modulos[m - 1].Topicos : null;
                for (var a = 1; a <= (topicos?.Length ?? 2); a++)
                    model.Entity<Aula>().HasData(new Aula { Id = moduloId * 10 + a, ModuloId = moduloId, Ordem = a,
                        Titulo = topicos?[a - 1] ?? $"{titulos[m - 1]}: {(a == 1 ? "conceitos essenciais" : "situação prática")}",
                        Descricao = topicos is not null ? $"Tópico demonstrativo do protótipo: {topicos[a - 1]}"
                            : a == 1 ? $"Identifique os conceitos de {titulos[m - 1].ToLowerInvariant()} e relacione-os a {d.Tema}."
                            : $"Analise um caso fictício de {d.Tema}, registre sua solução e explique os critérios utilizados." });
            }
        }
    }
}
