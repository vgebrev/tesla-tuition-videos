import { writable } from 'svelte/store';
import { api } from '$lib/api.js';
import { defaultPageSize } from '$lib/config.js';

/** @type {import('$lib/types').MyDiscountVouchersStoreState} */
const initialState = {
  isLoading: false,
  error: { isError: false, message: '' },
  pageInfo: { skip: 0, take: defaultPageSize, count: 0 },
  discountVouchers: null
};

/** My Discount Vouchers store
 *  @type {import('svelte/store').Writable<import('$lib/types').MyDiscountVouchersStoreState>}*/
export const myDiscountVouchersStore = writable(initialState);

/** My Discount Vouchers actions
 * @type {{getDiscountVouchers: ((function(): Promise<void>)|*)}}
 */
export const myDiscountVouchersActions = {
  getDiscountVouchers
};

/**
 * Retrieve the discount vouchers for the authenticated user
 * @param {number|null} [skip]
 * @param {number|null} [take]
 * @returns {Promise<void>}
 */
async function getDiscountVouchers(skip = null, take = null) {
  myDiscountVouchersStore.update((state) => {
    state.isLoading = true;
    return state;
  });
  try {
    const queryString = `skip=${skip !== null ? skip : ''}&take=${take !== null ? take : ''}`;
    const res = await api.get(`/discount-vouchers/own?${queryString}`);
    const discountVouchers = await res.json();
    myDiscountVouchersStore.update((state) => {
      state.discountVouchers = discountVouchers.items || [];
      state.pageInfo = discountVouchers.pageInfo;
      state.isLoading = false;
      return state;
    });
  } catch (e) {
    console.error(e);
    myDiscountVouchersStore.update((state) => {
      state.isLoading = false;
      return state;
    });
  }
}
