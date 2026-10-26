import { useCallback, useEffect, useReducer, useRef, useState } from "react";
import type { ReactNode } from "react";
import type { LearningLesson } from "./avaModel";
import { activityReady } from "./avaModel";
import { asset } from "./components";
import { initialQuiz, quizReducer } from "./quizState";

const demoDuration = 12;

function DemoVideo({ enabled, completed, onEnded }: { enabled: boolean; completed: boolean; onEnded: () => void }) {
  const [playing, setPlaying] = useState(false);
  const [elapsed, setElapsed] = useState(completed ? demoDuration : 0);
  const ended = elapsed >= demoDuration;
  useEffect(() => {
    if (!playing || !enabled || ended) return;
    let last = performance.now();
    const pause = () => { if (document.hidden) setPlaying(false); };
    const timer = window.setInterval(() => {
      const now = performance.now();
      const seconds = Math.min((now - last) / 1000, 0.5);
      last = now;
      if (!document.hidden) setElapsed(previous => Math.min(demoDuration, previous + seconds));
    }, 100);
    document.addEventListener("visibilitychange", pause);
    return () => { clearInterval(timer); document.removeEventListener("visibilitychange", pause); };
  }, [playing, enabled, ended]);
  useEffect(() => { if (ended) { setPlaying(false); onEnded(); } }, [ended, onEnded]);
  return <section className="ava-demo-video" aria-label="Prévia demonstrativa">
    <h2>Prévia demonstrativa</h2>
    <div className={`ava-player ${playing ? "is-playing" : ""} ${ended ? "has-ended" : ""}`}>
      <img className="ava-player-poster" src={asset("video-thumb.png")} alt="Profissionais em uma indústria" width="640" height="360" />
      <div className="ava-player-top"><img src={asset("logo-sesi.svg")} alt="SESI" width="57" height="19" /><span>Demonstração</span></div>
      {!playing && !ended && <button className="ava-player-central" type="button" disabled={!enabled} aria-label="Reproduzir prévia" onClick={() => setPlaying(true)}><svg viewBox="0 0 24 24" width="32" height="32" aria-hidden="true"><path d="m9 5 11 7-11 7Z" fill="currentColor" /></svg></button>}
      {ended && <div className="ava-player-finished" role="status"><svg viewBox="0 0 24 24" width="32" height="32" fill="none" stroke="currentColor" strokeWidth="2" aria-hidden="true"><path d="m5 12 4 4L19 6" /></svg><span>Reprodução concluída</span></div>}
      <div className="ava-video-controls">
        <button type="button" className="ava-player-toggle" disabled={!enabled || ended} aria-label={ended ? "Prévia concluída" : playing ? "Pausar prévia" : "Continuar prévia"} onClick={() => setPlaying(previous => !previous)}>
          <svg viewBox="0 0 24 24" width="22" height="22" aria-hidden="true">{ended ? <path d="m5 12 4 4L19 6" fill="none" stroke="currentColor" strokeWidth="2" /> : playing ? <path d="M6 5h4v14H6zm8 0h4v14h-4Z" fill="currentColor" /> : <path d="m8 5 11 7-11 7Z" fill="currentColor" />}</svg>
        </button>
        <progress aria-label="Tempo de reprodução da prévia" value={elapsed} max={demoDuration} />
        <span className="ava-player-time">0:{String(Math.floor(elapsed)).padStart(2, "0")} <span>/ 0:12</span></span>
        <span className="ava-player-muted" title="Simulação sem áudio" aria-label="Sem áudio"><svg viewBox="0 0 24 24" width="20" height="20" fill="none" stroke="currentColor" strokeWidth="1.6" aria-hidden="true"><path d="M10 5 5 9H2v6h3l5 4Zm5 4 6 6m0-6-6 6" /></svg></span>
      </div>
    </div>
    <p className="ava-activity-note">Imagem ilustrativa com reprodução simulada, sem áudio.</p>
  </section>;
}

export function LessonActivity({ lesson, completed, onComplete, onRevoke, children }: {
  lesson: LearningLesson;
  completed: boolean;
  onComplete: (evidence: { read: boolean; answered: boolean; watched: boolean }) => void;
  onRevoke: () => void;
  children: ReactNode;
}) {
  const endOfText = useRef<HTMLDivElement>(null);
  const [read, setRead] = useState(completed);
  const [watched, setWatched] = useState(completed);
  const [quiz, dispatch] = useReducer(quizReducer, completed && lesson.question
    ? { answer: lesson.question.correct, status: "correct", attempts: 0 } : initialQuiz);
  const answered = quiz.status === "correct";
  useEffect(() => {
    const marker = endOfText.current;
    if (!marker || completed) return;
    const check = () => {
      const rect = marker.getBoundingClientRect();
      if (!document.hidden && rect.bottom <= window.innerHeight) setRead(true);
    };
    const observer = new IntersectionObserver(check, { threshold: 1 });
    observer.observe(marker);
    const frame = requestAnimationFrame(check);
    window.addEventListener("scroll", check, { passive: true });
    window.addEventListener("resize", check);
    return () => { observer.disconnect(); cancelAnimationFrame(frame); window.removeEventListener("scroll", check); window.removeEventListener("resize", check); };
  }, [completed]);
  const ready = activityReady(lesson, { read, answered, watched });
  const videoEnded = useCallback(() => setWatched(true), []);
  return <>
    <div className="ava-reading-content">{children}<div ref={endOfText} className="ava-reading-end" /></div>
    <p className="ava-activity-note" role="status">{read ? "Leitura concluída." : "Leia até o fim para concluir a aula."}</p>
    {lesson.video && <DemoVideo enabled={read && !completed} completed={completed} onEnded={videoEnded} />}
    {lesson.question && <form className="ava-question" onSubmit={event => {
      event.preventDefault();
      if (!read || completed) return;
      dispatch({ type: "validate", correct: lesson.question!.correct });
    }}>
      <div className="ava-question-heading"><span>PRATIQUE O QUE APRENDEU</span><h2>Confira seu aprendizado</h2><p>Escolha uma alternativa e confira sua resposta.</p></div>
      <fieldset disabled={!read || completed || answered}><legend>{lesson.question.prompt}</legend>
        {lesson.question.options.map((option, index) => <label className={`ava-answer ${quiz.answer === index ? "is-selected" : ""} ${answered && quiz.answer === index ? "is-correct" : ""} ${quiz.status === "incorrect" && quiz.answer === index ? "is-incorrect" : ""}`} key={option}>
          <input type="radio" name={`answer-${lesson.key}`} value={index} checked={quiz.answer === index} aria-describedby={quiz.status !== "idle" ? "ava-answer-feedback" : undefined} onChange={() => dispatch({ type: "select", answer: index })} />
          <span className="ava-answer-letter" aria-hidden="true">{String.fromCharCode(65 + index)}</span><span>{option}</span>
        </label>)}
        {!answered && <button className="button button-dark" type="submit" disabled={quiz.answer === null}>Conferir resposta</button>}
      </fieldset>
      <div id="ava-answer-feedback" aria-live="polite" aria-atomic="true">
        {quiz.status !== "idle" && <div className={`ava-answer-feedback ${answered ? "is-correct" : "is-incorrect"}`}>
          <svg viewBox="0 0 24 24" width="28" height="28" fill="none" stroke="currentColor" strokeWidth="1.7" aria-hidden="true"><circle cx="12" cy="12" r="9" />{answered ? <path d="m7 12 3 3 7-7" /> : <path d="M12 6v7m0 3v2" />}</svg>
          <div><strong>{answered ? "Boa! Você acertou." : "Ainda não. Vamos revisar?"}</strong>
            <p>{answered ? lesson.question.explanation : "Revise o conteúdo e escolha outra alternativa."}</p>
            <span>{answered ? completed ? "Atividade concluída." : "Agora você pode concluir a aula." : `Tentativa ${quiz.attempts} · tente novamente.`}</span>
          </div>
        </div>}
      </div>
      {answered && !completed && <button className="ava-quiz-retry" type="button" onClick={() => dispatch({ type: "retry" })}>Responder novamente</button>}
    </form>}
    <div className="ava-lesson-actions">
      <button type="button" className="button button-dark" disabled={!completed && !ready} onClick={() => completed ? onRevoke() : onComplete({ read, answered, watched })}>
        {completed ? "Refazer aula" : "Concluir aula"}
      </button>
      <span role="status">{completed ? "Aula concluída." : !read ? "Conclua a leitura para continuar." : lesson.video && !watched ? "Acompanhe a prévia até o fim." : lesson.question && !answered ? "Responda à questão para concluir." : "Tudo pronto para concluir a aula."}</span>
    </div>
  </>;
}
