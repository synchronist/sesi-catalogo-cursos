export interface QuizState {
  answer: number | null;
  status: "idle" | "incorrect" | "correct";
  attempts: number;
}
export type QuizAction =
  | { type: "select"; answer: number }
  | { type: "validate"; correct: number }
  | { type: "retry" };

export const initialQuiz: QuizState = { answer: null, status: "idle", attempts: 0 };

export function quizReducer(state: QuizState, action: QuizAction): QuizState {
  if (action.type === "retry") return { ...initialQuiz, attempts: state.attempts };
  // Uma resposta validada só pode mudar por uma ação explícita de refazer.
  if (state.status === "correct") return state;
  if (action.type === "select") return { ...state, answer: action.answer, status: "idle" };
  if (state.answer === null) return state;
  return { ...state, status: state.answer === action.correct ? "correct" : "incorrect", attempts: state.attempts + 1 };
}
