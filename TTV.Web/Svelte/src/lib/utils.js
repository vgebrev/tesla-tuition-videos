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
