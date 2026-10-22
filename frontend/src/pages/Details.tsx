import { ApiError, catalogoApi } from "../api/catalogo";
import {
  Accordion,
  asset,
  Benefits,
  ErrorState,
  Loading,
  Testimonials,
} from "../components";
import { Link } from "../navigation";
import { useResource } from "../useResource";

export function Details({ id }: { id: string }) {
  const { result, retry } = useResource(`course:${id}`, (signal) =>
    catalogoApi.detalhar(Number(id), signal),
  );
  if (result.state === "loading")
    return (
      <div className="container section">
        <Loading label="Carregando informações do curso…" />
      </div>
    );
  if (result.state === "error")
    return (
      <div className="container section">
        {result.error instanceof ApiError &&
        (result.error.status === 404 || result.error.status === 400) ? (
          <div className="state-panel">
            <p className="eyebrow">Curso não encontrado</p>
            <h1>Este curso não está no catálogo</h1>
            <p>
              Confira o endereço ou encontre outra oportunidade para aprender.
            </p>
            <Link href="/cursos" className="button button-red">
              Explorar cursos
            </Link>
          </div>
        ) : (
          <ErrorState error={result.error} retry={retry} />
        )}
      </div>
    );
  const detail = result.data;
  const course = detail.curso;
  const date = course.dataDisponibilizacao
    ? new Intl.DateTimeFormat("pt-BR", { timeZone: "UTC" }).format(
        new Date(`${course.dataDisponibilizacao}T00:00:00Z`),
      )
    : null;
  return (
    <>
      <section className="detail-intro section">
        <div className="container">
          <nav className="breadcrumb" aria-label="Caminho">
            <Link href="/">Início</Link>
            <span aria-hidden="true">/</span>
            <Link href="/cursos">Cursos</Link>
            <span aria-hidden="true">/</span>
            <span>Detalhes</span>
          </nav>
          <div className="detail-grid">
            <div>
              <span
                className={`category-tag category-${course.categoria.codigo}`}
              >
                {course.categoria.nome}
              </span>
              <h1>{course.nomeCompleto}</h1>
              <dl className="detail-facts">
                <div>
                  <dt>Modalidade</dt>
                  <dd>{course.modalidade.nome}</dd>
                </div>
                <div>
                  <dt>Carga horária</dt>
                  <dd>{course.cargaHoraria} horas</dd>
                </div>
                <div>
                  <dt>Status</dt>
                  <dd>
                    <span className={`status status-${course.status.codigo}`}>
                      {course.status.nome}
                    </span>
                  </dd>
                </div>
                {date && (
                  <div>
                    <dt>Disponibilização</dt>
                    <dd>{date}</dd>
                  </div>
                )}
              </dl>
              <a href="#sobre-curso" className="button button-dark">
                Conhecer o conteúdo <span aria-hidden="true">↓</span>
              </a>
              <p className="small muted">
                Catálogo demonstrativo · matrícula indisponível
              </p>
            </div>
            <figure className="detail-photo">
              <img
                src={asset("video-thumb.png")}
                alt="Profissionais com equipamentos de segurança em uma indústria"
                width="640"
                height="360"
              />
              <figcaption>
                Imagem institucional de apresentação · sem vídeo disponível
              </figcaption>
            </figure>
          </div>
        </div>
      </section>
      <section
        className="container detail-benefits"
        aria-label="Benefícios institucionais ilustrativos"
      >
        <h2 className="sr-only">Benefícios institucionais ilustrativos</h2>
        <Benefits detail />
      </section>
      <section
        className="section"
        id="sobre-curso"
        aria-labelledby="sobre-curso-title"
      >
        <div className="container syllabus-grid">
          <div>
            <h2 id="sobre-curso-title">Sobre o curso</h2>
            <p>{detail.descricao}</p>
            <h3 className="audience-title">Para quem é este curso?</h3>
            <p>{detail.publicoAlvo}</p>
          </div>
          <div>
            <Accordion title="Módulos e aulas">
              {detail.modulos.map((modulo) => (
                <section className="module" key={modulo.id}>
                  <h3>
                    {modulo.ordem}. {modulo.titulo}
                  </h3>
                  <p>{modulo.descricao}</p>
                  <ol className="lesson-list">
                    {modulo.aulas.map((aula) => (
                      <li key={aula.id}>
                        <strong>{aula.titulo}</strong>
                        <p>{aula.descricao}</p>
                      </li>
                    ))}
                  </ol>
                </section>
              ))}
            </Accordion>
            <Accordion title="Modalidade e público-alvo">
              <p>
                <strong>Modalidade:</strong> {course.modalidade.nome}.
              </p>
              <p>
                <strong>Carga horária:</strong> {course.cargaHoraria} horas.
              </p>
              <p>
                <strong>Público-alvo:</strong> {detail.publicoAlvo}.
              </p>
            </Accordion>
            <Accordion title="Dúvidas frequentes">
              <h3>Posso me matricular por aqui?</h3>
              <p>
                Não. Esta é uma demonstração de catálogo. Não há matrícula,
                pagamento ou acesso às aulas.
              </p>
              <h3>Os cursos e datas são reais?</h3>
              <p>
                Não. Os dados são fictícios e foram criados exclusivamente para
                este projeto.
              </p>
            </Accordion>
          </div>
        </div>
      </section>
      <Testimonials beige />
      <section className="detail-end section">
        <div className="container">
          <p className="eyebrow">Continue explorando</p>
          <h2>
            Seu próximo conhecimento
            <br />
            começa com uma escolha.
          </h2>
          <Link href="/cursos" className="button button-white">
            Ver todos os cursos <span aria-hidden="true">→</span>
          </Link>
        </div>
      </section>
      <aside
        className="course-sticky"
        aria-label="Disponibilidade de matrícula"
      >
        <div>
          <strong>{course.nomeBreve}</strong>
          <span>Projeto demonstrativo · sem matrícula</span>
        </div>
        <button className="button button-white" disabled>
          Matrícula indisponível
        </button>
      </aside>
    </>
  );
}
