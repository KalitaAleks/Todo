
export const serverOption = {
    proxy: {
        '/api': {
            target: 'http://localhost:7105',
            changeOrigin: true,
            rewrite: (path) => path.replace(/^\/api/, '')
        }
    }
};