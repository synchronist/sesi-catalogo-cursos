import test from "node:test";
import assert from "node:assert/strict";
import { courseSectionHref, scrollToCourseSection } from "../src/courseAnchor.ts";

function environment(reduced, present, run) {
  const previousWindow = globalThis.window;
  const previousDocument = globalThis.document;
  const calls = [];
  globalThis.window = {
    matchMedia: (query) => {
      assert.equal(query, "(prefers-reduced-motion: reduce)");
      return { matches: reduced };
    },
  };
  globalThis.document = {
    getElementById: (id) => {
      assert.equal(id, "principais-cursos");
      return present ? {
        focus: (options) => calls.push(["focus", options]),
        scrollIntoView: (options) => calls.push(["scroll", options]),
      } : null;
    },
  };
  try { run(calls); }
  finally {
    globalThis.window = previousWindow;
    globalThis.document = previousDocument;
  }
}

test("âncora da landing move o foco sem salto e depois rola suavemente", () => {
  assert.equal(courseSectionHref, "/#principais-cursos");
  environment(false, true, (calls) => {
    assert.equal(scrollToCourseSection("#principais-cursos"), true);
    assert.deepEqual(calls, [
      ["focus", { preventScroll: true }],
      ["scroll", { behavior: "smooth", block: "start" }],
    ]);
  });
});

test("movimento reduzido usa rolagem sem animação", () => {
  environment(true, true, (calls) => {
    scrollToCourseSection("#principais-cursos");
    assert.equal(calls[1][1].behavior, "auto");
  });
});

test("não interfere nas demais âncoras ou rotas", () => {
  environment(false, true, (calls) => {
    assert.equal(scrollToCourseSection("#sobre"), false);
    assert.equal(scrollToCourseSection(""), false);
    assert.deepEqual(calls, []);
  });
});

test("alvo ausente não interrompe a navegação normal", () => {
  environment(false, false, (calls) => {
    assert.equal(scrollToCourseSection("#principais-cursos"), false);
    assert.deepEqual(calls, []);
  });
});
