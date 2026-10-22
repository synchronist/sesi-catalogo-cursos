import { useEffect, useState } from "react";
import type { FormEvent } from "react";
import { catalogoApi } from "../api/catalogo";
import { catalogUrl, readQuery } from "../catalogQuery";
import type { CatalogQuery } from "../catalogQuery";
import { CourseCard, ErrorState, Loading } from "../components";
import { Link, navigate } from "../navigation";
import { useResource } from "../useResource";

export function Catalog({ search }: { search: string }) {
  const query = readQuery(search);
  const [draft, setDraft] = useState<CatalogQuery>(query);
  useEffect(() => setDraft(readQuery(search)), [search]);
  const filters = useResource("catalog-filters", (signal) =>
    catalogoApi.filtros(signal),
  );
  const courses = useResource(`catalog:${search}`, (signal) =>
    catalogoApi.listar(query, signal),
  );
  function submit(event: FormEvent) {
    event.preventDefault();
    navigate(catalogUrl(draft));
  }
  function clear() {
    setDraft({});
    navigate("/cursos");
  }
  const active = Object.values(query).some(Boolean);
  return (
    <>
      <section className="catalog-intro">
        <div className="container">
          <nav className="breadcrumb" aria-label="Caminho">
            <Link href="/">Início</Link>
            <span aria-hidden="true">/</span>
            <span>Cursos</span>
          </nav>
          <p className="eyebrow">Conhecimento para transformar</p>
          <h1>Encontre seu próximo curso</h1>
          <p>
            Aprenda no seu ritmo. Explore novas possibilidades para crescer na
            indústria.
          </p>
        </div>
      </section>
      <section
        className="section catalog-section"
        aria-label="Catálogo de cursos"
      >
        <div className="container">
          <form
            className="filter-form"
            onSubmit={submit}
            aria-label="Pesquisar e filtrar cursos"
          >
            <div className="search-field">
              <label htmlFor="busca">O que você quer aprender?</label>
              <div className="search-input">
                <span aria-hidden="true">⌕</span>
                <input
                  id="busca"
                  type="search"
                  placeholder="Pesquise pelo nome do curso"
                  maxLength={150}
                  value={draft.busca ?? ""}
                  onChange={(e) =>
                    setDraft({ ...draft, busca: e.target.value })
                  }
                />
              </div>
            </div>
            <div className="filter-row">
              {(["categoria", "modalidade", "status"] as const).map(
                (name, index) => {
                  const options =
                    filters.result.state === "success"
                      ? name === "categoria"
                        ? filters.result.data.categorias
                        : name === "modalidade"
                          ? filters.result.data.modalidades
                          : filters.result.data.status
                      : [];
                  return (
                    <div className="field" key={name}>
                      <label htmlFor={name}>
                        {["Categoria", "Modalidade", "Status"][index]}
                      </label>
                      <select
                        id={name}
                        value={draft[name] ?? ""}
                        disabled={filters.result.state !== "success"}
                        onChange={(e) =>
                          setDraft({ ...draft, [name]: e.target.value })
                        }
                      >
                        <option value="">
                          {
                            [
                              "Todas as categorias",
                              "Todas as modalidades",
                              "Todos os status",
                            ][index]
                          }
                        </option>
                        {draft[name] &&
                          !options.some((o) => o.codigo === draft[name]) && (
                            <option value={draft[name]}>
                              Valor da URL: {draft[name]}
                            </option>
                          )}
                        {options.map((option) => (
                          <option key={option.codigo} value={option.codigo}>
                            {option.nome}
                          </option>
                        ))}
                      </select>
                    </div>
                  );
                },
              )}
              <button className="button button-red search-button" type="submit">
                Buscar cursos <span aria-hidden="true">→</span>
              </button>
            </div>
            {filters.result.state === "loading" && (
              <p className="small muted" role="status">
                Carregando opções de filtro…
              </p>
            )}
            {filters.result.state === "error" && (
              <div className="filter-error" role="alert">
                Não foi possível carregar as opções de filtro. A pesquisa por
                nome continua disponível.{" "}
                <button
                  className="text-button"
                  type="button"
                  onClick={filters.retry}
                >
                  Recarregar filtros
                </button>
              </div>
            )}
          </form>
          <div className="results-heading">
            <p role="status" aria-live="polite">
              {courses.result.state === "success" ? (
                <>
                  <strong>{courses.result.data.total}</strong>{" "}
                  {courses.result.data.total === 1
                    ? "curso encontrado"
                    : "cursos encontrados"}
                </>
              ) : courses.result.state === "loading" ? (
                "Consultando o catálogo…"
              ) : (
                "Consulta indisponível"
              )}
            </p>
            <button
              className="text-button"
              onClick={clear}
              disabled={!active && !Object.values(draft).some(Boolean)}
            >
              Limpar filtros <span aria-hidden="true">×</span>
            </button>
          </div>
          <div aria-busy={courses.result.state === "loading"}>
            <h2 className="sr-only">Resultados do catálogo</h2>
            {courses.result.state === "loading" && <Loading />}
            {courses.result.state === "error" && (
              <ErrorState
                error={courses.result.error}
                retry={courses.retry}
                clearFilters={clear}
              />
            )}
            {courses.result.state === "success" &&
              (courses.result.data.total > 0 ? (
                <div className="course-grid">
                  {courses.result.data.items.map((curso) => (
                    <CourseCard key={curso.id} curso={curso} />
                  ))}
                </div>
              ) : (
                <div className="state-panel empty-panel" role="status">
                  <span className="state-symbol" aria-hidden="true">
                    ⌕
                  </span>
                  <h2>Nenhum curso encontrado</h2>
                  <p>
                    Tente outro nome ou remova alguns filtros para explorar mais
                    possibilidades.
                  </p>
                  <button className="button button-dark" onClick={clear}>
                    Limpar busca e filtros
                  </button>
                </div>
              ))}
          </div>
        </div>
      </section>
    </>
  );
}
