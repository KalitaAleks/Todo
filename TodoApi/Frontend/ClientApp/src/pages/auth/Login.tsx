// client/src/pages/AUTH/Login.tsx
import { useAUTH } from '../../hooks/useAuth';
import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';

export const Login = () => {
    const { login } = useAUTH();
    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(false);
    const navigate = useNavigate();

    const { register, handleSubmit, formState: { errors } } = useForm<{
        email: string;
        password: string;
    }>();

    const onSubmit = async (data: { email: string; password: string }) => {
        try {
            setIsLoading(true);
            setError(null);
            await login(data.email, data.password);
            navigate('/'); // Перенаправление после успешного входа
        } catch (err) {
            setError('Неверный email или пароль');
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="max-w-md mx-auto mt-8 p-6 bg-white rounded-lg shadow-md">
            <h2 className="text-2xl font-bold mb-4 text-center">Вход</h2>
            {error && <div className="mb-4 p-2 bg-red-100 text-red-700 rounded">{error}</div>}

            <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
                <div>
                    <label className="block text-sm font-medium mb-1">Email</label>
                    <input
                        {...register('email', { required: 'Email обязателен' })}
                        type="email"
                        className="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                    />
                    {errors.email && <p className="text-red-500 text-sm">{errors.email.message}</p>}
                </div>

                <div>
                    <label className="block text-sm font-medium mb-1">Пароль</label>
                    <input
                        {...register('password', { required: 'Пароль обязателен' })}
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
                    {isLoading ? 'Вход...' : 'Войти'}
                </button>
            </form>

            <div className="mt-4 text-center">
                <span className="text-gray-600">Нет аккаунта? </span>
                <Link to="/register" className="text-blue-600 hover:underline">Зарегистрироваться</Link>
            </div>
        </div>
    );
};
export default Login;