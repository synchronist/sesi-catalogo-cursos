namespace Sesi.Catalogo.Api.Persistence;

// Texto observado no frame público 6017:707. Conteúdo demonstrativo do protótipo.
public static class CursoMetalurgiaSeed
{
    public const string Descricao = "Explore nosso curso de Matemática para a Precisão: Medidas e Escalas na Metalurgia, perfeito para quem deseja se destacar nesse campo! Este programa abrangente foi desenvolvido para capacitar profissionais com as habilidades e conhecimentos fundamentais para garantir medições precisas e eficientes no ambiente metalúrgico.";

    public static readonly (string Titulo, string[] Topicos)[] Modulos =
    [
        ("Introdução à Matemática na Metalurgia", [
            "Conceitos fundamentais de medidas e escalas aplicados à metalurgia.",
            "Importância da precisão nas medições e controle de qualidade.",
            "Panorama das normas e regulamentos no Brasil relacionados à metalurgia."]),
        ("Identificação e Avaliação de Medidas", [
            "Tipos de medidas utilizadas: comprimento, massa, volume e temperatura.",
            "Técnicas de análise de precisão nas medições.",
            "Elaboração de gráficos e tabelas de medidas."]),
        ("Normas e Regulamentações", [
            "Estudo das principais normas aplicáveis à metalurgia (ABNT, ISO, entre outras).",
            "Interpretação e aplicação prática das normas de medição.",
            "Atualizações e impactos legais nas práticas de medição."]),
        ("Instrumentos de Medição", [
            "Tipos de instrumentos utilizados na metalurgia: paquímetros, micrômetros e balanças.",
            "Critérios de seleção, uso e calibração.",
            "Responsabilidades legais de operadores e supervisores."]),
        ("Prevenção de Erros de Medição", [
            "Tipos de erros e incertezas nas medições.",
            "Procedimentos para garantir a precisão e exatidão.",
            "Treinamento prático em técnicas de medição."]),
        ("Ergonomia e Eficiência na Medição", [
            "Princípios ergonômicos aplicados ao uso de instrumentos de medição.",
            "Prevenção de lesões por uso inadequado de ferramentas.",
            "Qualidade de vida no ambiente de trabalho."]),
        ("Análise de Resultados de Medição", [
            "Técnicas de análise de dados de medições.",
            "Elaboração de relatórios e planos de ação para melhorias.",
            "Indicadores de precisão e melhoria contínua."]),
        ("Cultura de Precisão e Comportamento Seguro", [
            "Desenvolvimento de uma cultura organizacional voltada à precisão nas medições.",
            "Comunicação eficaz e liderança em práticas de medição.",
            "Programas de conscientização e engajamento dos colaboradores."]),
        ("Tecnologias Aplicadas à Medição", [
            "Uso de plataformas digitais, sensores e automação na medição de precisão.",
            "Sistemas de gestão de qualidade.",
            "Tendências e inovações no setor metalúrgico."]),
        ("Avaliação Final e Certificação", [
            "Teste de conhecimentos adquiridos sobre medidas e escalas.",
            "Atividades práticas e estudos de caso.",
            "Emissão de certificado digital de conclusão."])
    ];
}
