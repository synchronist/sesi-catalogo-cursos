/** Tolerância de um pixel para arredondamento de medidas do navegador. */
export function scrollBounds(left: number, width: number, total: number) {
  return { previous: left > 1, next: total - width - left > 1 };
}
