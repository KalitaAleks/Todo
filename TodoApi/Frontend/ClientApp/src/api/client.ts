import axios from "axios";
import { User } from "../types";

// Создаем экземпляр Axios с базовыми настройками
const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL || "/api", // Используем переменную окружения или прокси
    headers: {
        "Content-Type": "application/json",
    },
});

// Перехватчик для добавления JWT-токена в заголовки
api.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");
    if (token) {
        config.headers.AUTHorization = `Bearer ${token}`;
    }
    return config;
});

// Перехватчик для обработки ошибок
api.interceptors.response.use(
    (response) => response,
    (error) => {
        // Если 401 ошибка (UnAUTHorized) - редирект на логин
        if (error.response?.status === 401) {
            localStorage.removeItem("token");
            window.location.href = "/login";
        }
        return Promise.reject(error);
    }
);

// Типизированные методы API
export const AUTHApi = {
    register: (data: { email: string; password: string }) =>
        api.post<User>("/api/AUTH/register", data),

    login: (data: { email: string; password: string }) =>
        api.post<{ user: User; token: string }>("/api/AUTH/login", data),
};

export const todoApi = {
    getTodos: () => api.get("/api/todos"),
    createTodo: (title: string) => api.post("/api/todos", { title }),
    deleteTodo: (id: string) => api.delete(`/api/todos/${id}`),
};

export default api;