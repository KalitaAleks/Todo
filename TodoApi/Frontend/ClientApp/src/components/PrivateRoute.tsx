// client/src/components/PrivateRoute.tsx
import { ReactNode } from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAUTH } from '../hooks/useAuth';
import Loader from './Loader';


interface PrivateRouteProps {
    children: ReactNode;
    redirectTo?: string;
}

const PrivateRoute = ({ children }: { children: ReactNode }) => {
    const { user, isLoading } = useAUTH();
    const location = useLocation();

    if (isLoading) {
        return <Loader />;
    }

    return user ? (
        <>{children}</>
    ) : (
        <Navigate to="/login" state={{ from: location }} replace />
    );
};

export default PrivateRoute;