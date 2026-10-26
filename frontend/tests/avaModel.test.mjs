import test from "node:test";
import assert from "node:assert/strict";
import { activityReady, avaHref, browserProgressRepository, completeLesson, courseCompleted, filterLearningModules, learningModules, normalizeProgress, progressPercent, revokeCompletion, toggleCompletion } from "../src/avaModel.ts";
import { learningText, practicesContent } from "../src/avaContent.ts";

const detail = { descricao: "Descrição do curso", modulos: [
  { id: 2, ordem: 2, titulo: "Produção", aulas: [{ id: 4, ordem: 2, titulo: "Prática", descricao: "Conteúdo real" }, { id: 3, ordem: 1, titulo: "Precisão", descricao: "Medir" }] },
  { id: 1, ordem: 1, titulo: "Medição", aulas: [{ id: 1, ordem: 1, titulo: "Unidades", descricao: "Escalas" }] },
] };
test("AVA preserva aulas e descrições da API em ordem, sem mutar a resposta", () => {
  const modules = learningModules(detail);
  assert.deepEqual(modules.map(m => m.key), ["introduction", "module-1", "module-2"]);
  assert.deepEqual(modules[2].lessons.map(l => l.key), ["aula-3", "aula-4"]);
  assert.equal(modules[2].lessons[1].description, "Conteúdo real");
  assert.equal(detail.modulos[0].aulas[0].id, 4);
});
test("busca ignora acentos e mantém as aulas de um módulo encontrado", () => {
  const modules = learningModules(detail);
  assert.deepEqual(filterLearningModules(modules, "precisao")[0].lessons.map(l => l.key), ["aula-3"]);
  assert.equal(filterLearningModules(modules, "producao")[0].lessons.length, 2);
  assert.equal(filterLearningModules(modules, "inexistente").length, 0);
  assert.equal(filterLearningModules(modules, " ").length, 3);
});
test("progresso começa em zero e só conta aulas válidas únicas", () => {
  const valid = ["introducao", "aula-1"];
  assert.equal(progressPercent({ completed: [] }, valid), 0);
  const cleaned = normalizeProgress({ completed: ["aula-1", "aula-1", "removida", 3] }, valid);
  assert.deepEqual(cleaned.completed, ["aula-1"]);
  assert.equal(progressPercent(cleaned, valid), 50);
  assert.equal(progressPercent({ completed: [] }, []), 0);
  assert.equal(progressPercent(toggleCompletion(cleaned, "introducao"), valid), 100);
  assert.equal(progressPercent(toggleCompletion(cleaned, "aula-1"), valid), 0);
});
test("repositório persiste por curso e tolera JSON corrompido/armazenamento bloqueado", () => {
  const values = new Map();
  const repository = browserProgressRepository({ getItem: key => values.get(key) ?? null, setItem: (key, value) => values.set(key, value) });
  assert.equal(repository.save(1, { completed: ["aula-1"] }), true);
  assert.deepEqual(repository.load(1, ["aula-1"]).completed, ["aula-1"]);
  assert.deepEqual(repository.load(2, ["aula-1"]).completed, []);
  values.set("sesi.ava.progress.v2.course.1", "{broken");
  assert.deepEqual(repository.load(1, ["aula-1"]).completed, []);
  const blocked = browserProgressRepository({ getItem() { throw Error("blocked"); }, setItem() { throw Error("blocked"); } });
  assert.deepEqual(blocked.load(1, []).completed, []);
  assert.equal(blocked.save(1, { completed: [] }), false);
});
test("link da aula é reproduzível e conteúdo contextualiza outros cursos", () => {
  assert.equal(avaHref(6, "aula-42"), "/ava/6?aula=aula-42");
  assert.equal(avaHref(1), "/ava/1?aula=introducao");
  assert.match(practicesContent("Química e Sustentabilidade", false).practices[0], /Química e Sustentabilidade/);
  assert.doesNotMatch(practicesContent("Química e Sustentabilidade", false).recommendations.join(" "), /metalurgia/);
});
test("revisão editorial limpa rótulos provisórios sem inventar conteúdo ou serviços", () => {
  assert.equal(learningText("Tópico demonstrativo do protótipo: Unidades de medida"), "Unidades de medida");
  assert.equal(learningText("Curso fictício de química."), "Este curso aborda química.");
  assert.equal(learningText(""), "");
  const text = Object.values(practicesContent("Química", false)).flat().join(" ");
  assert.doesNotMatch(text, /fóruns|chats|instrutores|certificado/i);
});
test("conclusões fora de ordem são preservadas, sem concluir o curso prematuramente", () => {
  const keys = ["a", "b", "c"];
  assert.deepEqual(normalizeProgress({ completed: ["c", "a"] }, keys), { completed: ["c", "a"] });
  assert.equal(courseCompleted({ completed: ["c", "a"] }, keys), false);
  assert.equal(courseCompleted({ completed: ["c", "b", "a"] }, keys), true);
  assert.equal(courseCompleted({ completed: [] }, []), false);
  const many = Array.from({ length: 201 }, (_, i) => String(i));
  assert.equal(progressPercent({ completed: many.slice(0, -1) }, many), 99);
});
test("conclusão exige leitura, resposta validada e reprodução conforme os tipos presentes", () => {
  const lesson = { key: "a", question: { correct: 1 }, video: true };
  const empty = { completed: [] };
  for (const evidence of [{ read: false, answered: true, watched: true }, { read: true, answered: false, watched: true }, { read: true, answered: true, watched: false }]) {
    assert.equal(activityReady(lesson, evidence), false);
    assert.deepEqual(completeLesson(empty, lesson, ["a"], evidence), empty);
  }
  const valid = { read: true, answered: true, watched: true };
  assert.deepEqual(completeLesson(empty, lesson, ["a"], valid), { completed: ["a"] });
  assert.deepEqual(completeLesson(empty, { ...lesson, key: "b" }, ["a", "b"], valid), { completed: ["b"] });
  assert.deepEqual(completeLesson(empty, { ...lesson, key: "inexistente" }, ["a", "b"], valid), empty);
  assert.equal(activityReady({ key: "text" }, { read: true, answered: false, watched: false }), true);
});
test("refazer uma aula preserva as conclusões das outras e persistência fora de ordem", () => {
  assert.deepEqual(revokeCompletion({ completed: ["a", "b", "c"] }, "b", ["a", "b", "c"]), { completed: ["a", "c"] });
  const store = new Map();
  const repository = browserProgressRepository({ getItem: key => store.get(key), setItem: (key, value) => store.set(key, value) });
  repository.save(2, { completed: ["c"] });
  assert.deepEqual(repository.load(2, ["a", "b", "c"]), { completed: ["c"] });
});
test("progresso antigo manual não libera o novo fluxo e cada módulo tem demonstração e pergunta", () => {
  const repository = browserProgressRepository({ getItem: key => key.includes("v1") ? JSON.stringify({ completed: ["introducao"] }) : null, setItem() {} });
  assert.deepEqual(repository.load(1, ["introducao"]).completed, []);
  const modules = learningModules(detail);
  assert.equal(modules[1].lessons[0].video, true);
  assert.ok(modules[1].lessons.at(-1).question);
  assert.ok(modules[0].lessons[2].question);
});
