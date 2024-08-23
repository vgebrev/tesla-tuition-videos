import { writable } from 'svelte/store';
import { defaultErrorMessage } from '$lib/config.js';
import { api } from '$lib/api.js';

/** @type {import('$lib/types').OrderCompleteStoreState} */
const initialState = {
  isLoading: false,
  completeOrderResult: null,
  order: null,
  error: { isError: false, message: '' }
};

/** Order complete store state
 * @type {import('svelte/store').Writable<import('$lib/types').OrderCompleteStoreState>} */
export const orderCompleteStore = writable(initialState);

/** Order complete actions */
export const orderCompleteActions = {
  completeOrder
};

/**
 * Complete an order
 * @param {number} orderId
 * @returns {Promise<void>}
 */
async function completeOrder(orderId) {
  orderCompleteStore.update((state) => {
    state.isLoading = true;
    return state;
  });
  try {
    const response = await api.put(`/orders/${orderId}/complete`, {});
    const completeOrderResult = await response.json();
    orderCompleteStore.update((state) => {
      state.completeOrderResult = completeOrderResult;
      state.order = completeOrderResult.value;
      state.error = { isError: !completeOrderResult.isSuccess, message: completeOrderResult.message };
      return state;
    });
  } catch (e) {
    console.error(e);
    orderCompleteStore.update((state) => {
      state.order = null;
      state.completeOrderResult = null;
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
  orderCompleteStore.update((state) => {
    state.isLoading = false;
    return state;
  });
}
