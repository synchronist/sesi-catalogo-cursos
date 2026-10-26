import test from "node:test";
import assert from "node:assert/strict";
import { carouselPosition } from "../src/carouselMotion.ts";

test("movimento de 850ms respeita início, meio e final sem ultrapassar limites", () => {
  assert.equal(carouselPosition(0, 728, -1), 0);
  assert.equal(carouselPosition(0, 728, 425), 364);
  assert.equal(carouselPosition(0, 728, 850), 728);
  assert.equal(carouselPosition(0, 728, 1000), 728);
});
test("acelera e desacelera em ambos os sentidos", () => {
  assert.ok(carouselPosition(0, 728, 100) < 728 * 100 / 850);
  assert.ok(carouselPosition(0, 728, 750) > 728 * 750 / 850);
  assert.equal(carouselPosition(728, 0, 850), 0);
  assert.equal(carouselPosition(728, 0, 0), 728);
  assert.equal(carouselPosition(0, 728, 0, 0), 728);
});
