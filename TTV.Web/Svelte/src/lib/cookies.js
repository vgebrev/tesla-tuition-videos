/**
 * Returns true if a cookie with the given name and value exists
 * @param {string} name
 * @param {string} withValue
 * @returns {boolean}
 */
export function getCookie(name, withValue = 'true') {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) {
    const cookieValue = parts.pop();
    if (cookieValue !== undefined) {
      return cookieValue.split(';').shift() === withValue;
    }
  }
  return false;
}

/**
 * Sets a cookie with the given name, value and days to expire
 * @param {string} name
 * @param {string} value
 * @param {number} days
 */
export function setCookie(name, value, days) {
  const date = new Date();
  date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
  const expires = `expires=${date.toUTCString()}`;
  document.cookie = `${name}=${value}; ${expires}; path=/; Secure; SameSite=Lax`;
}
