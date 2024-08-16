import { writable, get } from 'svelte/store';
import { api } from '$lib/api.js';
import { defaultErrorMessage } from '$lib/config.js';

/** @type {import('$lib/types').LessonListStoreState} */
const initialState = {
  isLoadingLessons: false,
  lessons: null,
  lessonsError: { isError: false, message: '' },

  isLoadingTags: false,
  tags: null,
  tagsError: { isError: false, message: '' },

  isTagFilterDrawerOpen: false,
  searchText: null,
  searchTags: null
};

/**
 * Store for the lesson list state
 */
export const lessonListStore = writable(initialState);

/**
 * Actions for the lesson list
 */
export const actions = {
  getTags,
  search,
  setTagFilterDrawer,
  setSearchTags,
  updateLessons
};

/**
 * Fetch all tags from the API and populate the store
 * @returns {Promise<void>}
 */
async function getTags() {
  if (get(lessonListStore).tags) return;
  lessonListStore.update((state) => ({ ...state, isLoadingTags: true }));
  try {
    const response = await api.get('/tags');
    const tags = await response.json();
    lessonListStore.update((state) => ({
      ...state,
      tags,
      tagsError: { isError: false, message: '' }
    }));
  } catch (exception) {
    console.error(exception);
    lessonListStore.update((state) => ({
      ...state,
      tagsError: { isError: true, message: defaultErrorMessage }
    }));
  } finally {
    lessonListStore.update((state) => ({ ...state, isLoadingTags: false }));
  }
}

/**
 * Search for lessons based on the provided search text and tags
 * @param {string|null} searchText
 * @param {import('$lib/types').Tag[]|null}searchTags
 * @returns {Promise<void>}
 */
async function search(searchText, searchTags) {
  lessonListStore.update((state) => ({ ...state, searchText, searchTags, isLoadingLessons: true }));
  try {
    const searchTagsIds = searchTags?.map((tag) => tag.id);
    const response = await api.post('/lessons/search', { searchText, searchTagsIds });
    const lessons = await response.json();
    lessonListStore.update((state) => ({
      ...state,
      lessons,
      lessonsError: { isError: false, message: '' }
    }));
  } catch (exception) {
    console.error(exception);
    lessonListStore.update((state) => ({
      ...state,
      lessonsError: { isError: true, message: defaultErrorMessage }
    }));
  } finally {
    lessonListStore.update((state) => ({ ...state, isLoadingLessons: false }));
  }
}

/**
 * Open or close the tag filter drawer
 * @param {boolean} isOpen
 */
function setTagFilterDrawer(isOpen) {
  lessonListStore.update((state) => ({ ...state, isTagFilterDrawerOpen: isOpen }));
}

/**
 * Set the search tags and, optionally, trigger a search
 * @param {?import('$lib/types').Tag[]} tags
 * @param {boolean} [triggerSearch=true]
 */
async function setSearchTags(tags, triggerSearch = true) {
  lessonListStore.update((state) => ({
    ...state,
    searchTags: tags,
    isTagFilterDrawerOpen: !triggerSearch
  }));
  if (triggerSearch) {
    await search(null, tags);
  }
}

/**
 * Update the lessons in the store
 * @param {import('$lib/types').Lesson[]} lessons
 */
function updateLessons(lessons) {
  //TODO: Implement updateLessons when an order is complete with the new owner
  console.log('updateLessons', lessons);
}
