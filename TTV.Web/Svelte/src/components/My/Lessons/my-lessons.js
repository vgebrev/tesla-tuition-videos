import { writable } from 'svelte/store';
import { api } from '$lib/api.js';
import { defaultErrorMessage, defaultPageSize } from '$lib/config.js';
import { mergeArrays, updateArray } from '$lib/util.js';

/** @type {import('$lib/types').MyLessonsStoreState} */
const initialState = {
  isLoading: false,
  lessons: null,
  pageInfo: { skip: 0, take: defaultPageSize, count: 0 },
  pageItems: null,
  error: { isError: false, message: '' }
};

/** Store for the "my lessons" state
 * @type {import('svelte/store').Writable<import('$lib/types').MyLessonsStoreState>} */
export const myLessonsStore = writable(initialState);

/** "My lessons" store actions */
export const myLessonsActions = {
  getOwnedLessons,
  getOwnedLessonsPage,
  addLessons
};

/**
 * Get all lessons owned by the current user
 * @returns {Promise<void>}
 */
async function getOwnedLessons() {
  myLessonsStore.update((state) => {
    state.isLoading = true;
    return state;
  });

  try {
    const response = await api.get('/lessons/own');
    const lessons = await response.json();
    myLessonsStore.update((state) => {
      state.lessons = lessons.items;
      state.isLoading = false;
      state.error = { isError: false, message: '' };
      return state;
    });
  } catch (e) {
    console.error(e);
    myLessonsStore.update((state) => {
      state.isLoading = false;
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
}

/**
 * Get a page of lessons owned by the current user
 * @param {number} skip
 * @param {number} take
 * @returns {Promise<void>}
 */
async function getOwnedLessonsPage(skip, take) {
  myLessonsStore.update((state) => {
    state.isLoading = true;
    return state;
  });

  try {
    const queryString = `skip=${skip}&take=${take}`;
    const response = await api.get(`/lessons/own?${queryString}`);
    const page = await response.json();
    myLessonsStore.update((state) => {
      state.pageItems = page.items;
      state.pageInfo = page.pageInfo;
      state.isLoading = false;
      state.error = { isError: false, message: '' };
      return state;
    });
  } catch (e) {
    console.error(e);
    myLessonsStore.update((state) => {
      state.isLoading = false;
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
}

/**
 * Add lessons to the store
 * @param {import('$lib/types').Lesson[]} lessons
 */
function addLessons(lessons) {
  myLessonsStore.update((state) => {
    state.lessons = mergeArrays(state.lessons || [], lessons || []);
    state.pageItems = updateArray(state.pageItems || [], lessons || []);
    return state;
  });
}
