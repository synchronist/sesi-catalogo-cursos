export const courseSectionId = "principais-cursos";
export const courseSectionHref = `/#${courseSectionId}`;

export function scrollToCourseSection(hash: string): boolean {
  if (hash !== `#${courseSectionId}`) return false;
  const section = document.getElementById(courseSectionId);
  if (!section) return false;
  section.focus({ preventScroll: true });
  section.scrollIntoView({
    behavior: window.matchMedia("(prefers-reduced-motion: reduce)").matches
      ? "auto"
      : "smooth",
    block: "start",
  });
  return true;
}
