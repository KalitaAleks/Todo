// client/src/pages/todos/TodoList.tsx
import { useEffect } from 'react';
import { useTodo } from '../../hooks/useTodo';
import TodoItem from '../../components/TodoItem';

export const TodoList = () => {
    const { todos, loading, fetchTodos } = useTodo();

    useEffect(() => {
        fetchTodos();
    }, []);

    return (
        <div className="max-w-2xl mx-auto mt-8">
            <h1 className="text-3xl font-bold mb-6">Мои задачи</h1>
            {loading ? (
                <div className="text-center">Загрузка...</div>
            ) : (
                <div className="space-y-4">
                    {todos.map(todo => (
                        <TodoItem key={todo.id} todo={todo} />
                    ))}
                </div>
            )}
        </div>
    );
};
export default TodoList;