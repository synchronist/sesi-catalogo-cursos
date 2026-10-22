export interface CatalogQuery {
  busca?: string;
  categoria?: string;
  modalidade?: string;
  status?: string;
}
const keys = ["busca", "categoria", "modalidade", "status"] as const;
export function readQuery(search: string): CatalogQuery {
  const params = new URLSearchParams(search);
  return Object.fromEntries(
    keys.flatMap((key) => {
      const value = params.get(key)?.trim();
      return value ? [[key, value]] : [];
    }),
  );
}
export function catalogUrl(query: CatalogQuery): string {
  const params = new URLSearchParams();
  for (const key of keys)
    if (query[key]?.trim()) params.set(key, query[key]!.trim());
  const search = params.toString();
  return `/cursos${search ? `?${search}` : ""}`;
}
// Mesmo se o transporte ignorar AbortSignal, uma resposta antiga não será aplicada.
export function latestRequest() {
  let revision = 0;
  let controller: AbortController | undefined;
  return {
    begin() {
      controller?.abort();
      controller = new AbortController();
      const current = ++revision;
      const signal = controller.signal;
      return {
        signal,
        isCurrent: () => current === revision && !signal.aborted,
      };
    },
    cancel() {
      revision++;
      controller?.abort();
    },
  };
}
