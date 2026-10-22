import { defineConfig, loadEnv } from 'vite';

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, '.', '');
  return {
    server: {
      port: 5173,
      strictPort: true,
      proxy: { '/api': { target: env.API_PROXY_TARGET || 'http://localhost:5080' }, '/health': { target: env.API_PROXY_TARGET || 'http://localhost:5080' } },
    },
  };
});
