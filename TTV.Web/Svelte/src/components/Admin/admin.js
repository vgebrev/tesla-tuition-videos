import { writable } from 'svelte/store';
import { api } from '$lib/api.js';
import { defaultErrorMessage } from '$lib/config.js';

/** @type {import('$lib/types').AdminStoreState} */
const initialState = {
  isLoading: false,
  error: { isError: false, message: '' },
  discountVouchers: null,
  issuedVoucher: null
};

/** Admin state store
 * @type {import('svelte/store').Writable<import('$lib/types').AdminStoreState>} */
export const adminStore = writable(initialState);

/** Admin actions
 */
export const adminActions = {
  getDiscountVouchers,
  issueDiscountVoucher
};

async function getDiscountVouchers() {
  adminStore.update((state) => {
    state.isLoading = true;
    return state;
  });
  try {
    const response = await api.get('/discount-vouchers');
    const discountVouchers = await response.json();
    adminStore.update((state) => {
      state.isLoading = false;
      state.discountVouchers = discountVouchers;
      return state;
    });
  } catch (e) {
    console.error(e);
    adminStore.update((state) => {
      state.isLoading = false;
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
}

/**
 * Issue a discount voucher
 * @param {number} amount
 * @param {?string} expirationDate
 * @param {?string} note
 * @returns {Promise<void>}
 */
async function issueDiscountVoucher(amount, expirationDate, note) {
  adminStore.update((state) => {
    state.isLoading = true;
    return state;
  });
  try {
    const response = await api.post('/discount-vouchers', { amount, expirationDate, note });
    const issuedVoucher = await response.json();
    adminStore.update((state) => {
      state.isLoading = false;
      state.issuedVoucher = issuedVoucher;
      return state;
    });
    await getDiscountVouchers();
  } catch (e) {
    console.error(e);
    adminStore.update((state) => {
      state.isLoading = false;
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
}
