import ApiService from './ApiService';

class UrlService {
  static async create(data) {
    return ApiService.request('Url/create', 'POST', data);
  }

  static async getAll() {
    return ApiService.request('Url/getAll', 'GET');
  }

  static async getById(id) {
    return ApiService.request(`Url/${id}`, 'GET');
  }

  static async delete(id) {
    return ApiService.request(`Url/${id}`, 'DELETE');
  }
}

export default UrlService;
