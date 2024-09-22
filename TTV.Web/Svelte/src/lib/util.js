/**
 * Groups an array of objects by a key returned by keyGetter.
 * @param {Array<*>}array
 * @param {function(*): *} keyGetter
 * @returns {Object<*, Array<*>>}
 *
 * @example
 * const data = [
 *   {id: 1, category: 'fruit'},
 *   {id: 2, category: 'vegetable'},
 *   {id: 3, category: 'fruit'},
 * ];
 * const grouped = groupBy(data, item => item.category);
 * // grouped = {
 * //   fruit: [{id: 1, category: 'fruit'}, {id: 3, category: 'fruit'}],
 * //   vegetable: [{id: 2, category: 'vegetable'}]
 * // }
 */
export function groupBy(array, keyGetter) {
  return array.reduce((result, item) => {
    const key = keyGetter(item);
    if (!result[key]) {
      result[key] = [];
    }
    result[key].push(item);
    return result;
  }, {});
}

/**
 * Sums the elements of an array.
 *
 * @template T
 * @param {T[]} array - The array of elements to sum.
 * @param {(item: T) => number} [selector] - Optional. A function to project each element of the array into a numeric value.
 * @returns {number} - The sum of the projected elements or the sum of the elements if no selector is provided.
 *
 * @example
 * const data = [
 *   {id: 1, amount: 10},
 *   {id: 2, amount: 20},
 *   {id: 3, amount: 30},
 * ];
 * const total = sum(data, item => item.amount); // total = 60
 */
export function sum(array, selector) {
  if (!selector) return 0;
  return array.reduce((total, item) => total + selector(item), 0);
}

/**
 * @template {{id: number}} T
 * Merges two arrays, where objects with matching `id` fields are taken from the second array.
 * @param {T[]} array1 - The first array of objects.
 * @param {T[]} array2 - The second array of objects.
 * @returns {T[]} - A new array that is the union of the two arrays.
 *
 * @example
 * const array1 = [
 *   { id: 1, name: 'Item 1' },
 *   { id: 2, name: 'Item 2' },
 *   { id: 3, name: 'Item 3' }
 * ];
 *
 * const array2 = [
 *   { id: 2, name: 'Updated Item 2' },
 *   { id: 4, name: 'Item 4' }
 * ];
 *
 * const mergedArray = mergeArrays(array1, array2);
 * // mergedArray = [
 * //   { id: 1, name: 'Item 1' },
 * //   { id: 2, name: 'Updated Item 2' },
 * //   { id: 3, name: 'Item 3' },
 * //   { id: 4, name: 'Item 4' }
 * // ]
 */
export function mergeArrays(array1, array2) {
  const map = new Map();
  array1.forEach((item) => map.set(item.id, item));
  array2.forEach((item) => map.set(item.id, item));
  return Array.from(map.values());
}

/**
 * @template {{id: number}} T
 * Merges two arrays based on the `id` property, with items from `newItems`
 * only being added if their `id` is already present in `array`.
 *
 * @param {T[]} array - The first array of objects.
 * @param {T[]} newItems - The second array of objects.
 * @returns {T[]} A merged array where items from `newItems` are included only if their `id` exists in `array`.
 *
 * @example
 * const array1 = [{ id: 1, name: 'Alice' }, { id: 2, name: 'Bob' }];
 * const array2 = [{ id: 2, name: 'Bobby' }, { id: 3, name: 'Charlie' }];
 * const result = mergeArrays(array1, array2);
 * console.log(result); // [{ id: 1, name: 'Alice' }, { id: 2, name: 'Bobby' }]
 */
export function updateArray(array, newItems) {
  const map = new Map();
  array.forEach((item) => map.set(item.id, item));
  newItems.forEach((item) => {
    if (map.has(item.id)) {
      map.set(item.id, item);
    }
  });
  return Array.from(map.values());
}

/**
 * Check if the given date is in the past.
 * @param {Date | string | null} date
 */
export function isPast(date) {
  if (!date) return false;
  let today = new Date();
  today.setHours(0, 0, 0, 0);
  return new Date(date) < today;
}

/**
 * Returns the number of days between two dates
 * @param {Date} from - the start date
 * @param {Date} [to] - the end date (default is current date and time)
 * @returns {number}
 */
export function daysBetween(from, to = new Date()) {
  const timeDifference = to.getTime() - from.getTime();
  const daysDifference = timeDifference / (1000 * 60 * 60 * 24);
  return Math.floor(daysDifference);
}

/** Formats a date as dd-mmm-yyyy
 * @param {?Date | string} date
 * @returns {string}
 *
 * @example
 * const date = new Date('2021-12-31');
 * console.log(formatDate(date)); // '31-Dec-2021'
 */
export function formatDate(date) {
  if (!date) return '';
  date = new Date(date);
  const day = String(date.getDate()).padStart(2, '0');
  const month = date.toLocaleString('default', { month: 'short' });
  const year = date.getFullYear();
  return `${day}-${month}-${year}`;
}

/**
 * Formats a date as dd-mmm-yyyy hh:mm
 * @param {?Date | string} date
 * @returns {string}
 *
 * @example
 * const date = new Date('2021-12-31 23:59');
 * console.log(formatDate(date)); // '31-Dec-2021 23:59'
 */
export function formatDateTime(date) {
  if (!date) return '';
  date = new Date(date);
  const day = String(date.getDate()).padStart(2, '0');
  const month = date.toLocaleString('default', { month: 'short' });
  const year = date.getFullYear();
  const hours = String(date.getHours()).padStart(2, '0');
  const minutes = String(date.getMinutes()).padStart(2, '0');
  return `${day}-${month}-${year} ${hours}:${minutes}`;
}

/**
 * Returns a query string of all object properties
 * @param {Record<string, any>} o
 * @returns {string}
 */
export function toQueryString(o) {
  return Object.keys(o)
    .map((key) => encodeURIComponent(key) + '=' + encodeURIComponent(o[key] == null ? '' : o[key]))
    .join('&');
}
