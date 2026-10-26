import { useEffect, useRef, useState } from "react";
import { carouselTarget } from "./carouselTarget";
import { scrollBounds } from "./scrollBounds";
import { carouselPosition } from "./carouselMotion";

export function useCourseCarousel(count: number) {
  const rail = useRef<HTMLDivElement>(null);
  const stopAnimation = useRef<(() => void) | null>(null);
  const [bounds, setBounds] = useState({ previous: false, next: false });
  useEffect(() => {
    const element = rail.current;
    if (!element) return;
    const update = () => setBounds(scrollBounds(
      element.scrollLeft, element.clientWidth, element.scrollWidth,
    ));
    const observer = new ResizeObserver(update);
    observer.observe(element);
    if (element.firstElementChild) observer.observe(element.firstElementChild);
    element.addEventListener("scroll", update, { passive: true });
    const interrupt = () => stopAnimation.current?.();
    element.addEventListener("pointerdown", interrupt, { passive: true });
    element.addEventListener("wheel", interrupt, { passive: true });
    element.addEventListener("keydown", interrupt);
    update();
    return () => {
      observer.disconnect();
      interrupt();
      element.removeEventListener("scroll", update);
      element.removeEventListener("pointerdown", interrupt);
      element.removeEventListener("wheel", interrupt);
      element.removeEventListener("keydown", interrupt);
    };
  }, [count]);

  function move(direction: "previous" | "next" | "start" | "end") {
    const element = rail.current;
    if (!element) return;
    stopAnimation.current?.();
    const cards = Array.from(element.children) as HTMLElement[];
    const origin = cards[0]?.offsetLeft ?? 0;
    const target = carouselTarget(element.scrollLeft, element.scrollWidth - element.clientWidth,
      cards.map((card) => card.offsetLeft - origin), direction);
    const reduced = window.matchMedia("(prefers-reduced-motion: reduce)");
    if (reduced.matches) {
      element.scrollTo({ left: target, behavior: "instant" });
      return;
    }
    const start = element.scrollLeft;
    const started = performance.now();
    const duration = direction === "start" || direction === "end" ? 850 : 400;
    const previousBehavior = element.style.scrollBehavior;
    const previousSnap = element.style.scrollSnapType;
    element.style.scrollBehavior = "auto";
    element.style.scrollSnapType = "none";
    let frame = 0;
    const stop = () => {
      cancelAnimationFrame(frame);
      element.style.scrollBehavior = previousBehavior;
      element.style.scrollSnapType = previousSnap;
      stopAnimation.current = null;
    };
    stopAnimation.current = stop;
    const step = (now: number) => {
      const elapsed = reduced.matches ? duration : now - started;
      element.scrollTo({ left: carouselPosition(start, target, elapsed, duration), behavior: "instant" });
      if (elapsed < duration) frame = requestAnimationFrame(step);
      else stop();
    };
    frame = requestAnimationFrame(step);
  }
  return { rail, bounds, move };
}
