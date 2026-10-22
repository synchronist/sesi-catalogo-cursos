import { defineConfig } from "@neon/config/v1";

export default defineConfig({
  // Somente PostgreSQL existente; nenhum serviço adicional ou expiração de branch.
  auth: false,
});
