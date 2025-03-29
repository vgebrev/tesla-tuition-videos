import { writable } from 'svelte/store';

/** @type {import('$lib/types').LearningPathsStoreState} */
const initialState = {
  isLoading: false,
  learningPaths: [],
  error: { isError: false, message: '' }
};

/**
 * Learning paths state store.
 * @type {import('svelte/store').Writable<import('$lib/types').LearningPathsStoreState>}
 */
export const learningPathsState = writable(initialState);
