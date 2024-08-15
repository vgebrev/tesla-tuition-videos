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

//** @type {import {Writable} from 'svelte/store'} */
export const lessonListStore = writable(initialState);

export const actions = {
  getTags,
  search,
  setTagFilterDrawer,
  setSearchTags,
  updateLessons
};

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
 * @param {boolean} isOpen
 */
function setTagFilterDrawer(isOpen) {
  lessonListStore.update((state) => ({ ...state, isTagFilterDrawerOpen: isOpen }));
}

/**
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

function updateLessons() {
  //TODO: Implement updateLessons when an order is complete with the new owner
}
