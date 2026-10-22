import test from "node:test";
import assert from "node:assert/strict";
import { scrollBounds } from "../src/scrollBounds.ts";

test("depoimentos sem overflow desabilitam ambas as direções", () => {
  assert.deepEqual(scrollBounds(0, 1206, 1206), { previous: false, next: false });
});
test("rolagem habilita somente as direções disponíveis", () => {
  assert.deepEqual(scrollBounds(0, 350, 1200), { previous: false, next: true });
  assert.deepEqual(scrollBounds(320, 350, 1200), { previous: true, next: true });
  assert.deepEqual(scrollBounds(850, 350, 1200), { previous: true, next: false });
});
test("arredondamento e overscroll não habilitam direção inexistente", () => {
  assert.deepEqual(scrollBounds(-2, 350, 1200), { previous: false, next: true });
  assert.deepEqual(scrollBounds(849.5, 350, 1200), { previous: true, next: false });
});
