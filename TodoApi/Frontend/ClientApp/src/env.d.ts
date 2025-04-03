// src/env.d.ts
/// <reference types="vite/client" />

interface ImportMetaEnv {
    readonly VITE_API_BASE_URL: string;
    // Добавьте другие переменные окружения, которые используете
}

interface ImportMeta {
    readonly env: ImportMetaEnv;
}