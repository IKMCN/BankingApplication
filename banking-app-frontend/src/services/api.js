import axios from 'axios';

// Create axios instance with base configuration
const api = axios.create({
  baseURL: 'https://localhost:7038/api', // Replace with your .NET API URL
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor to add JWT token to requests
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor to handle token expiration
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      // Token expired or invalid
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

// Auth API functions
export const authAPI = {
  register: async (userData) => {
    const response = await api.post('/authentication/register', userData);
    return response.data;
  },
  
  login: async (credentials) => {
    const response = await api.post('/authentication/login', credentials);
    return response.data;
  }
};

// Accounts API functions
export const accountsAPI = {
  getAccounts: async () => {
    const response = await api.get('/accounts');
    return response.data;
  },
  
  deposit: async (accountId, amount) => {
    const response = await api.post(`/accounts/${accountId}/deposit`, { amount });
    return response.data;
  },
  
  withdraw: async (accountId, amount) => {
    const response = await api.post(`/accounts/${accountId}/withdraw`, { amount });
    return response.data;
  }
};

export default api;