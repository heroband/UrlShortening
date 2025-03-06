import ApiService from './ApiService';

class AlgorithmService {
  static async create(data) {
    const token = localStorage.getItem('token');
    const headers = { Authorization: `Bearer ${token}` };
    return ApiService.request('Algorithm/', 'POST', data, headers);
  }

  static async get() {
    return ApiService.request('Algorithm/', 'GET');
  }

  static async update(data) {
    const token = localStorage.getItem('token');
    const headers = { Authorization: `Bearer ${token}` };
    return ApiService.request(`Algorithm/`, 'PUT', data, headers);
  }

  static async delete() {
    const token = localStorage.getItem('token');
    const headers = { Authorization: `Bearer ${token}` };
    return ApiService.request(`Algorithm/`, 'DELETE', null, headers);
  }
}

export default AlgorithmService;
