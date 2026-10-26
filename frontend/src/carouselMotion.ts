/** Interpolação com aceleração e desaceleração, limitada aos extremos. */
export function carouselPosition(start: number, end: number, elapsed: number, duration = 850): number {
  const t = duration <= 0 ? 1 : Math.min(1, Math.max(0, elapsed / duration));
  const eased = t < 0.5 ? 4 * t ** 3 : 1 - (-2 * t + 2) ** 3 / 2;
  return start + (end - start) * eased;
}
