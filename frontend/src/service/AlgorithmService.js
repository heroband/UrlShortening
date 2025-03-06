import ApiService from './ApiService';

class AlgorithmService {
  static async create(data) {
    return ApiService.request('Algorithm/create', 'POST', data);
  }

  static async get() {
    return ApiService.request('Algorithm/get', 'GET');
  }

  static async update(data) {
    return ApiService.request(`Algorithm/update`, 'PUT', data);
  }

  static async delete() {
    return ApiService.request(`Algorithm/delete`, 'DELETE');
  }
}
