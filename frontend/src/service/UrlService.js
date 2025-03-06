import ApiService from './ApiService';

class UrlService {
  static async create(data) {
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('Token is missing or expired');
    }
    const headers = { Authorization: `Bearer ${token}` };
    return ApiService.request('Url/', 'POST', data, headers);
  }

  static async getAll() {
    return ApiService.request('Url/', 'GET');
  }

  static async getById(id) {
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('Token is missing or expired');
    }
    const headers = { Authorization: `Bearer ${token}` };
    return ApiService.request(`Url/${id}`, 'GET', null, headers);
  }

  static async delete(id) {
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('Token is missing or expired');
    }
    const headers = { Authorization: `Bearer ${token}` };
    return ApiService.request(`Url/${id}`, 'DELETE', null, headers);
  }

  static async redirect(shortUrl) {
    return ApiService.request(`Url/short/${shortUrl}`, 'GET');
  }
}

export default UrlService;
