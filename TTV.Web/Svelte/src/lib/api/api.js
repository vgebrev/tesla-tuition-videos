import { config } from '$lib/config.js';
import { userManager } from '$lib/user-manager.js';

async function apiFetch(url, options = {}) {
  const user = await userManager.getUser();
  if (user) {
    options.headers = {
      ...options.headers,
      Authorization: `Bearer ${user.access_token}`
    };
  }

  const response = await fetch(config.api.baseUrl + url, options);
  if (!response.ok) {
    throw new Error(response.statusText);
  }

  return response.json();
}

export const api = {
  get: (url, options) => apiFetch(url, { ...options, method: 'GET' }),
  post: (url, body, options) =>
    apiFetch(url, { ...options, method: 'POST', body: JSON.stringify(body) }),
  put: (url, body, options) =>
    apiFetch(url, { ...options, method: 'PUT', body: JSON.stringify(body) }),
  delete: (url, options) => apiFetch(url, { ...options, method: 'DELETE' })
};
