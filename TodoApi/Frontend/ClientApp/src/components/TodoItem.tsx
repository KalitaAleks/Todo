// client/src/components/TodoItem.tsx
import { Todo } from '../types/index'; 

export const TodoItem = ({ todo }: { todo: Todo }) => {
    return (
        <div className="p-4 bg-white rounded-lg shadow-md flex items-center justify-between">
            <div className="flex items-center space-x-3">
                <input
                    type="checkbox"
                    checked={todo.isCompleted}
                    className="w-4 h-4"
                />
                <span className={todo.isCompleted ? 'line-through text-gray-500' : ''}>
                    {todo.title}
                </span>
            </div>
            <div className="flex space-x-2">
                <button className="text-blue-500 hover:text-blue-700">✏️</button>
                <button className="text-red-500 hover:text-red-700">🗑️</button>
            </div>
        </div>
    );
};
export default TodoItem;