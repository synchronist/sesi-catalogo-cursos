export interface Opcao {
  codigo: string;
  nome: string;
}
export interface CursoResumo {
  id: number;
  nomeCompleto: string;
  nomeBreve: string;
  resumo: string;
  categoria: Opcao;
  modalidade: Opcao;
  status: Opcao;
  cargaHoraria: number;
  imagem: string;
  dataDisponibilizacao: string | null;
}
export interface Aula {
  id: number;
  titulo: string;
  descricao: string;
  ordem: number;
}
export interface Modulo {
  id: number;
  titulo: string;
  descricao: string;
  ordem: number;
  aulas: Aula[];
}
export interface CursoDetalhe {
  curso: CursoResumo;
  descricao: string;
  publicoAlvo: string;
  modulos: Modulo[];
}
export interface ListaCursos {
  items: CursoResumo[];
  total: number;
}
export interface Filtros {
  categorias: Opcao[];
  modalidades: Opcao[];
  status: Opcao[];
}
export interface Consulta {
  busca?: string;
  categoria?: string;
  modalidade?: string;
  status?: string;
}
export interface ProblemDetails {
  type?: string;
  title?: string;
  status?: number;
  detail?: string;
  instance?: string;
  traceId?: string;
  errors?: Record<string, string[]>;
}
export class ApiError extends Error {
  constructor(
    public readonly status: number,
    public readonly problem: ProblemDetails,
  ) {
    super(
      problem.detail ?? problem.title ?? "Não foi possível consultar a API.",
    );
  }
}
const baseUrl = (import.meta.env.VITE_API_BASE_URL ?? "").replace(/\/$/, "");

async function get<T>(path: string, signal?: AbortSignal): Promise<T> {
  const response = await fetch(`${baseUrl}${path}`, {
    signal,
    headers: { Accept: "application/json, application/problem+json" },
  });
  if (!response.ok) {
    const problem = (await response.json().catch(() => ({}))) as ProblemDetails;
    throw new ApiError(response.status, problem);
  }
  return response.json() as Promise<T>;
}
export const catalogoApi = {
  listar(consulta: Consulta = {}, signal?: AbortSignal) {
    const params = new URLSearchParams();
    for (const [key, value] of Object.entries(consulta))
      if (value?.trim()) params.set(key, value.trim());
    return get<ListaCursos>(`/api/cursos?${params}`, signal);
  },
  detalhar: (id: number, signal?: AbortSignal) =>
    get<CursoDetalhe>(`/api/cursos/${id}`, signal),
  filtros: (signal?: AbortSignal) =>
    get<Filtros>("/api/catalogo/filtros", signal),
};
