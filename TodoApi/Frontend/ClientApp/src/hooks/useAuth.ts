// client/src/hooks/useAUTH.ts
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { User } from '../types';
import api from '../api/client';

export const useAUTH = () => {
    const navigate = useNavigate();
    const [user, setUser] = useState<User | null>(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const checkAUTH = async () => {
            try {
                const token = localStorage.getItem('token');
                if (token) {
                    const response = await api.get('/AUTH/me');
                    setUser(response.data);
                }
            } catch (error) {
                localStorage.removeItem('token');
            } finally {
                setIsLoading(false);
            }
        };

        checkAUTH();
    }, []);

    const login = async (email: string, password: string) => {
        const response = await api.post('/AUTH/login', { email, password });
        localStorage.setItem('token', response.data.token);
        setUser(response.data.user);
        navigate('/todos');
    };

    const logout = () => {
        localStorage.removeItem('token');
        setUser(null);
        navigate('/login');
    };

    return { user, isLoading, login, logout };
};