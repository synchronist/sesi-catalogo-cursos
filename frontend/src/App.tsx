import { useEffect } from "react";
import { Footer, Header } from "./components";
import { Link, useLocation } from "./navigation";
import { Catalog } from "./pages/Catalog";
import { Details } from "./pages/Details";
import { Home } from "./pages/Home";
import { scrollToCourseSection } from "./courseAnchor";
import { Learning, LearningCourses } from "./pages/Learning";

export function App() {
  const { pathname, search, hash } = useLocation();
  const learning = /^\/ava\/([^/]+)\/?$/.exec(pathname);
  useEffect(() => {
    document.title = `${pathname.startsWith("/ava") ? "Ambiente de aprendizagem" : pathname === "/" ? "Educação para a indústria" : pathname === "/cursos" ? "Catálogo de cursos" : "Detalhes do curso"} | SESI Indústria`;
    if (scrollToCourseSection(hash)) return;
    if (hash) document.getElementById(hash.slice(1))?.scrollIntoView();
    else window.scrollTo(0, 0);
    document.getElementById("conteudo")?.focus({ preventScroll: true });
  }, [pathname, hash]);
  const detail = /^\/cursos\/([^/]+)\/?$/.exec(pathname);
  return (
    <>
      <a href="#conteudo" className="skip-link">
        Ir para o conteúdo
      </a>
      {!learning && <Header key={pathname} home={pathname === "/"} pathname={pathname} />}
      <main id="conteudo" tabIndex={-1} className={learning ? "ava-page" : detail ? "detail-page" : ""}>
        {learning ? <Learning id={learning[1]} search={search} /> : pathname === "/ava" ? <LearningCourses /> : pathname === "/" ? (
          <Home />
        ) : pathname === "/cursos" ? (
          <Catalog search={search} />
        ) : detail ? (
          <Details id={detail[1]} />
        ) : (
          <div className="container section state-panel">
            <h1>Página não encontrada</h1>
            <Link className="button button-red" href="/cursos">
              Ir para o catálogo
            </Link>
          </div>
        )}
      </main>
      {!learning && <Footer />}
    </>
  );
}
