import type { CursoResumo } from "./api/catalogo";

// Fotografias recuperadas do próprio protótipo; associação apenas da Landing.
const photos: Record<number, string> = {
  1: "medicao-metalurgia.png",
  2: "industria-textil.jpg",
  3: "controle-producao.jpg",
  4: "soldagem-industrial.jpg",
  5: "laboratorio-quimico.jpg",
  6: "equipamento-industrial.jpg",
};

export function featuredImage(course: Pick<CursoResumo, "id" | "imagem">): string {
  // Uma imagem específica fornecida futuramente pela API continua prioritária.
  if (course.imagem !== "/assets/oficiais/banner-ava.png" || !photos[course.id]) {
    return course.imagem;
  }
  return `/assets/oficiais/figma/${photos[course.id]}`;
}

export function featuredOrder<T extends { id: number }>(courses: readonly T[]): T[] {
  return [...courses].sort((a, b) => a.id - b.id);
}
