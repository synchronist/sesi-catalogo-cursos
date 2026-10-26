import type { CursoDetalhe } from "./api/catalogo";

export interface LearningLesson {
  key: string;
  title: string;
  description: string;
  kind: "introduction" | "practices" | "extras" | "lesson";
  video?: boolean;
  question?: { prompt: string; options: string[]; correct: number; explanation: string };
}
export interface LearningModule {
  key: string;
  title: string;
  lessons: LearningLesson[];
}
export interface LearningProgress {
  completed: string[];
}
export interface ProgressRepository {
  load(courseId: number, validKeys: readonly string[]): LearningProgress;
  save(courseId: number, progress: LearningProgress): boolean;
}

export function learningModules(detail: CursoDetalhe): LearningModule[] {
  return [
    {
      key: "introduction", title: "Introdução",
      lessons: [
        { key: "introducao", title: "Introdução", description: detail.descricao, kind: "introduction" },
        { key: "boas-praticas", title: "Boas práticas", description: "", kind: "practices" },
        { key: "extras", title: "Extras", description: "", kind: "extras", question: {
          prompt: "Qual atitude contribui para o aprendizado?",
          options: ["Avançar sem revisar o conteúdo", "Revisar e aplicar os conhecimentos", "Ignorar os tópicos dos módulos"], correct: 1,
          explanation: "Revisar reforça o que você aprendeu. Aplicar os conceitos em situações práticas ajuda a transformar esse conhecimento em habilidade.",
        } },
      ],
    },
    ...[...detail.modulos].sort((a, b) => a.ordem - b.ordem || a.id - b.id).map(module => ({
      key: `module-${module.id}`, title: module.titulo,
      lessons: [...module.aulas].sort((a, b) => a.ordem - b.ordem || a.id - b.id).map((lesson, index, all) => ({
        key: `aula-${lesson.id}`, title: lesson.titulo, description: lesson.descricao, kind: "lesson" as const,
        video: index === 0,
        question: index === all.length - 1 ? {
          prompt: "Qual é o tema do módulo que você acabou de estudar?",
          options: ["Um assunto fora deste curso", module.titulo, "Nenhum tema foi apresentado"], correct: 1,
          explanation: `Este módulo aborda ${module.titulo}. Relacionar o tema às situações apresentadas nas aulas ajuda a organizar sua revisão.`,
        } : undefined,
      })),
    })),
  ];
}

export function normalizeProgress(value: unknown, validKeys: readonly string[]): LearningProgress {
  const completed = value && typeof value === "object" && "completed" in value
    ? (value as { completed: unknown }).completed : [];
  return { completed: Array.isArray(completed)
    ? [...new Set(completed.filter((key): key is string => typeof key === "string" && validKeys.includes(key)))]
    : [] };
}
export function progressPercent(progress: LearningProgress, validKeys: readonly string[]) {
  const count = normalizeProgress(progress, validKeys).completed.length;
  return !validKeys.length ? 0 : count === validKeys.length ? 100 : Math.min(99, Math.round(count / validKeys.length * 100));
}
export function courseCompleted(progress: LearningProgress, keys: readonly string[]) {
  return keys.length > 0 && keys.every(key => progress.completed.includes(key));
}
export function toggleCompletion(progress: LearningProgress, key: string): LearningProgress {
  return { completed: progress.completed.includes(key)
    ? progress.completed.filter(item => item !== key) : [...progress.completed, key] };
}
export function filterLearningModules(modules: readonly LearningModule[], query: string) {
  const normalize = (text: string) => text.normalize("NFD").replace(/\p{Diacritic}/gu, "").toLocaleLowerCase("pt-BR");
  const term = normalize(query.trim());
  if (!term) return [...modules];
  return modules.map(module => ({ ...module, lessons: normalize(module.title).includes(term)
    ? module.lessons : module.lessons.filter(lesson => normalize(lesson.title).includes(term))
  })).filter(module => module.lessons.length);
}
export const avaHref = (courseId: number, lesson = "introducao") =>
  `/ava/${courseId}?aula=${encodeURIComponent(lesson)}`;

// Substituível por implementação HTTP quando existir identidade autenticada.
// Não representa matrícula, certificação ou progresso sincronizado no servidor.
export function browserProgressRepository(storage: Pick<Storage, "getItem" | "setItem">): ProgressRepository {
  const key = (courseId: number) => `sesi.ava.progress.v2.course.${courseId}`;
  return {
    load(courseId, validKeys) {
      try { return normalizeProgress(JSON.parse(storage.getItem(key(courseId)) ?? "null"), validKeys); }
      catch { return { completed: [] }; }
    },
    save(courseId, progress) {
      try { storage.setItem(key(courseId), JSON.stringify(progress)); return true; }
      catch { return false; }
    },
  };
}

export function activityReady(lesson: LearningLesson, evidence: { read: boolean; answered: boolean; watched: boolean }) {
  return evidence.read && (!lesson.question || evidence.answered) && (!lesson.video || evidence.watched);
}
export function completeLesson(progress: LearningProgress, lesson: LearningLesson, keys: readonly string[], evidence: { read: boolean; answered: boolean; watched: boolean }): LearningProgress {
  if (!keys.includes(lesson.key) || !activityReady(lesson, evidence)) return progress;
  return normalizeProgress({ completed: [...progress.completed, lesson.key] }, keys);
}
export function revokeCompletion(progress: LearningProgress, key: string, keys: readonly string[]) {
  return { completed: normalizeProgress(progress, keys).completed.filter(item => item !== key) };
}
