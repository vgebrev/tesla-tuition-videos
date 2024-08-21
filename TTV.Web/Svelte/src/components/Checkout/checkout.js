import { writable } from 'svelte/store';
import { api } from '$lib/api.js';
import { defaultErrorMessage } from '$lib/config.js';

/** @type {import('$lib/types').CheckoutStoreState} */
const initialState = {
  isLoading: false,
  voucherCode: '',
  applyVoucherResult: null,
  cancelOrderResult: null,
  initiatePaymentResult: null,
  order: null,
  error: { isError: false, message: '' }
};

/**
 * Checkout state store.
 * @type {Writable<import('$lib/types').CheckoutStoreState>}
 */
export const checkoutStore = writable(initialState);

/** Checkout actions */
export const checkoutActions = {
  getOrder,
  cancelOrder,
  applyVoucher
};

/**
 * Get an order by id.
 * @param {int} orderId
 * @param {Order} currentOrder
 * @returns {Promise<void>}
 */
async function getOrder(orderId, currentOrder) {
  checkoutStore.update((state) => {
    state.isLoading = true;
    state.applyVoucherResult = null;
    state.cancelOrderResult = null;
    state.initiatePaymentResult = null;
    return state;
  });
  try {
    let order = currentOrder;
    if (!order || order.id !== orderId) {
      const response = await api.get(`/orders/${orderId}`);
      if (response.status === 404) {
        order = null;
      } else if (response.ok) {
        order = await response.json();
      }
    }
    checkoutStore.update((state) => {
      state.order = order;
      state.error = { isError: false, message: '' };
      return state;
    });
  } catch (e) {
    console.error(e);
    checkoutStore.update((state) => {
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
  checkoutStore.update((state) => {
    state.isLoading = false;
    return state;
  });
}

/**
 * Cancel an order
 * @param {int} orderId
 * @returns {Promise<void>}
 */
async function cancelOrder(orderId) {
  checkoutStore.update((state) => {
    state.isLoading = true;
    return state;
  });

  try {
    const response = await api.delete(`/orders/${orderId}`);
    const cancelOrderResult = await response.json();
    checkoutStore.update((state) => {
      state.cancelOrderResult = cancelOrderResult;
      state.order = cancelOrderResult.value;
      state.error = { isError: false, message: '' };
      return state;
    });
  } catch (e) {
    console.error(e);
    checkoutStore.update((state) => {
      state.cancelOrderResult = null;
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
  checkoutStore.update((state) => {
    state.isLoading = false;
    return state;
  });
}

/**
 * Apply a discount voucher to an order
 * @param {int} orderId
 * @param {string|null} code
 * @returns {Promise<void>}
 */
async function applyVoucher(orderId, code) {
  checkoutStore.update((state) => {
    state.isLoading = true;
    state.applyVoucherResult = null;
    return state;
  });
  try {
    const response = await api.put(`/discount-vouchers/${code}/order/${orderId}`, {});
    const orderResult = await response.json();
    checkoutStore.update((state) => {
      if (state.order && orderResult.isSuccess) {
        state.order = orderResult.value;
      }
      state.applyVoucherResult = {
        isSuccess: orderResult.isSuccess,
        message: orderResult.message,
        value: orderResult.value?.appliedDiscounts.slice(-1)[0]
      };
      state.error = { isError: false, message: '' };
      return state;
    });
  } catch (e) {
    console.error(e);
    checkoutStore.update((state) => {
      state.applyVoucherResult = null;
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
  checkoutStore.update((state) => {
    state.isLoading = false;
    return state;
  });
}
