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
  if (typeof selector === 'function') {
    return array.reduce((total, item) => total + selector(item), 0);
  }
  return array.reduce((total, item) => total + item, 0);
}

/**
 * Returns the number of days between two dates
 * @param {Date} from - the start date
 * @param {Date} [to] - the end date (default is current date and time)
 * @returns {number}
 */
export function daysBetween(from, to = new Date()) {
  const timeDifference = to - from; // Difference in milliseconds
  const daysDifference = timeDifference / (1000 * 60 * 60 * 24); // Convert milliseconds to days
  return Math.floor(daysDifference); // Round down to nearest whole number
}
