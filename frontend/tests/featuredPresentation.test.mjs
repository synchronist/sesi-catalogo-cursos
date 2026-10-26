import test from "node:test";
import assert from "node:assert/strict";
import { existsSync } from "node:fs";
import { featuredImage, featuredOrder } from "../src/featuredPresentation.ts";

test("ordena a apresentação sem substituir, duplicar ou mutar os resultados da API", () => {
  const courses = [{ id: 5, nome: "Real A" }, { id: 1, nome: "Real B" }, { id: 3, nome: "Real C" }];
  const ordered = featuredOrder(courses);
  assert.deepEqual(ordered.map(c => c.id), [1, 3, 5]);
  assert.deepEqual(courses.map(c => c.id), [5, 1, 3]);
  assert.equal(ordered[0], courses[1]);
  assert.equal(ordered.length, courses.length);
});
test("seis fotografias distintas do protótipo existem fisicamente", () => {
  const paths = [1, 2, 3, 4, 5, 6].map(id => featuredImage({ id, imagem: "/assets/oficiais/banner-ava.png" }));
  assert.equal(new Set(paths).size, 6);
  for (const path of paths) assert.ok(existsSync(new URL(`../public${path}`, import.meta.url)));
});
test("preserva imagem específica da API e mantém fallback para curso desconhecido", () => {
  assert.equal(featuredImage({ id: 1, imagem: "/imagem-da-api.jpg" }), "/imagem-da-api.jpg");
  assert.equal(featuredImage({ id: 99, imagem: "/assets/oficiais/banner-ava.png" }), "/assets/oficiais/banner-ava.png");
});
