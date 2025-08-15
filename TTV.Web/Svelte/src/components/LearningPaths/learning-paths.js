import { writable, get } from 'svelte/store';
import { api } from '$lib/api';
import { defaultErrorMessage } from '$lib/config';
/** @type {import('$lib/types').LearningPathsStoreState} */
const initialState = {
  isLoading: false,
  learningPaths: [],
  loadedLessons: [],
  error: { isError: false, message: '' }
};

/**
 * Learning paths state store.
 * @type {import('svelte/store').Writable<import('$lib/types').LearningPathsStoreState>}
 */
export const learningPathsState = writable(initialState);

export const learningPathsActions = {
  getLearningPaths,
  getLessonDetails
};

/**
 * Get learning paths from the API.
 * @returns {Promise<void>}
 */
async function getLearningPaths() {
  learningPathsState.update((state) => {
    state.isLoading = true;
    state.error = { isError: false, message: '' };
    return state;
  });
  try {
    const response = await api.get('/learning-paths');
    const learningPaths = await response.json();
    learningPathsState.update((state) => {
      state.learningPaths = learningPaths;
      state.isLoading = false;
      return state;
    });
  } catch (error) {
    console.error(error);
    learningPathsState.update((state) => {
      state.isLoading = false;
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
}

/**
 * Get lesson details from the API and add to loaded lessons.
 * @param {number} lessonId - The lesson ID to fetch
 * @returns {Promise<void>}
 */
async function getLessonDetails(lessonId) {
  // Check if lesson is already loaded
  const currentState = get(learningPathsState);
  if (currentState.loadedLessons.find((lesson) => lesson.id === lessonId)) {
    return; // Already loaded
  }

  learningPathsState.update((state) => {
    state.isLoading = true;
    state.error = { isError: false, message: '' };
    return state;
  });

  try {
    const response = await api.get(`/lessons/${lessonId}`);
    const lesson = await response.json();

    learningPathsState.update((state) => {
      state.loadedLessons = [...state.loadedLessons, lesson];
      state.isLoading = false;
      return state;
    });
  } catch (error) {
    console.error(error);
    learningPathsState.update((state) => {
      state.isLoading = false;
      state.error = { isError: true, message: defaultErrorMessage };
      return state;
    });
  }
}
