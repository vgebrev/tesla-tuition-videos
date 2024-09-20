import { writable, get } from 'svelte/store';
import { api } from '$lib/api.js';
import { defaultErrorMessage, defaultPageSize } from '$lib/config.js';

/** @type {import('$lib/types').AdminOrdersStoreState} */
const initialState = {
  isLoadingOrders: false,
  isLoadingUsers: false,
  isLoadingStatuses: false,

  pageInfo: { skip: 0, take: defaultPageSize, total: 0 },
  orders: null,
  statuses: null,
  users: null,

  filter: { status: null, userId: null, isFinalised: false, from: null, to: null, skip: 0, take: defaultPageSize },

  ordersError: { isError: false, message: '' },
  statusesError: { isError: false, message: '' },
  usersError: { isError: false, message: '' }
};

/** Store for the admin orders state
 * @type {import('svelte/store').Writable<import('$lib/types').AdminOrdersStoreState>} */
export const adminOrdersStore = writable(initialState);

/** Orders admin actions */
export const adminOrdersActions = {
  getUsers,
  getStatuses,
  getOrders,
  completeOrder
};

async function getUsers() {
  adminOrdersStore.update((state) => {
    state.isLoadingUsers = true;
    return state;
  });
  try {
    const response = await api.get('/users');
    const users = await response.json();
    adminOrdersStore.update((state) => {
      state.isLoadingUsers = false;
      state.users = users;
      return state;
    });
  } catch (e) {
    console.error(e);
    adminOrdersStore.update((state) => {
      state.isLoadingUsers = false;
      state.usersError = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
}

async function getStatuses() {
  adminOrdersStore.update((state) => {
    state.isLoadingStatuses = true;
    return state;
  });
  try {
    const response = await api.get('/orders/statuses');
    const statuses = await response.json();
    adminOrdersStore.update((state) => {
      state.isLoadingStatuses = false;
      state.statuses = statuses;
      return state;
    });
  } catch (e) {
    console.error(e);
    adminOrdersStore.update((state) => {
      state.isLoadingStatuses = false;
      state.statusesError = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
}

async function getOrders() {
  adminOrdersStore.update((state) => {
    state.isLoadingOrders = true;
    return state;
  });
  try {
    const filter = get(adminOrdersStore).filter;
    const queryString = Object.keys(filter)
      .map((key) => encodeURIComponent(key) + '=' + encodeURIComponent(filter[key] == null ? '' : filter[key]))
      .join('&');
    const response = await api.get(`/orders?${queryString}`);
    const orders = await response.json();
    adminOrdersStore.update((state) => {
      state.isLoadingOrders = false;
      state.pageInfo = orders.pageInfo;
      state.orders = orders.items;
      return state;
    });
  } catch (e) {
    console.error(e);
    adminOrdersStore.update((state) => {
      state.isLoadingOrders = false;
      state.ordersError = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
}

async function completeOrder(order) {
  adminOrdersStore.update((state) => {
    state.isLoadingOrders = true;
    return state;
  });

  try {
    const response = await api.put(`/orders/${order.id}/complete-admin`, {});
    const completeOrderResult = await response.json();
    adminOrdersStore.update((state) => {
      state.isLoadingOrders = false;
      if (completeOrderResult.isSuccess) {
        state.orders = state.orders.map((o) => (o.id === order.id ? completeOrderResult.value : o));
      } else {
        state.ordersError = { isError: !completeOrderResult.isSuccess, message: completeOrderResult.message };
      }
      return state;
    });
    await getOrders();
  } catch (e) {
    console.error(e);
    adminOrdersStore.update((state) => {
      state.isLoadingOrders = false;
      state.ordersError = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
}
