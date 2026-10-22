import test from "node:test";
import assert from "node:assert/strict";
import { catalogUrl, readQuery, latestRequest } from "../src/catalogQuery.ts";

test("URL preserva busca e todos os filtros, incluindo caracteres especiais", () => {
  const query = {
    busca: "Energia & movimento",
    categoria: "matematica",
    modalidade: "online",
    status: "em-breve",
  };
  const url = catalogUrl(query);
  assert.deepEqual(readQuery(url.slice(url.indexOf("?"))), query);
});
test("limpeza remove query string e valores vazios não são enviados", () => {
  assert.equal(catalogUrl({ busca: "  ", status: "" }), "/cursos");
  assert.deepEqual(
    readQuery("?busca=%20Medidas%20&categoria=&desconhecido=1"),
    { busca: "Medidas" },
  );
});
test("voltar e recarregar podem reconstruir o estado exclusivamente pela URL", () => {
  const url =
    "/cursos?busca=Medidas&categoria=matematica&modalidade=online&status=disponivel";
  assert.equal(catalogUrl(readQuery(url.split("?")[1])), url);
});
test("requisição nova aborta a anterior e invalida respostas fora de ordem", async () => {
  const requests = latestRequest();
  const first = requests.begin();
  const second = requests.begin();
  const applied = [];
  await Promise.resolve().then(() => {
    if (second.isCurrent()) applied.push("nova");
  });
  await Promise.resolve().then(() => {
    if (first.isCurrent()) applied.push("antiga");
  });
  assert.equal(first.signal.aborted, true);
  assert.deepEqual(applied, ["nova"]);
});
test("cancelamento na desmontagem impede atualizações posteriores", () => {
  const requests = latestRequest();
  const request = requests.begin();
  requests.cancel();
  assert.equal(request.signal.aborted, true);
  assert.equal(request.isCurrent(), false);
});
