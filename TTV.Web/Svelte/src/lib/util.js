/**
 * Groups an array of objects by a key returned by keyGetter.
 * @param {Array<T>}array
 * @param {function(T): *} keyGetter
 * @returns {Object<*, Array<T>>}
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
 * Returns the number of days between the provided date and the current date.
 * @param {Date} from
 * @param {Date} [to]
 * @returns {number}
 */
export function daysBetween(from, to = new Date()) {
  const timeDifference = to - from; // Difference in milliseconds
  const daysDifference = timeDifference / (1000 * 60 * 60 * 24); // Convert milliseconds to days
  return Math.floor(daysDifference); // Round down to nearest whole number
}
