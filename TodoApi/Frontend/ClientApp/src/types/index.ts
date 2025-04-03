// client/src/types/index.ts
export type User = {
    id: string;
    email: string;
};

export type Todo = {
    id: string;
    title: string;
    isCompleted: boolean;
    createdAt: string;
};