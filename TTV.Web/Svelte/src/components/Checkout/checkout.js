import { writable } from 'svelte/store';
import { api } from '$lib/api.js';
import { defaultErrorMessage } from '$lib/config.js';

/** @type {CheckoutStoreState} */
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
 * @type {Writable<CheckoutStoreState>}
 */
export const checkoutStore = writable(initialState);

/** Checkout actions */
export const checkoutActions = {
  getOrder,
  cancelOrder
};

/**
 * Get an order by id.
 * @param {number} orderId
 * @param {Order} currentOrder
 * @returns {Promise<void>}
 */
async function getOrder(orderId, currentOrder) {
  checkoutStore.update((state) => ({
    ...state,
    isLoading: true,
    applyVoucherResult: null,
    cancelOrderResult: null,
    initiatePaymentResult: null
  }));
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
    checkoutStore.update((state) => ({
      ...state,
      order,
      error: { isError: false, message: '' }
    }));
  } catch (e) {
    console.error(e);
    checkoutStore.update((state) => ({
      ...state,
      error: { isError: true, message: defaultErrorMessage }
    }));
  }
  checkoutStore.update((state) => ({ ...state, isLoading: false }));
}

/**
 * Cancel an order
 * @param orderId
 * @returns {Promise<void>}
 */
async function cancelOrder(orderId) {
  console.log('TODO: Cancel Order', orderId);
}
