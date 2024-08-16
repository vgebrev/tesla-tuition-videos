import { writable } from 'svelte/store';
import { UserManager } from 'oidc-client';
import { config } from '$lib/config';

/** @type {UserManager} */
export const userManager = new UserManager(config.oidc);

/** @type {Writable<import('$lib/types').AuthStoreState>} */
export const authStore = writable({
  user: null,
  isAuthenticated: false
});

userManager.events.addUserLoaded((user) => {
  authStore.set({ user, isAuthenticated: true });
});

userManager.events.addUserUnloaded(() => {
  authStore.set({ user: null, isAuthenticated: false });
});
