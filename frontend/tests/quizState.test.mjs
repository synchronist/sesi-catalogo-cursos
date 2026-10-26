import test from "node:test";
import assert from "node:assert/strict";
import { initialQuiz, quizReducer } from "../src/quizState.ts";

test("selecionar não valida, erro admite tentativa e acerto trava alterações", () => {
  let state = quizReducer(initialQuiz, { type: "validate", correct: 1 });
  assert.equal(state.status, "idle");
  state = quizReducer(state, { type: "select", answer: 0 });
  assert.equal(state.status, "idle");
  state = quizReducer(state, { type: "validate", correct: 1 });
  assert.equal(state.status, "incorrect");
  assert.equal(state.attempts, 1);
  state = quizReducer(state, { type: "select", answer: 1 });
  state = quizReducer(state, { type: "validate", correct: 1 });
  assert.equal(state.status, "correct");
  assert.equal(state.attempts, 2);
  assert.equal(quizReducer(state, { type: "select", answer: 2 }), state);
  assert.equal(quizReducer(state, { type: "validate", correct: 0 }), state);
});
test("somente refazer explícito limpa a resposta validada", () => {
  const correct = { answer: 1, status: "correct", attempts: 1 };
  const retry = quizReducer(correct, { type: "retry" });
  assert.deepEqual(retry, { answer: null, status: "idle", attempts: 1 });
  assert.equal(quizReducer(retry, { type: "select", answer: 2 }).answer, 2);
});
