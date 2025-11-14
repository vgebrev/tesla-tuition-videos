import { get, writable } from 'svelte/store';
import { defaultErrorMessage } from '$lib/config.js';
import { api } from '$lib/api.js';
import { goto } from '$app/navigation';
import { resolve } from '$app/paths';
import { checkoutStore } from '$components/Checkout/checkout.js';

/** @type {import('$lib/types').ShoppingCartStoreState} */
const initialState = {
  isLoading: false,
  lessons: [],
  error: { isError: false, message: '' }
};

/** Store for the shopping cart state
 * @type {import('svelte/store').Writable<import('$lib/types').ShoppingCartStoreState>} */
export const shoppingCartStore = writable(initialState);

/** Shopping cart store actions */
export const shoppingCartActions = {
  addLesson,
  removeLesson,
  loadFromLocalStorage,
  confirmOrder
};

/**
 * Add a lesson to the shopping cart
 * @param {import('$lib/types').Lesson} lesson
 */
function addLesson(lesson) {
  shoppingCartStore.update((state) => {
    state.lessons = [...state.lessons, lesson];
    state = saveToLocalStorage(state);
    return state;
  });
}

/**
 * Remove a lesson from the shopping cart
 * @param {import('$lib/types').Lesson} lesson
 */
function removeLesson(lesson) {
  shoppingCartStore.update((state) => {
    state.lessons = state.lessons.filter((l) => l.id !== lesson.id);
    state = saveToLocalStorage(state);
    return state;
  });
}

/**
 * Save lessons to local storage
 * @param {import('$lib/types').ShoppingCartStoreState} state
 * @returns {import('$lib/types').ShoppingCartStoreState}
 */
function saveToLocalStorage(state) {
  try {
    localStorage.setItem('TTV_ShoppingCartState_Lessons', JSON.stringify(state.lessons));
  } catch (e) {
    console.error(e);
    state = { ...state, error: { isError: true, message: defaultErrorMessage } };
  }
  return state;
}

/**
 * Load the shopping cart from local storage
 */
function loadFromLocalStorage() {
  const lessons = JSON.parse(localStorage.getItem('TTV_ShoppingCartState_Lessons') || '[]');
  shoppingCartStore.update((state) => {
    state.lessons = lessons;
    return state;
  });
}

/**
 * Clear the shopping cart
 */
function clearCart() {
  shoppingCartStore.update((state) => {
    state.lessons = [];
    state.error = { isError: false, message: '' };
    state = saveToLocalStorage(state);
    return state;
  });
}

/**
 * Confirm the order
 */
async function confirmOrder() {
  /** @type {?import('$lib/types').Order} */
  let order = null;
  shoppingCartStore.update((state) => {
    state.isLoading = true;
    return state;
  });
  try {
    const lessonsIds = get(shoppingCartStore).lessons.map((lesson) => lesson.id);
    const response = await api.post('/orders', { lessonsIds });
    order = await response.json();
    clearCart();
    checkoutStore.update((state) => {
      state.isLoading = false;
      state.order = order;
      return state;
    });
  } catch (e) {
    console.error(e);
    shoppingCartStore.update((state) => {
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
  shoppingCartStore.update((state) => {
    state.isLoading = false;
    return state;
  });
  if (order) await goto(resolve(`/checkout/${order.id}`));
}
