import { writable, get } from 'svelte/store';
import { api } from '$lib/api.js';
import { defaultErrorMessage } from '$lib/config.js';
import { mergeArrays } from '$lib/util.js';

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

/** Store for the lesson list state
 * @type {import('svelte/store').Writable<import('$lib/types').LessonListStoreState>} */
export const lessonListStore = writable(initialState);

/** Lesson list actions */
export const lessonListActions = {
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
  lessonListStore.update((state) => {
    state.isLoadingTags = true;
    return state;
  });
  try {
    const response = await api.get('/tags');
    const tags = await response.json();
    lessonListStore.update((state) => {
      state.tags = tags;
      state.tagsError = { isError: false, message: '' };
      return state;
    });
  } catch (e) {
    console.error(e);
    lessonListStore.update((state) => {
      state.tagsError = { isError: true, message: defaultErrorMessage };
      return state;
    });
  } finally {
    lessonListStore.update((state) => {
      state.isLoadingTags = false;
      return state;
    });
  }
}

/**
 * Search for lessons based on the provided search text and tags
 * @param {string | null} searchText
 * @param {import('$lib/types').Tag[] | null} searchTags
 * @returns {Promise<void>}
 */
async function search(searchText, searchTags) {
  lessonListStore.update((state) => {
    state.searchText = searchText;
    state.searchTags = searchTags;
    state.isLoadingLessons = true;
    return state;
  });
  try {
    const searchTagsIds = searchTags?.map((tag) => tag.id);
    const response = await api.post('/lessons/search', { searchText, searchTagsIds });
    const lessons = await response.json();
    lessonListStore.update((state) => {
      state.lessons = lessons;
      state.lessonsError = { isError: false, message: '' };
      return state;
    });
  } catch (exception) {
    console.error(exception);
    lessonListStore.update((state) => {
      state.lessonsError = { isError: true, message: defaultErrorMessage };
      return state;
    });
  } finally {
    lessonListStore.update((state) => {
      state.isLoadingLessons = false;
      return state;
    });
  }
}

/**
 * Open or close the tag filter drawer
 * @param {boolean} isOpen
 */
function setTagFilterDrawer(isOpen) {
  lessonListStore.update((state) => {
    state.isTagFilterDrawerOpen = isOpen;
    return state;
  });
}

/**
 * Set the search tags and, optionally, trigger a search
 * @param {import('$lib/types').Tag[]|null} tags
 * @param {boolean} [triggerSearch=true]
 */
async function setSearchTags(tags, triggerSearch = true) {
  lessonListStore.update((state) => {
    state.searchTags = tags;
    state.isTagFilterDrawerOpen = !triggerSearch;
    return state;
  });
  if (triggerSearch) {
    await search(null, tags);
  }
}

/**
 * Update the lessons in the store
 * @param {import('$lib/types').Lesson[]} lessons
 */
function updateLessons(lessons) {
  lessonListStore.update((state) => {
    state.lessons = mergeArrays(state.lessons || [], lessons || []);
    return state;
  });
}
