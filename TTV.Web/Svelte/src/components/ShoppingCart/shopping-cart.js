import { writable } from 'svelte/store';

/** @type {import('$lib/types').ShoppingCartStoreState}*/
const initialState = {
  isLoading: false,
  lessons: [],
  error: { isError: false, message: '' }
};

export const shoppingCartStore = writable(initialState);

export const actions = {
  addLesson,
  removeLesson,
  clearCart
  // confirmOrder,
  // localStorageLoad,
  // localStorageSave
};

/**
 * Add a lesson to the shopping cart
 * @param {import('$lib/types').Lesson} lesson
 */
function addLesson(lesson) {
  shoppingCartStore.update((state) => ({
    ...state,
    lessons: [...state.lessons, lesson]
  }));
}

/**
 * Remove a lesson from the shopping cart
 * @param {import('$lib/types').Lesson} lesson
 */
function removeLesson(lesson) {
  shoppingCartStore.update((state) => ({
    ...state,
    lessons: state.lessons.filter((l) => l.id !== lesson.id)
  }));
}

/**
 * Clear the shopping cart
 */
function clearCart() {
  shoppingCartStore.update((state) => ({
    ...state,
    lessons: []
  }));
}
