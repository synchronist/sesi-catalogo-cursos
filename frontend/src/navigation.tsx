import { useEffect, useState } from "react";
import type { AnchorHTMLAttributes, MouseEvent } from "react";
import { scrollToCourseSection } from "./courseAnchor";

export function navigate(href: string, replace = false) {
  if (replace) window.history.replaceState(null, "", href);
  else window.history.pushState(null, "", href);
  window.dispatchEvent(new PopStateEvent("popstate"));
}
export function useLocation() {
  const [location, setLocation] = useState(() => ({
    pathname: window.location.pathname,
    search: window.location.search,
    hash: window.location.hash,
  }));
  useEffect(() => {
    const update = () =>
      setLocation({
        pathname: window.location.pathname,
        search: window.location.search,
        hash: window.location.hash,
      });
    window.addEventListener("popstate", update);
    return () => window.removeEventListener("popstate", update);
  }, []);
  return location;
}
export function Link({
  href = "/",
  onClick,
  ...props
}: AnchorHTMLAttributes<HTMLAnchorElement>) {
  function follow(event: MouseEvent<HTMLAnchorElement>) {
    onClick?.(event);
    if (
      event.defaultPrevented ||
      event.button !== 0 ||
      event.metaKey ||
      event.ctrlKey ||
      event.shiftKey ||
      event.altKey ||
      props.target
    )
      return;
    event.preventDefault();
    // Repetir uma âncora (ou o link da página atual) também deve reposicionar a leitura.
    if (new URL(href, window.location.href).href === window.location.href) {
      const id = window.location.hash.slice(1);
      if (scrollToCourseSection(window.location.hash)) return;
      if (id) document.getElementById(id)?.scrollIntoView();
      else window.scrollTo(0, 0);
      return;
    }
    navigate(href);
  }
  return <a {...props} href={href} onClick={follow} />;
}
