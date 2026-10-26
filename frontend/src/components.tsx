import { useEffect, useRef, useState } from "react";
import type { ReactNode } from "react";
import type { CursoResumo } from "./api/catalogo";
import { ApiError } from "./api/catalogo";
import { Link } from "./navigation";
import { scrollBounds } from "./scrollBounds";
import { courseSectionHref } from "./courseAnchor";
import { featuredImage } from "./featuredPresentation";

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
            href={courseSectionHref}
            onClick={() => setOpen(false)}
          >
            Cursos
          </Link>
          <Link href="/#sobre" onClick={() => setOpen(false)}>
            Sobre
          </Link>
          <Link
            className="button button-outline student-button"
            href="/ava"
            onClick={() => setOpen(false)}
          >
            Área do aluno
          </Link>
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
            <p title="Contato e endereço ilustrativos do protótipo">
              contato@sesindustria.com.br
              <br />Av. Paulista, 1313
            </p>
          </div>
          <div>
            <h2>Navegação</h2>
            <Link href="/">Início</Link>
            <span className="footer-placeholder" aria-disabled="true" title="Não disponível nesta demonstração">Certificação</span>
            <Link href="/cursos">Cursos</Link>
            <Link href="/#sobre">Sobre</Link>
            <span className="footer-placeholder" aria-disabled="true" title="Não disponível nesta demonstração">Blog</span>
          </div>
          <div>
            <h2>Cursos populares</h2>
            {["Cultura Organizacional", "Ferramentas Google para Produtividade", "Excel Básico ao Avançado", "Trabalho em Equipe e Colaboração", "Gestão do Tempo e Produtividade", "Comunicação Assertiva", "Inteligência Emocional no Trabalho"].map(name => (
              <span key={name} className="footer-placeholder" aria-disabled="true" title="Curso ilustrativo do Figma, não disponível no catálogo">{name}</span>
            ))}
          </div>
          <div>
            <h2>Suporte</h2>
            {["Termos de Uso", "Política de Privacidade", "Trabalhe Conosco", "Fale Conosco", "Central de Ajuda"].map(name => (
              <span key={name} className="footer-placeholder" aria-disabled="true" title="Não disponível nesta demonstração">{name}</span>
            ))}
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
            Todos os direitos reservados.
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
      data-course-id={featured ? curso.id : undefined}
    >
      <Link href={`/cursos/${curso.id}`} className="course-card-link">
        <div className="course-image">
          <img
            src={featured ? featuredImage(curso) : curso.imagem}
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
    text: "Cursos com a melhor certificação do mercado industrial reconhecidos pelo SESI.",
  },
  {
    icon: "02-professores",
    title: "Professores especialistas",
    text: "Contamos com professores reconhecidos por sua excelência no mercado.",
  },
  {
    icon: "03-suporte",
    title: "Suporte contínuo",
    text: "Você não está sozinho! Acompanhamento online durante todo o curso.",
  },
  {
    icon: "04-flexibilidade",
    title: "Flexibilidade",
    text: "Turmas presenciais e online com horários que se adaptam à sua rotina.",
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
  if (beige) {
    items[3].text = "Finalizar o curso foi uma experiência motivadora. As aulas foram muito bem preparadas e o cronograma foi ideal para o aprendizado.";
    items.push({
      name: "Gabriel Santos",
      role: "Analista na Empresa X",
      text: "A conclusão do curso foi uma etapa muito gratificante. As aulas eram bem organizadas e o horário foi pensado para facilitar nosso aprendizado.",
    });
  }
  const move = (direction: -1 | 1) => {
    const element = rail.current;
    if (!element) return;
    const maximum = element.scrollWidth - element.clientWidth;
    const target = beige
      ? direction === 1
        ? bounds.next ? maximum : 0
        : bounds.previous ? 0 : maximum
      : element.scrollLeft + direction * 320;
    element.scrollTo({
      left: target,
      behavior: window.matchMedia("(prefers-reduced-motion: reduce)").matches ? "instant" : "smooth",
    });
  };
  return (
    <section
      className={`testimonials section ${beige ? "testimonials-beige" : ""}`}
      aria-labelledby="depoimentos-title"
    >
      <div className="container">
        <p className="eyebrow">Depoimentos</p>
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
          <span></span>
          <div>
            <button
              aria-label="Depoimentos anteriores"
              disabled={beige ? !bounds.previous && !bounds.next : !bounds.previous}
              onClick={() => move(-1)}
            >
              <img src={asset("arrow-navigation.svg")} alt="" width="48" height="48" />
            </button>
            <button
              aria-label="Próximos depoimentos"
              disabled={beige ? !bounds.previous && !bounds.next : !bounds.next}
              onClick={() => move(1)}
            >
              <img src={asset("arrow-navigation-1.svg")} alt="" width="48" height="48" />
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
            transformar sua carreira?
          </h2>
          <p>
            Junte-se a milhares de profissionais que já estão transformando a
            indústria através dos nossos cursos. Invista no seu futuro hoje mesmo!
          </p>
          <Link className="button button-white" href="/cursos" aria-label="Matricule-se — consultar catálogo demonstrativo, sem matrícula disponível">
            Matricule-se <img src={asset("arrow-navigation-1.svg")} alt="" width="24" height="24" />
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
          <button className="cta-play" type="button" disabled aria-label="Vídeo demonstrativo indisponível" title="Imagem do protótipo: vídeo não disponível nesta demonstração">
            <svg viewBox="0 0 24 24" width="32" height="32" aria-hidden="true"><path d="M8 4 20 12 8 20Z" fill="currentColor" /></svg>
          </button>
          <figcaption className="sr-only">
            Imagem institucional · sem reprodução de vídeo
          </figcaption>
        </figure>
      </div>
    </section>
  );
}
