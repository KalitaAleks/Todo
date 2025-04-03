﻿// client/src/components/Loader.tsx
import React from 'react';

const Loader = () => (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
        <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-blue-500"></div>
    </div>
);

export default Loader; // Добавлен экспорт