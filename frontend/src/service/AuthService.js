import ApiService from './ApiService';

class AuthService {
  static async login(data) {
    const response = await ApiService.request('Account/login', 'POST', data);
    if (response.token) {
      localStorage.setItem('token', response.token);
    }
    return response;
  }
  static async register(data) {
    const response = await ApiService.request('Account/register', 'POST', data);
    if (response.token) {
      localStorage.setItem('token', response.token);
    }
    return response;
  }
}

export default AuthService;
