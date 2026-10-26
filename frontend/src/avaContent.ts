// Revisão editorial do frame 6017:1042, sem prometer recursos indisponíveis.
export function learningText(text: string) {
  return text.replace(/^Curso fictício de /i, "Este curso aborda ")
    .replace(/^Tópico demonstrativo do protótipo:\s*/i, "")
    .replace(/^Identifique os conceitos de (.+) e relacione-os a .+\.$/i, "Estude os conceitos de $1 e suas aplicações práticas.")
    .replace(/^Analise um caso fictício de (.+), registre sua solução e explique os critérios utilizados\.$/i, "Relacione o conteúdo a uma situação prática de $1 e explique suas escolhas.");
}
export function practicesContent(courseName: string, metalurgia: boolean) {
  return {
    practices: [
      `Para estudar ${courseName}, escolha um local tranquilo e organize o material de que vai precisar.`,
      "Reserve um horário para estudar, faça anotações e revise os conceitos antes de passar para outro tema. Se encontrar alguma dificuldade, retome a aula e registre suas dúvidas.",
      "Relacione o conteúdo às situações do seu dia a dia. Explorar exemplos e explicar o que aprendeu com suas próprias palavras ajuda a fixar os conceitos.",
    ],
    recommendations: [
      metalurgia ? "Ao trabalhar com medidas e escalas, confira as unidades, a precisão do instrumento e a interpretação dos resultados." : "Ao revisar cada módulo, identifique os conceitos principais e pense em como aplicá-los na prática.",
      "Leia o conteúdo completo e responda às questões com atenção. Quando precisar, você pode voltar às aulas e refazer as atividades.",
      "Avance no seu ritmo. Para finalizar o curso, conclua todas as aulas, incluindo as atividades e prévias indicadas.",
    ],
  };
}
