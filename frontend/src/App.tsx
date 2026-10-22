import { useEffect } from "react";
import { Footer, Header } from "./components";
import { Link, useLocation } from "./navigation";
import { Catalog } from "./pages/Catalog";
import { Details } from "./pages/Details";
import { Home } from "./pages/Home";

export function App() {
  const { pathname, search, hash } = useLocation();
  useEffect(() => {
    document.title = `${pathname === "/" ? "Educação para a indústria" : pathname === "/cursos" ? "Catálogo de cursos" : "Detalhes do curso"} | SESI Indústria`;
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
      <Header key={pathname} home={pathname === "/"} pathname={pathname} />
      <main id="conteudo" tabIndex={-1} className={detail ? "detail-page" : ""}>
        {pathname === "/" ? (
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
      <Footer />
    </>
  );
}
