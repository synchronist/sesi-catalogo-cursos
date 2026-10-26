import { catalogoApi } from "../api/catalogo";
import {
  Benefits,
  CallToAction,
  CourseCard,
  ErrorState,
  Loading,
  Testimonials,
  asset,
} from "../components";
import { Link } from "../navigation";
import { useResource } from "../useResource";
import { courseSectionHref, courseSectionId } from "../courseAnchor";
import { useCourseCarousel } from "../useCourseCarousel";
import { featuredOrder } from "../featuredPresentation";

export function Home() {
  const { result, retry } = useResource("home-cursos", (signal) =>
    catalogoApi.listar({}, signal),
  );
  const { rail, bounds, move } = useCourseCarousel(
    result.state === "success" ? result.data.items.length : 0,
  );
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
          <Link href={courseSectionHref} className="button button-white">
            Conhecer cursos <img src={asset("arrow-navigation-1.svg")} alt="" width="24" height="24" />
          </Link>
        </div>
      </section>
      <section
        id={courseSectionId}
        tabIndex={-1}
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
                id="principais-cursos-trilho"
                ref={rail}
                tabIndex={0}
                role="region"
                aria-roledescription="carrossel"
                aria-label="Cursos em destaque"
                onKeyDown={(event) => {
                  if (event.altKey || event.ctrlKey || event.metaKey || event.shiftKey) return;
                  const direction = { ArrowLeft: "previous", ArrowRight: "next", Home: "start", End: "end" }[event.key];
                  if (!direction) return;
                  event.preventDefault();
                  move(direction as "previous" | "next" | "start" | "end");
                }}
              >
                {featuredOrder(result.data.items).map((curso) => (
                  <CourseCard key={curso.id} curso={curso} featured />
                ))}
                <Link href="/cursos" className="course-card featured-all-courses">
                  <span>Todos os cursos</span>
                  <img src={asset("arrow-navigation-1.svg")} alt="" width="32" height="32" />
                </Link>
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
                  aria-controls="principais-cursos-trilho"
                  disabled={!bounds.previous}
                  onClick={() => move("start")}
                >
                  <img src={asset("arrow-navigation.svg")} alt="" width="48" height="48" />
                </button>
                <button
                  aria-label="Próximos cursos"
                  aria-controls="principais-cursos-trilho"
                  disabled={!bounds.next}
                  onClick={() => move("end")}
                >
                  <img src={asset("arrow-navigation-1.svg")} alt="" width="48" height="48" />
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
            <p className="sr-only">
              Indicadores ilustrativos do protótipo.
            </p>
          </div>
          <div>
            <dl className="impact-stats">
              <div>
                <dt>Profissionais Capacitados</dt>
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
