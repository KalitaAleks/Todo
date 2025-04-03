// client/src/hooks/useTodo.ts
import { useState, useEffect } from 'react';
import api from '../api/client';
import { Todo } from '../types';

export const useTodo = () => {
    const [todos, setTodos] = useState<Todo[]>([]);
    const [loading, setLoading] = useState(false);

    const fetchTodos = async () => {
        setLoading(true);
        try {
            const response = await api.get('/todos');
            setTodos(response.data);
        } finally {
            setLoading(false);
        }
    };

    return { todos, loading, fetchTodos };
};
