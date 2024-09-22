import { writable, get } from 'svelte/store';
import { api } from '$lib/api.js';
import { defaultErrorMessage, defaultPageSize } from '$lib/config.js';

/** @type {import('$lib/types').AdminDiscountVouchersStoreState} */
const initialState = {
  isLoading: false,
  error: { isError: false, message: '' },
  pageInfo: { skip: 0, take: defaultPageSize, count: 0 },
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

/** Get discount vouchers
 *
 * @param {number|null} [skip]
 * @param {number|null} [take]
 * @returns {Promise<void>}
 */
async function getDiscountVouchers(skip = null, take = null) {
  adminDiscountVouchersStore.update((state) => {
    state.isLoading = true;
    return state;
  });
  try {
    const queryString = `skip=${skip !== null ? skip : ''}&take=${take !== null ? take : ''}`;
    const response = await api.get(`/discount-vouchers?${queryString}`);
    const discountVouchers = await response.json();
    adminDiscountVouchersStore.update((state) => {
      state.isLoading = false;
      state.discountVouchers = discountVouchers.items;
      state.pageInfo = discountVouchers.pageInfo;
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
    const pageInfo = get(adminDiscountVouchersStore).pageInfo;
    await getDiscountVouchers(pageInfo?.skip || 0, pageInfo?.take || defaultPageSize);
  } catch (e) {
    console.error(e);
    adminDiscountVouchersStore.update((state) => {
      state.isLoading = false;
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
}
