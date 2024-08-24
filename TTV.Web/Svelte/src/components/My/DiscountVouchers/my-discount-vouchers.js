import { writable } from 'svelte/store';
import { api } from '$lib/api.js';

/** @type {import('$lib/types').MyDiscountVouchersStoreState} */
const initialState = {
  isLoading: false,
  error: { isError: false, message: '' },
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
 * @returns {Promise<void>}
 */
async function getDiscountVouchers() {
  myDiscountVouchersStore.update((state) => {
    state.isLoading = true;
    return state;
  });

  try {
    const res = await api.get('/discount-vouchers/own');
    const discountVouchers = await res.json();
    myDiscountVouchersStore.update((state) => {
      state.discountVouchers = discountVouchers || [];
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
