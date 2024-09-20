import { writable, get } from 'svelte/store';
import { api } from '$lib/api.js';
import { defaultPageSize } from '$lib/config.js';

/** @type {import('$lib/types').MyOrdersStoreState} */
const initialState = {
  isLoading: false,
  error: { isError: false, message: '' },
  pageInfo: { skip: 0, take: defaultPageSize, total: 0 },
  orders: null
};

/** My Orders store
 * @type {import('svelte/store').Writable<import('$lib/types').MyOrdersStoreState>}
 */
export const myOrdersStore = writable(initialState);

/**
 * My Orders actions
 * @type {{getOrders: ((function(): Promise<void>)|*)}}
 */
export const myOrdersActions = {
  getOrders
};

/**
 * Retrieve the orders for the authenticated user
 * @returns {Promise<void>}
 */
async function getOrders() {
  myOrdersStore.update((state) => {
    state.isLoading = true;
    return state;
  });

  try {
    const pageInfo = get(myOrdersStore).pageInfo;
    const queryString = `skip=${pageInfo.skip}&take=${pageInfo.take}`;
    const res = await api.get(`/orders/own?${queryString}`);
    const orders = await res.json();
    myOrdersStore.update((state) => {
      state.pageInfo = orders.pageInfo;
      state.orders = orders.items || [];
      state.isLoading = false;
      return state;
    });
  } catch (e) {
    console.error(e);
    myOrdersStore.update((state) => {
      state.isLoading = false;
      return state;
    });
  }
}
