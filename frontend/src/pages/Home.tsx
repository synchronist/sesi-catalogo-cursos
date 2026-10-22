import { useRef } from "react";
import { catalogoApi } from "../api/catalogo";
import {
  Arrow,
  Benefits,
  CallToAction,
  CourseCard,
  ErrorState,
  Loading,
  Testimonials,
} from "../components";
import { Link } from "../navigation";
import { useResource } from "../useResource";

export function Home() {
  const { result, retry } = useResource("home-cursos", (signal) =>
    catalogoApi.listar({}, signal),
  );
  const rail = useRef<HTMLDivElement>(null);
  return (
    <>
      <section className="hero">
        <div className="container hero-content">
          <h1>
            Impulsione
            <br />
            sua carreira
            <br />
            na indústria
          </h1>
          <p>
            Amplie seus conhecimentos com excelência na melhor plataforma de
            estudos da indústria brasileira.
          </p>
          <Link href="/cursos" className="button button-white">
            Conhecer cursos <Arrow />
          </Link>
        </div>
      </section>
      <section
        className="section featured-section"
        aria-labelledby="destaques-title"
      >
        <div className="container">
          <h2 id="destaques-title">Principais cursos</h2>
          {result.state === "loading" && <Loading />}
          {result.state === "error" && (
            <ErrorState error={result.error} retry={retry} />
          )}
          {result.state === "success" &&
            (result.data.total ? (
              <div
                className="featured-rail"
                ref={rail}
                tabIndex={0}
                aria-label="Cursos em destaque"
              >
                {result.data.items.map((curso) => (
                  <CourseCard key={curso.id} curso={curso} featured />
                ))}
              </div>
            ) : (
              <p className="state-panel" role="status">
                Nenhum curso disponível no momento.
              </p>
            ))}
          <div className="section-bottom">
            <Link href="/cursos" className="button button-outline">
              Ver todos os cursos
            </Link>
            {result.state === "success" && result.data.total > 0 && (
              <div className="rail-buttons">
                <button
                  aria-label="Cursos anteriores"
                  onClick={() => rail.current?.scrollBy({ left: -320 })}
                >
                  ←
                </button>
                <button
                  aria-label="Próximos cursos"
                  onClick={() => rail.current?.scrollBy({ left: 320 })}
                >
                  →
                </button>
              </div>
            )}
          </div>
        </div>
      </section>
      <section
        id="sobre"
        className="section impact-section"
        aria-labelledby="impacto-title"
      >
        <div className="container impact-grid">
          <div>
            <h2 id="impacto-title">
              Impacto{" "}
              <br />
              transformador
            </h2>
            <p>
              Resultados que demonstram o sucesso da nossa metodologia de
              capacitação contínua para a indústria.
            </p>
            <p className="muted small">
              Indicadores ilustrativos do protótipo.
            </p>
          </div>
          <div>
            <dl className="impact-stats">
              <div>
                <dt>Profissionais capacitados</dt>
                <dd>25.000+</dd>
              </div>
              <div>
                <dt>Empresas parceiras</dt>
                <dd>500+</dd>
              </div>
              <div>
                <dt>Avaliação média</dt>
                <dd>4.8/5</dd>
              </div>
              <div>
                <dt>Horas de capacitação</dt>
                <dd>2.5M+</dd>
              </div>
            </dl>
            <Benefits />
          </div>
        </div>
      </section>
      <Testimonials />
      <CallToAction />
    </>
  );
}
