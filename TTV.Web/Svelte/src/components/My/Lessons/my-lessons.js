import { writable } from 'svelte/store';
import { api } from '$lib/api.js';
import { defaultErrorMessage } from '$lib/config.js';

/** @type {import('$lib/types').MyLessonsStoreState} */
const initialState = {
  isLoading: false,
  lessons: null,
  error: { isError: false, message: '' }
};

/* Store for the "my lessons" state
 * @type {Writable<import('$lib/types').MyLessonsStoreState>} */
export const myLessonsStore = writable(initialState);

/** "My lessons" store actions */
export const myLessonsActions = {
  getOwnedLessons
};

/**
 * Get all lessons owned by the current user
 * @returns {Promise<void>}
 */
async function getOwnedLessons() {
  myLessonsStore.update((state) => {
    return { ...state, isLoading: true };
  });

  try {
    const response = await api.get('/lessons/own');
    const lessons = await response.json();
    myLessonsStore.update((state) => {
      return { ...state, isLoading: false, lessons, error: { isError: false, message: '' } };
    });
  } catch (e) {
    console.error(e);
    myLessonsStore.update((state) => {
      return { ...state, isLoading: false, error: { isError: true, message: defaultErrorMessage } };
    });
  }
}
