import test from "node:test";
import assert from "node:assert/strict";
import { carouselTarget } from "../src/carouselTarget.ts";

test("avanço usa início real do cartão incluindo o gap", () => {
  assert.equal(carouselTarget(0, 1200, [0, 321, 642, 963], "next"), 321);
  assert.equal(carouselTarget(321, 1200, [0, 321, 642, 963], "next"), 642);
});
test("rolagem parcial retorna ou avança ao cartão adjacente", () => {
  assert.equal(carouselTarget(400, 1200, [0, 321, 642, 963], "previous"), 321);
  assert.equal(carouselTarget(400, 1200, [0, 321, 642, 963], "next"), 642);
});
test("desktop com cinco cartões visíveis limita o avanço ao fim real", () => {
  assert.equal(carouselTarget(0, 214, [0, 252, 504, 756, 1008, 1260], "next"), 214);
  assert.equal(carouselTarget(214, 214, [0, 252, 504, 756, 1008, 1260], "previous"), 0);
});
test("Home, End, ausência de overflow e arredondamento ficam nos limites", () => {
  assert.equal(carouselTarget(320, 1200, [0, 321, 642], "start"), 0);
  assert.equal(carouselTarget(0, 1200, [0, 321, 642], "end"), 1200);
  assert.equal(carouselTarget(-2, 0, [0], "next"), 0);
  assert.equal(carouselTarget(320.5, 1200, [0, 321, 642], "next"), 642);
});
