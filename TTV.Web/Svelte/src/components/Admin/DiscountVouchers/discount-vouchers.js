import { writable } from 'svelte/store';
import { api } from '$lib/api.js';
import { defaultErrorMessage } from '$lib/config.js';

/** @type {import('$lib/types').AdminDiscountVouchersStore} */
const initialState = {
  isLoading: false,
  error: { isError: false, message: '' },
  discountVouchers: null,
  issuedVoucher: null
};

/** Admin state store
 * @type {import('svelte/store').Writable<import('$lib/types').AdminDiscountVouchersStoreState>} */
export const adminDiscountVouchersStore = writable(initialState);

/** Admin actions
 */
export const adminDiscountVouchersActions = {
  getDiscountVouchers,
  issueDiscountVoucher
};

async function getDiscountVouchers() {
  adminDiscountVouchersStore.update((state) => {
    state.isLoading = true;
    return state;
  });
  try {
    const response = await api.get('/discount-vouchers');
    const discountVouchers = await response.json();
    adminDiscountVouchersStore.update((state) => {
      state.isLoading = false;
      state.discountVouchers = discountVouchers;
      return state;
    });
  } catch (e) {
    console.error(e);
    adminDiscountVouchersStore.update((state) => {
      state.isLoading = false;
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
}

/**
 * Issue a discount voucher
 * @param {number} amount
 * @param {?string | ?Date} expirationDate
 * @param {?string} note
 * @returns {Promise<void>}
 */
async function issueDiscountVoucher(amount, expirationDate, note) {
  adminDiscountVouchersStore.update((state) => {
    state.isLoading = true;
    return state;
  });
  try {
    const response = await api.post('/discount-vouchers', { amount, expirationDate, note });
    const issuedVoucher = await response.json();
    adminDiscountVouchersStore.update((state) => {
      state.isLoading = false;
      state.issuedVoucher = issuedVoucher;
      return state;
    });
    await getDiscountVouchers();
  } catch (e) {
    console.error(e);
    adminDiscountVouchersStore.update((state) => {
      state.isLoading = false;
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
}
