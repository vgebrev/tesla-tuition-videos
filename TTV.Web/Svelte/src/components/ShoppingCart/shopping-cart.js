import { get, writable } from 'svelte/store';
import { defaultErrorMessage } from '$lib/config.js';
import { api } from '$lib/api.js';
import { goto } from '$app/navigation';

/** @type {import('$lib/types').ShoppingCartStoreState} */
const initialState = {
  isLoading: false,
  lessons: [],
  error: { isError: false, message: '' }
};

/** Store for the shopping cart state
 * @type {Writable<import('$lib/types').ShoppingCartStoreState>} */
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
    /** @type {import('$lib/types').ShoppingCartStoreState} */
    let newState = {
      ...state,
      lessons: [...state.lessons, lesson]
    };
    newState = saveToLocalStorage(newState);
    return newState;
  });
}

/**
 * Remove a lesson from the shopping cart
 * @param {import('$lib/types').Lesson} lesson
 */
function removeLesson(lesson) {
  shoppingCartStore.update((state) => {
    /** @type {import('$lib/types').ShoppingCartStoreState} */
    let newState = {
      ...state,
      lessons: state.lessons.filter((l) => l.id !== lesson.id)
    };
    newState = saveToLocalStorage(newState);
    return newState;
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
  shoppingCartStore.update(
    (state) =>
      /** @type {import('$lib/types').ShoppingCartStoreState} */
      ({ ...state, lessons })
  );
}

/**
 * Clear the shopping cart
 */
function clearCart() {
  shoppingCartStore.update((state) => {
    /** @type {import('$lib/types').ShoppingCartStoreState} */
    let newState = {
      ...state,
      lessons: [],
      error: { isError: false, message: '' }
    };
    newState = saveToLocalStorage(newState);
    return newState;
  });
}

/**
 * Confirm the order
 */
async function confirmOrder() {
  let order;
  shoppingCartStore.update((state) => ({ ...state, isLoading: true }));
  try {
    const lessonIds = get(shoppingCartStore).lessons.map((lesson) => lesson.id);
    const response = await api.post('/orders', { lessonIds });
    order = await response.json();
    clearCart();
  } catch (e) {
    console.error(e);
    shoppingCartStore.update((state) => ({
      ...state,
      error: { isError: true, message: defaultErrorMessage }
    }));
  }
  shoppingCartStore.update((state) => ({ ...state, isLoading: false }));
  if (order) await goto(`/checkout/${order.id}`);
}
