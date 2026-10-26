/** Próximo início real de cartão, limitado à extensão rolável do trilho. */
export function carouselTarget(
  left: number,
  maximum: number,
  starts: number[],
  direction: "previous" | "next" | "start" | "end",
): number {
  const max = Math.max(0, maximum);
  const current = Math.min(max, Math.max(0, left));
  if (direction === "start") return 0;
  if (direction === "end") return max;
  const target = direction === "next"
    ? starts.find((start) => start > current + 1) ?? max
    : [...starts].reverse().find((start) => start < current - 1) ?? 0;
  return Math.min(max, Math.max(0, target));
}
