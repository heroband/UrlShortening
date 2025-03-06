import ApiService from './ApiService';

class AuthService {
  static async login(data) {
    return ApiService.request('Account/login', 'POST', data);
  }
  static async register(data) {
    return ApiService.request('Account/register', 'POST', data);
  }
}

export default AuthService;
