// serverOption.ts
import type { ServerOptions } from 'vite';

export const serverOptions: ServerOptions = {
    proxy: {
        '/api': {
            target: 'http://localhost:5273',
            changeOrigin: true,
            rewrite: (path) => path.replace(/^\/api/, '')
        }
    }
};