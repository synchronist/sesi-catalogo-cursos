import { useEffect, useMemo, useRef, useState } from "react";
import type { CursoDetalhe } from "../api/catalogo";
import { ApiError, catalogoApi } from "../api/catalogo";
import { asset, ErrorState, Loading } from "../components";
import { Link } from "../navigation";
import { useResource } from "../useResource";
import { featuredImage, featuredOrder } from "../featuredPresentation";
import { avaHref, browserProgressRepository, completeLesson, courseCompleted, filterLearningModules, learningModules, progressPercent, revokeCompletion } from "../avaModel";
import type { LearningProgress } from "../avaModel";
import { learningText, practicesContent } from "../avaContent";
import { LessonActivity } from "../LessonActivity";
import "../ava.css";

const progressRepository = browserProgressRepository({
  getItem: key => window.localStorage.getItem(key),
  setItem: (key, value) => window.localStorage.setItem(key, value),
});

export function LearningCourses() {
  const { result, retry } = useResource("ava-courses", signal => catalogoApi.listar({}, signal));
  return <div className="container section learning-courses">
    <nav className="breadcrumb" aria-label="Caminho"><Link href="/">Início</Link><span>/</span><span aria-current="page">Área do aluno</span></nav>
    <h1>Meus estudos</h1>
    <p className="learning-intro">Escolha um curso e continue seus estudos.</p>
    <p className="learning-notice">Ambiente demonstrativo. Seu progresso fica salvo neste navegador.</p>
    {result.state === "loading" ? <Loading /> : result.state === "error" ? <ErrorState error={result.error} retry={retry} /> :
      <div className="learning-course-grid">{featuredOrder(result.data.items).map(course => <Link className="learning-course" key={course.id} href={avaHref(course.id)}>
        <img src={featuredImage(course)} alt="" width="400" height="180" />
        <span>{course.categoria.nome}</span><h2>{course.nomeCompleto}</h2><span>Acessar curso →</span>
      </Link>)}{!result.data.items.length && <p>Nenhum curso disponível.</p>}</div>}
  </div>;
}

export function Learning({ id, search }: { id: string; search: string }) {
  const { result, retry } = useResource(`ava:${id}`, signal => catalogoApi.detalhar(Number(id), signal));
  if (result.state === "loading") return <div className="container section"><Loading label="Carregando curso…" /></div>;
  if (result.state === "error") return <div className="container section">
    {result.error instanceof ApiError && [400, 404].includes(result.error.status)
      ? <div className="state-panel"><h1>Curso não encontrado</h1><Link href="/ava" className="button button-dark">Escolher outro curso</Link></div>
      : <ErrorState error={result.error} retry={retry} />}
  </div>;
  return <LearningWorkspace key={result.data.curso.id} detail={result.data} search={search} />;
}

function LearningWorkspace({ detail, search }: { detail: CursoDetalhe; search: string }) {
  const { curso } = detail;
  const modules = useMemo(() => learningModules(detail), [detail]);
  const lessons = useMemo(() => modules.flatMap(module => module.lessons), [modules]);
  const keys = useMemo(() => lessons.map(lesson => lesson.key), [lessons]);
  const selectedKey = new URLSearchParams(search).get("aula") ?? "introducao";
  const selected = lessons.find(lesson => lesson.key === selectedKey);
  const activeModule = modules.find(module => module.lessons.some(lesson => lesson.key === selectedKey));
  const [progress, setProgress] = useState<LearningProgress>(() => progressRepository.load(curso.id, keys));
  const [saveFailed, setSaveFailed] = useState(false);
  const [query, setQuery] = useState("");
  const [menuOpen, setMenuOpen] = useState(false);
  const [opened, setOpened] = useState(() => new Set([activeModule?.key ?? "introduction"]));
  const menuButton = useRef<HTMLButtonElement>(null);
  const article = useRef<HTMLElement>(null);
  const percent = progressPercent(progress, keys);
  const filtered = filterLearningModules(modules, query);
  const content = practicesContent(curso.nomeCompleto, curso.id === 1);
  const currentIndex = lessons.findIndex(lesson => lesson.key === selectedKey);
  const allCompleted = courseCompleted(progress, keys);
  useEffect(() => {
    setMenuOpen(false);
    if (activeModule) setOpened(previous => new Set([...previous, activeModule.key]));
    article.current?.focus({ preventScroll: true });
    window.scrollTo({ top: 0, behavior: "instant" });
  }, [selectedKey, activeModule]);
  function persist(next: LearningProgress) {
    setProgress(next);
    setSaveFailed(!progressRepository.save(curso.id, next));
  }
  return <div className="ava-shell" onKeyDown={event => {
    if (event.key === "Escape" && menuOpen) { setMenuOpen(false); menuButton.current?.focus(); }
  }}>
    <div className="ava-mobile-toolbar"><Link href={`/cursos/${curso.id}`} aria-label="Voltar aos detalhes do curso">←</Link>
      <button ref={menuButton} type="button" aria-controls="ava-sidebar" aria-expanded={menuOpen} onClick={() => setMenuOpen(!menuOpen)}>Conteúdo do curso <span aria-hidden="true">☰</span></button>
    </div>
    <aside id="ava-sidebar" className={`ava-sidebar ${menuOpen ? "is-open" : ""}`} aria-label="Conteúdo do curso">
      <Link className="ava-back" href={`/cursos/${curso.id}`} aria-label="Voltar aos detalhes do curso">←</Link>
      <div className="ava-course-heading"><span>Seu curso</span><h2>{curso.nomeCompleto}</h2>
        <div className="ava-progress"><progress max="100" value={percent} aria-label="Aulas concluídas" /><span aria-live="polite">{percent}%</span></div>
      </div>
      <div className="ava-search"><label className="sr-only" htmlFor="ava-search">Buscar no conteúdo do curso</label>
        <input id="ava-search" type="search" value={query} onChange={event => setQuery(event.target.value)} />
        <svg aria-hidden="true" viewBox="0 0 24 24" width="20" height="20"><circle cx="10" cy="10" r="6" fill="none" stroke="currentColor"/><path d="m15 15 5 5" stroke="currentColor"/></svg>
      </div>
      <nav aria-label="Módulos e aulas">
        {filtered.map(module => {
          const index = modules.findIndex(item => item.key === module.key);
          const expanded = Boolean(query.trim()) || opened.has(module.key);
          return <div className="ava-module" key={module.key}>
            <button type="button" title={module.title} aria-label={index ? `${index + 1}. Módulo ${index}: ${module.title}` : "1. Introdução"} aria-expanded={expanded} aria-controls={`ava-${module.key}`} onClick={() => setOpened(previous => {
              const next = new Set(previous); if (next.has(module.key)) next.delete(module.key); else next.add(module.key); return next;
            })}>{index + 1}. {index ? `Módulo ${index}` : module.title}</button>
            <ul id={`ava-${module.key}`} hidden={!expanded}>{module.lessons.map(lesson => <li key={lesson.key}>
              <Link href={avaHref(curso.id, lesson.key)} aria-current={selectedKey === lesson.key ? "page" : undefined}>
                <span className={`ava-lesson-state ${progress.completed.includes(lesson.key) ? "is-complete" : ""}`} aria-hidden="true">
                  {progress.completed.includes(lesson.key) ? "✓" : selectedKey === lesson.key ? "▸" : ""}
                </span><span><span className="sr-only">{progress.completed.includes(lesson.key) ? "Concluída: " : ""}</span>{index + 1}.{modules[index].lessons.findIndex(item => item.key === lesson.key) + 1} {lesson.title}</span>
              </Link>
            </li>)}</ul>
          </div>;
        })}
        {!filtered.length && <div className="ava-search-empty" role="status"><p>Nenhuma aula encontrada.</p><button type="button" onClick={() => setQuery("")}>Limpar busca</button></div>}
      </nav>
      <p className="ava-local-note">Ambiente demonstrativo.<br />Progresso salvo neste navegador.</p>
    </aside>
    <article className="ava-article" ref={article} tabIndex={-1} aria-labelledby="ava-lesson-title">
      <div className="ava-banner"><img src={curso.id === 1 ? asset("banner-ava.png") : featuredImage(curso)} alt="" width="1940" height="1080" />
        <img className="ava-banner-logo" src={asset("logo-sesi.svg")} alt="SESI" width="57" height="19" />
      </div>
      {!selected ? <div className="state-panel"><h1 id="ava-lesson-title">Aula não encontrada</h1><Link href={avaHref(curso.id)}>Voltar ao conteúdo do curso</Link></div> : <>
        <h1 id="ava-lesson-title">{selected.title}</h1>
        <LessonActivity key={`${selected.key}-${progress.completed.includes(selected.key)}`} lesson={selected} completed={progress.completed.includes(selected.key)} onComplete={evidence => persist(completeLesson(progress, selected, keys, evidence))} onRevoke={() => persist(revokeCompletion(progress, selected.key, keys))}>
        {selected.kind === "practices" ? <>
          {content.practices.map(text => <p key={text}>{text}</p>)}
          <h2>Recomendações</h2>{content.recommendations.map(text => <p key={text}>{text}</p>)}
        </> : selected.kind === "extras" ? <>
          <p>Revise suas anotações e os principais temas do curso.</p>
          <h2>Roteiro de revisão</h2><ul>{detail.modulos.map(module => <li key={module.id}>{module.titulo}</li>)}</ul>
          <p>Responda à questão abaixo para verificar o que aprendeu.</p>
        </> : <>
          <p>{learningText(selected.description) || "O conteúdo desta aula ainda não está disponível."}</p>
          {selected.kind === "lesson" && <><h2>Neste módulo</h2><p>{activeModule?.title}</p></>}
        </>}
        </LessonActivity>
        <p className="ava-activity-note" role="status">{saveFailed ? "Não foi possível salvar o progresso neste navegador. Ele será mantido até você sair da página." : allCompleted ? "Parabéns! Você concluiu todas as aulas." : "Conclua todas as aulas para finalizar o curso."}</p>
        <nav className="ava-pagination" aria-label="Navegação entre aulas">
          {currentIndex > 0 ? <Link href={avaHref(curso.id, lessons[currentIndex - 1].key)}>← Aula anterior</Link> : <span />}
          {currentIndex < lessons.length - 1 ? <Link href={avaHref(curso.id, lessons[currentIndex + 1].key)}>Próxima aula →</Link>
            : allCompleted ? <span>Curso concluído</span> : <Link href={avaHref(curso.id, lessons.find(lesson => !progress.completed.includes(lesson.key))!.key)}>Revisar aulas pendentes →</Link>}
        </nav>
      </>}
    </article>
  </div>;
}
