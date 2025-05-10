import { API_ENDPOINTS, fetchApi } from './api';

interface LoginData {
    email: string;
    password: string;
}

interface RegisterData extends LoginData {
    confirmPassword?: string;
}

export async function login(data: LoginData) {
    try {
        const response = await fetchApi(API_ENDPOINTS.AUTH.LOGIN, {
            method: 'POST',
            body: JSON.stringify(data),
        });

        if (response.token) {
            localStorage.setItem('token', response.token);
            return response;
        }
        throw new Error('Login failed');
    } catch (error) {
        console.error('Login error:', error);
        throw error;
    }
}

export async function register(data: RegisterData) {
    // Remove confirmPassword before sending to API
    const { confirmPassword, ...registerData } = data;
    
    try {
        const response = await fetchApi(API_ENDPOINTS.AUTH.REGISTER, {
            method: 'POST',
            body: JSON.stringify(registerData),
        });
        return response;
    } catch (error) {
        console.error('Registration error:', error);
        throw error;
    }
}

export function logout() {
    localStorage.removeItem('token');
    // You can add additional cleanup here if needed
}
