import { useEffect, useState } from "react";
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
import { CourseLearningContent, CourseQuestions } from "../CourseLearningPanels";

export function Details({ id }: { id: string }) {
  const { result, retry } = useResource(`course:${id}`, (signal) =>
    catalogoApi.detalhar(Number(id), signal),
  );
  const [footerVisible, setFooterVisible] = useState(false);
  useEffect(() => {
    if (result.state !== "success") return;
    const footer = document.querySelector(".site-footer");
    if (!footer) return;
    const observer = new IntersectionObserver(([entry]) => {
      setFooterVisible(entry.isIntersecting);
    });
    observer.observe(footer);
    return () => observer.disconnect();
  }, [result.state, id]);
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
            <span aria-current="page">{course.nomeCompleto}</span>
          </nav>
          <div className="detail-grid">
            <div>
              <span
                className={`category-tag category-${course.categoria.codigo}`}
              >
                <img src={asset(`icon-${course.categoria.codigo === "ciencias-da-natureza" ? "ciencias-natureza" : course.categoria.codigo}-20px.svg`)} alt="" width="16" height="16" />
                {course.categoria.nome}
              </span>
              <h1>{course.nomeCompleto}</h1>
              <div className="detail-highlights">
                {date && (
                  <p><svg viewBox="0 0 24 24" width="20" height="20" fill="none" stroke="currentColor" aria-hidden="true"><rect x="4" y="5" width="16" height="16" rx="2"/><path d="M8 2v6m8-6v6M4 11h16"/></svg>Início do curso: <strong>{date}</strong></p>
                )}
                <p title="Certificação ilustrativa do protótipo; não emitida nesta demonstração"><img src={asset("01-certificacao-bege.svg")} alt="" width="20" height="20" />Certificação: <strong>SESI</strong></p>
              </div>
              <Link className="button button-dark detail-enroll" href={`/ava/${course.id}?aula=introducao`}>
                Acessar curso <img src={asset("arrow-navigation-1.svg")} alt="" width="24" height="24" />
              </Link>
            </div>
            <figure className="detail-photo cta-photo">
              <img
                src={asset("video-thumb.png")}
                alt="Profissionais com equipamentos de segurança em uma indústria"
                width="640"
                height="360"
              />
              <button className="cta-play" type="button" disabled aria-label="Vídeo demonstrativo indisponível" title="Vídeo não disponível nesta demonstração"><svg viewBox="0 0 24 24" width="32" height="32" aria-hidden="true"><path d="M8 4 20 12 8 20Z" fill="currentColor" /></svg></button>
              <figcaption className="sr-only">
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
          </div>
          <div>
            <Accordion title="Módulos">
              {detail.modulos.map((modulo) => (
                <section className="module" key={modulo.id}>
                  <h3>
                    {modulo.ordem}. {modulo.titulo}
                  </h3>
                  <ul className="lesson-list">
                    {modulo.aulas.map((aula) => (
                      <li key={aula.id}>
                        {aula.titulo}
                      </li>
                    ))}
                  </ul>
                </section>
              ))}
            </Accordion>
            <Accordion title="Aulas online e avaliações">
              <CourseLearningContent />
            </Accordion>
            <Accordion title="Dúvidas frequentes">
              <CourseQuestions online={course.modalidade.codigo === "online"} />
            </Accordion>
          </div>
        </div>
      </section>
      <Testimonials beige />
      <section className="detail-end section">
        <div className="container">
          <p className="eyebrow">Precisa de ajuda para decidir?</p>
          <h2>Fale com a gente</h2>
          <button className="button button-white" disabled title="Contato não disponível nesta demonstração">
            Mandar uma mensagem <img src={asset("arrow-navigation-1.svg")} alt="" width="24" height="24" />
          </button>
        </div>
      </section>
      <aside
        className="course-sticky"
        hidden={footerVisible}
        aria-label="Acesso ao curso"
      >
        <div>
          <strong>{course.nomeCompleto}</strong>
        </div>
        <Link className="button button-white" href={`/ava/${course.id}?aula=introducao`}>
          Acessar curso <img src={asset("arrow-navigation-1.svg")} alt="" width="24" height="24" />
        </Link>
      </aside>
    </>
  );
}
