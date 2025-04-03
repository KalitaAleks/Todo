// client/src/pages/AUTH/Register.tsx
import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { useForm, SubmitHandler } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';

// Схема валидации с помощью Zod
const schema = z.object({
    email: z.string().email('Некорректный email'),
    password: z.string().min(6, 'Пароль должен содержать минимум 6 символов')
});

type FormData = z.infer<typeof schema>;

const Register = () => {
    const navigate = useNavigate();
    const [error, setError] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm<FormData>({
        resolver: zodResolver(schema)
    });

    const onSubmit: SubmitHandler<FormData> = async (data) => {
        try {
            setIsLoading(true);
            await axios.post('/api/AUTH/register', data);
            navigate('/login');
        } catch (err: any) {
            setError(err.response?.data?.message || 'Ошибка регистрации');
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="max-w-md mx-auto mt-8 p-6 bg-white rounded-lg shadow-md">
            <h2 className="text-2xl font-bold mb-4 text-center">Регистрация</h2>
            {error && <div className="mb-4 p-2 bg-red-100 text-red-700 rounded">{error}</div>}

            <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
                <div>
                    <label className="block text-sm font-medium mb-1">Email</label>
                    <input
                        {...register('email')}
                        type="email"
                        className="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                    />
                    {errors.email && <p className="text-red-500 text-sm">{errors.email.message}</p>}
                </div>

                <div>
                    <label className="block text-sm font-medium mb-1">Пароль</label>
                    <input
                        {...register('password')}
                        type="password"
                        className="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                    />
                    {errors.password && <p className="text-red-500 text-sm">{errors.password.message}</p>}
                </div>

                <button
                    type="submit"
                    disabled={isLoading}
                    className="w-full bg-blue-600 text-white p-2 rounded hover:bg-blue-700 disabled:bg-gray-400"
                >
                    {isLoading ? 'Загрузка...' : 'Зарегистрироваться'}
                </button>
            </form>

            <div className="mt-4 text-center">
                <span className="text-gray-600">Уже есть аккаунт? </span>
                <Link to="/login" className="text-blue-600 hover:underline">Войти</Link>
            </div>
        </div>
    );
};

export default Register;
