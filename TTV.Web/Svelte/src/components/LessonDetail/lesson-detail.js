import { writable } from 'svelte/store';
import { defaultErrorMessage } from '$lib/config.js';
import { api } from '$lib/api.js';

/** @type {LessonDetailStoreState} */
const initialState = {
  isLoadingLesson: false,
  isLoadingDocuments: false,
  lesson: null,
  documents: [],
  error: { isError: false, message: '' }
};

/** Lesson detail store
 * @type {Writable<LessonDetailStoreState>} */
export const lessonDetailStore = writable(initialState);

/** Lesson detail actions */
export const lessonDetailActions = {
  getLesson,
  getDocuments
};

/**
 * Get a lesson by id. First try get from the user's lessons, then from the lesson search results, and finally from the API.
 * @param {number} lessonId
 */
async function getLesson(lessonId) {
  lessonDetailStore.update((state) => {
    state.isLoadingLesson = true;
    return state;
  });
  try {
    const response = await api.get(`/lessons/${lessonId}`);
    const lesson = await response.json();
    lessonDetailStore.update((state) => {
      state.lesson = lesson;
      state.error = { isError: false, message: '' };
      return state;
    });
  } catch (e) {
    console.error(e);
    lessonDetailStore.update((state) => {
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
  lessonDetailStore.update((state) => {
    state.isLoadingLesson = false;
    return state;
  });
}

/**
 * Get documents for a lesson
 * @param {number} lessonId
 * @returns {Promise<void>}
 */
async function getDocuments(lessonId) {
  lessonDetailStore.update((state) => {
    state.isLoadingDocuments = true;
    return state;
  });
  try {
    const response = await api.get(`/documents/lesson/${lessonId}`);
    const documents = await response.json();
    lessonDetailStore.update((state) => {
      state.documents = documents;
      state.error = { isError: false, message: '' };
      return state;
    });
  } catch (e) {
    console.error(e);
    lessonDetailStore.update((state) => {
      state.documents = [];
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
  lessonDetailStore.update((state) => {
    state.isLoadingDocuments = false;
    return state;
  });
}
