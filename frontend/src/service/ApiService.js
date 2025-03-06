class ApiService {
  static baseURL = 'http://localhost:5047/api';

  static async request(endpoint, method = 'GET', data = null, headers = {}) {
    const options = {
      method,
      headers: {
        'Content-Type': 'application/json',
        ...headers,
      },
    };

    if (data) {
      options.body = JSON.stringify(data);
    }

    try {
      const response = await fetch(`${this.baseURL}/${endpoint}`, options);
      const responseData = await response.json();

      if (!response.ok) {
        const errorMessage = responseData.errors
          ? responseData.errors.join(', ')
          : responseData.message || `Request error ${response.status}`;
        throw new Error(errorMessage);
      }
      return responseData;
    } catch (error) {
      console.error(`AuthService Error on ${endpoint}:`, error);
      throw error;
    }
  }
}

export default ApiService;
