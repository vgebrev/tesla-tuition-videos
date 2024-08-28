import { writable } from 'svelte/store';
import { api } from '$lib/api.js';
import { defaultErrorMessage } from '$lib/config.js';

export const paymentMethods = {
  bankTransfer: 1,
  payfast: 2
};

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
 * @type {import('svelte/store').Writable<import('$lib/types').CheckoutStoreState>}
 */
export const checkoutStore = writable(initialState);

/** Checkout actions */
export const checkoutActions = {
  getOrder,
  cancelOrder,
  applyVoucher,
  initiatePayment
};

/**
 * Get an order by id.
 * @param {number} orderId
 * @param {import('$lib/types').Order|null} currentOrder
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
 * @param {number} orderId
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
 * @param {number} orderId
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

/**
 * Open the PayFast modal
 * @param {string} paymentId - The payment external identifier, provided by the PayFast API
 * @param {number} orderId
 */
function payfastModal(paymentId, orderId) {
  window.payfast_do_onsite_payment({
    uuid: paymentId,
    return_url: `${window.location.origin}/order-complete/${orderId}`,
    cancel_url: `${window.location.origin}/payment-cancel/${orderId}`
  });
}

/**
 * Initiate payment for an order
 * @param {number} orderId
 * @param {import('$lib/types').PaymentMethod} paymentMethod
 * @returns {Promise<void>}
 */
async function initiatePayment(orderId, paymentMethod) {
  checkoutStore.update((state) => {
    state.isLoading = true;
    return state;
  });
  try {
    const response = await api.post(`/payments`, { orderId, paymentMethod });
    const initiatePaymentResult = await response.json();
    checkoutStore.update((state) => {
      state.initiatePaymentResult = initiatePaymentResult;
      state.error = { isError: false, message: '' };
      return state;
    });
    if (
      initiatePaymentResult.isSuccess &&
      initiatePaymentResult.value &&
      initiatePaymentResult.value.paymentMethod === paymentMethods.payfast
    ) {
      payfastModal(initiatePaymentResult.value.externalIdentifier, orderId);
    }
  } catch (e) {
    console.error(e);
    checkoutStore.update((state) => {
      state.initiatePaymentResult = null;
      state.error = { isError: true, message: defaultErrorMessage };
      state.isLoading = false;
      return state;
    });
  }
  if (paymentMethod === paymentMethods.payfast) return;
  checkoutStore.update((state) => {
    state.isLoading = false;
    return state;
  });
}
