import { useEffect, useRef, useState } from "react";
import { latestRequest } from "./catalogQuery";
export type Resource<T> =
  | { state: "loading" }
  | { state: "success"; data: T }
  | { state: "error"; error: unknown };
export function useResource<T>(
  key: string,
  load: (signal: AbortSignal) => Promise<T>,
) {
  const loader = useRef(load);
  loader.current = load;
  const requests = useRef(latestRequest());
  const [attempt, setAttempt] = useState(0);
  const [result, setResult] = useState<{ key: string; value: Resource<T> }>({
    key,
    value: { state: "loading" },
  });
  useEffect(() => {
    const request = requests.current.begin();
    setResult({ key, value: { state: "loading" } });
    loader
      .current(request.signal)
      .then((data) => {
        if (request.isCurrent())
          setResult({ key, value: { state: "success", data } });
      })
      .catch((error: unknown) => {
        if (request.isCurrent())
          setResult({ key, value: { state: "error", error } });
      });
    const pending = requests.current;
    return () => pending.cancel();
  }, [key, attempt]);
  return {
    result:
      result.key === key ? result.value : ({ state: "loading" } as Resource<T>),
    retry: () => setAttempt((a) => a + 1),
  };
}
