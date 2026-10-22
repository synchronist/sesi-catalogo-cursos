import { useEffect, useRef, useState } from "react";
import type { ReactNode } from "react";
import type { CursoResumo } from "./api/catalogo";
import { ApiError } from "./api/catalogo";
import { Link } from "./navigation";
import { scrollBounds } from "./scrollBounds";

export const asset = (name: string) => `/assets/oficiais/${name}`;
export function Arrow() {
  return <span aria-hidden="true">→</span>;
}

export function Header({
  home,
  pathname,
}: {
  home: boolean;
  pathname: string;
}) {
  const [open, setOpen] = useState(false);
  const [scrolled, setScrolled] = useState(window.scrollY > 20);
  const menuButton = useRef<HTMLButtonElement>(null);
  useEffect(() => {
    const update = () => setScrolled(window.scrollY > 20);
    window.addEventListener("scroll", update, { passive: true });
    return () => window.removeEventListener("scroll", update);
  }, []);
  const light = home && !scrolled && !open;
  return (
    <header className={`site-header ${light ? "on-hero" : ""}`}>
      <div className="container header-inner">
        <Link
          href="/"
          aria-label="SESI Indústria — início"
          onClick={() => setOpen(false)}
        >
          <img
            className="brand"
            src={asset(
              `logo-sesi-industria-${light ? "white" : "primary"}.svg`,
            )}
            alt="SESI Indústria"
            width="188"
            height="19"
          />
        </Link>
        <button
          ref={menuButton}
          className="menu-button"
          aria-expanded={open}
          aria-controls="main-nav"
          onClick={() => setOpen(!open)}
        >
          {open ? "Fechar" : "Menu"}{" "}
          <span aria-hidden="true">{open ? "×" : "☰"}</span>
        </button>
        <nav
          id="main-nav"
          className={open ? "nav-open" : ""}
          aria-label="Principal"
          onKeyDown={(event) => {
            if (event.key === "Escape") {
              setOpen(false);
              menuButton.current?.focus();
            }
          }}
        >
          <Link
            href="/"
            aria-current={pathname === "/" ? "page" : undefined}
            onClick={() => setOpen(false)}
          >
            Início
          </Link>
          <Link
            href="/cursos"
            aria-current={pathname.startsWith("/cursos") ? "page" : undefined}
            onClick={() => setOpen(false)}
          >
            Cursos
          </Link>
          <Link href="/#sobre" onClick={() => setOpen(false)}>
            Sobre
          </Link>
          <button
            className="button button-outline student-button"
            disabled
            title="Área do aluno não disponível nesta demonstração"
          >
            Área do aluno <span className="demo-label">Em demonstração</span>
          </button>
        </nav>
      </div>
    </header>
  );
}

export function Footer() {
  return (
    <footer className="site-footer">
      <div className="container">
        <div className="footer-grid">
          <div>
            <Link href="/" aria-label="SESI Indústria — início">
              <img
                src={asset("logo-sesi-industria-white.svg")}
                alt="SESI Indústria"
                width="188"
                height="19"
              />
            </Link>
            <p>
              Conhecimento que transforma
              <br />a indústria e a sua carreira.
            </p>
          </div>
          <div>
            <h2>Navegação</h2>
            <Link href="/">Início</Link>
            <Link href="/cursos">Cursos</Link>
            <Link href="/#sobre">Sobre</Link>
          </div>
          <div>
            <h2>Explore o catálogo</h2>
            <Link href="/cursos?categoria=matematica">Matemática</Link>
            <Link href="/cursos?categoria=ciencias-da-natureza">
              Ciências da Natureza
            </Link>
            <Link href="/cursos?categoria=linguagens">Linguagens</Link>
          </div>
          <div>
            <h2>Sobre esta experiência</h2>
            <p>Projeto demonstrativo para o teste técnico SESI-SP.</p>
            <p>
              Cursos e depoimentos fictícios. Sem matrícula ou área do aluno.
            </p>
          </div>
        </div>
        <div className="footer-bottom">
          <p>
            <img
              src={asset("logo-sesi.svg")}
              alt="SESI"
              width="57"
              height="19"
            />{" "}
            Catálogo educacional · Demonstração
          </p>
          <div
            className="social-marks"
            aria-label="Ícones institucionais ilustrativos, sem links para redes sociais"
          >
            {["linkedIn", "instagram", "facebook"].map((name) => (
              <img
                key={name}
                src={asset(`icon-${name}-24px.svg`)}
                alt=""
                width="20"
                height="20"
              />
            ))}
          </div>
        </div>
      </div>
    </footer>
  );
}

export function CourseCard({
  curso,
  featured = false,
}: {
  curso: CursoResumo;
  featured?: boolean;
}) {
  const icon =
    curso.categoria.codigo === "ciencias-da-natureza"
      ? "ciencias-natureza"
      : curso.categoria.codigo === "matematica"
        ? "matematica"
        : "linguagens";
  return (
    <article
      className={`course-card course-crop-${curso.id % 3} ${featured ? "featured-card" : ""}`}
    >
      <Link href={`/cursos/${curso.id}`} className="course-card-link">
        <div className="course-image">
          <img
            src={curso.imagem}
            alt=""
            loading="lazy"
            width="640"
            height="360"
          />
          <span className={`category-tag category-${curso.categoria.codigo}`}>
            <img
              src={asset(`icon-${icon}-20px.svg`)}
              alt=""
              width="16"
              height="16"
            />
            {curso.categoria.nome}
          </span>
        </div>
        <div className="course-content">
          {!featured && (
            <span className={`status status-${curso.status.codigo}`}>
              {curso.status.nome}
            </span>
          )}
          <h3>{curso.nomeCompleto}</h3>
          <p>{curso.resumo}</p>
          {!featured && (
            <>
              <div className="course-meta">
                <span>{curso.modalidade.nome}</span>
                <span>{curso.cargaHoraria} horas</span>
              </div>
              <span className="course-link">
                Conhecer curso <Arrow />
              </span>
            </>
          )}
        </div>
      </Link>
    </article>
  );
}

export function Loading({ label = "Carregando cursos…" }: { label?: string }) {
  return (
    <div className="state-panel" role="status">
      <span className="loader" aria-hidden="true" />
      <p>{label}</p>
    </div>
  );
}
export function ErrorState({
  error,
  retry,
  clearFilters,
}: {
  error: unknown;
  retry: () => void;
  clearFilters?: () => void;
}) {
  const invalid = error instanceof ApiError && error.status === 400;
  return (
    <div className="state-panel error-panel" role="alert">
      <span className="state-symbol" aria-hidden="true">
        !
      </span>
      <h2>
        {invalid
          ? "Revise os filtros informados"
          : "Não foi possível carregar o conteúdo"}
      </h2>
      <p>
        {invalid
          ? "Um dos valores da URL não é válido. Limpe os filtros para começar novamente."
          : "A conexão com o catálogo está indisponível. Tente novamente em instantes."}
      </p>
      <button
        className="button button-dark"
        onClick={invalid && clearFilters ? clearFilters : retry}
      >
        {invalid && clearFilters ? "Limpar filtros e ver cursos" : "Tentar novamente"}
      </button>
    </div>
  );
}

const benefits = [
  {
    icon: "01-certificacao",
    title: "Certificação SESI",
    text: "Conhecimento e qualificação para os desafios do mercado industrial.",
  },
  {
    icon: "02-professores",
    title: "Professores especialistas",
    text: "Aprendizado conectado à experiência e à prática profissional.",
  },
  {
    icon: "03-suporte",
    title: "Suporte contínuo",
    text: "Acompanhamento para uma jornada de aprendizagem com autonomia.",
  },
  {
    icon: "04-flexibilidade",
    title: "Flexibilidade",
    text: "Diferentes modalidades para conhecer e planejar seus estudos.",
  },
];
export function Benefits({ detail = false }: { detail?: boolean }) {
  return (
    <div>
      <div className={`benefits-grid ${detail ? "four-columns" : ""}`}>
        {benefits.slice(0, detail ? 4 : 3).map((b) => (
          <div className="benefit" key={b.icon}>
            <img
              src={asset(`${b.icon}-${detail ? "bege" : "red"}.svg`)}
              alt=""
              width="48"
              height="48"
            />
            <h3>{b.title}</h3>
            <p>{b.text}</p>
          </div>
        ))}
      </div>
      <p className="institutional-note">
        Apresentação institucional ilustrativa. Este projeto não oferece aulas,
        suporte ou certificados.
      </p>
    </div>
  );
}
export function Accordion({
  title,
  children,
}: {
  title: string;
  children: ReactNode;
}) {
  return (
    <details className="accordion">
      <summary>
        {title}
        <span className="accordion-symbol" aria-hidden="true" />
      </summary>
      <div className="accordion-content">{children}</div>
    </details>
  );
}

export function Testimonials({ beige = false }: { beige?: boolean }) {
  const rail = useRef<HTMLDivElement>(null);
  const [bounds, setBounds] = useState({ previous: false, next: false });
  useEffect(() => {
    const element = rail.current;
    if (!element) return;
    const update = () =>
      setBounds(scrollBounds(element.scrollLeft, element.clientWidth, element.scrollWidth));
    const observer = new ResizeObserver(update);
    observer.observe(element);
    element.addEventListener("scroll", update, { passive: true });
    update();
    return () => {
      observer.disconnect();
      element.removeEventListener("scroll", update);
    };
  }, []);
  const items = [
    {
      name: "Lucas Pereira",
      role: "Analista na Empresa X",
      text: "A experiência do curso foi incrível! As aulas foram muito bem estruturadas e o cronograma foi cuidadosamente planejado, facilitando muito o aprendizado.",
    },
    {
      name: "Mariana Costa",
      role: "Gestora na Empresa X",
      text: "Concluir o curso foi uma jornada enriquecedora. As aulas eram dinâmicas e o horário foi organizado para maximizar nosso tempo de estudo.",
    },
    {
      name: "Thiago Almeida",
      role: "Técnico na Empresa X",
      text: "O curso superou minhas expectativas! As aulas eram bem elaboradas e o planejamento do horário ajudou a manter o foco e a produtividade.",
    },
    {
      name: "Fernanda Silva",
      role: "Analista na Empresa X",
      text: "Finalizar o curso foi uma experiência motivadora. As aulas foram bem preparadas e o cronograma foi ideal para o aprendizado.",
    },
  ];
  return (
    <section
      className={`testimonials section ${beige ? "testimonials-beige" : ""}`}
      aria-labelledby="depoimentos-title"
    >
      <div className="container">
        <p className="eyebrow">Depoimentos ilustrativos</p>
        <h2 id="depoimentos-title">Quem fez recomenda</h2>
        <div
          className="testimonial-rail"
          ref={rail}
          tabIndex={0}
          aria-label="Depoimentos fictícios do protótipo"
        >
          {items.map((item) => (
            <figure className="testimonial" key={item.name}>
              <blockquote>“{item.text}”</blockquote>
              <figcaption>
                <strong>{item.name}</strong>
                <span>{item.role}</span>
                <span className="stars" aria-label="5 de 5 estrelas">
                  ★★★★★
                </span>
              </figcaption>
            </figure>
          ))}
        </div>
        <div className="rail-controls">
          <span>Conteúdo fictício do protótipo</span>
          <div>
            <button
              aria-label="Depoimentos anteriores"
              disabled={!bounds.previous}
              onClick={() => rail.current?.scrollBy({ left: -320 })}
            >
              ←
            </button>
            <button
              aria-label="Próximos depoimentos"
              disabled={!bounds.next}
              onClick={() => rail.current?.scrollBy({ left: 320 })}
            >
              →
            </button>
          </div>
        </div>
      </div>
    </section>
  );
}
export function CallToAction() {
  return (
    <section className="cta-section section">
      <div className="container cta-grid">
        <div>
          <h2>
            Pronto para{" "}
            <br />
            dar o próximo passo?
          </h2>
          <p>
            Explore conhecimentos que conectam educação, tecnologia e indústria.
            Encontre um curso para a sua trajetória.
          </p>
          <Link className="button button-white" href="/cursos">
            Conhecer cursos <Arrow />
          </Link>
        </div>
        <figure className="cta-photo">
          <img
            src={asset("video-thumb.png")}
            alt="Profissionais em um ambiente industrial"
            loading="lazy"
            width="640"
            height="360"
          />
          <figcaption>
            Imagem institucional · sem reprodução de vídeo
          </figcaption>
        </figure>
      </div>
    </section>
  );
}
