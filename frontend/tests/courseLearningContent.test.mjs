import test from "node:test";
import assert from "node:assert/strict";
import { courseQuestions, onlineTopics, assessmentTopics } from "../src/courseLearningContent.ts";

test("conteúdo compartilhado mantém quatro tópicos de aulas e quatro de avaliações", () => {
  assert.equal(onlineTopics.length, 4);
  assert.equal(assessmentTopics.length, 4);
});
test("dez perguntas preservam ordem e adaptam presença e horários à modalidade", () => {
  const online = courseQuestions(true);
  const mixed = courseQuestions(false);
  assert.equal(online.length, 10);
  assert.deepEqual(online.map(q => q.question), mixed.map(q => q.question));
  assert.match(online[0].answer, /^Sim\./);
  assert.match(mixed[0].answer, /^Não\./);
  assert.match(mixed[0].answer, /semipresencial/);
  assert.match(mixed[1].answer, /encontros presenciais/);
  assert.deepEqual(online.slice(2), mixed.slice(2));
});
