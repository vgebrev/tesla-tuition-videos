import { writable } from 'svelte/store';
import { api } from '$lib/api/api.js';

export const tagsStore = writable([]);

export async function getTags() {
  const tags = await api.get('/tags');
  tagsStore.set(tags);
}
