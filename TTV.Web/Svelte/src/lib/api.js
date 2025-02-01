import { config } from '$lib/config.js';
import { userManager } from '$lib/auth.js';

/**
 * Fetch data from the API with the user's access token
 * @param {RequestInfo | URL} url
 * @param {RequestInit} [options]
 * @returns {Promise<Response>}
 */
async function apiFetch(url, options = {}) {
  const user = await userManager.getUser();
  if (user) {
    options.headers = {
      ...options.headers,
      Authorization: `Bearer ${user.access_token}`
    };
  }

  return await fetch(config.api.baseUrl + url, options);
}

/**
 * API methods for REST-ful operations
 */
export const api = {
  /**
   * GET data from the API
   * @param {RequestInfo | URL} url
   * @param {RequestInit} [options]
   * @returns {Promise<Response>}
   */
  get: (url, options = {}) => apiFetch(url, { ...options, method: 'GET' }),

  /**
   * POST data to the API
   * @param {RequestInfo | URL} url
   * @param {Object} body
   * @param {RequestInit} [options]
   * @returns {Promise<Response>}
   */
  post: (url, body, options = {}) =>
    apiFetch(url, {
      ...options,
      headers: { 'Content-Type': 'application/json' },
      method: 'POST',
      body: JSON.stringify(body)
    }),

  /**
   * PUT data to the API
   * @param {RequestInfo | URL} url
   * @param {Object} body
   * @param {RequestInit} [options]
   * @returns {Promise<Response>}
   */
  put: (url, body, options = {}) =>
    apiFetch(url, {
      ...options,
      headers: { 'Content-Type': 'application/json' },
      method: 'PUT',
      body: JSON.stringify(body)
    }),

  /**
   * DELETE data from the API
   * @param {RequestInfo | URL} url
   * @param {RequestInit} [options]
   * @returns {Promise<Response>}
   */
  delete: (url, options = {}) => apiFetch(url, { ...options, method: 'DELETE' }),

  /**
   * Generic fetch from the API
   * @param url
   * @param options
   * @returns {Promise<Response>}
   */
  fetch: (url, options = {}) => apiFetch(url, { ...options })
};
