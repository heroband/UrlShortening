import { createContext, useContext, useEffect, useState } from 'react';

import { jwtDecode } from 'jwt-decode';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decoded = jwtDecode(token);
        setUser({ id: decoded.sub, username: decoded.name, role: decoded.role });
      } catch (error) {
        console.log('Invalid token', error);
        localStorage.removeItem('token');
      }
    }
  }, []);

  const login = token => {
    localStorage.setItem('token', token);
    const decoded = jwtDecode(token);
    setUser({ id: decoded.sub, username: decoded.name, role: decoded.role });
  };

  const logout = () => {
    localStorage.removeItem('token');
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
